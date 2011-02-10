namespace nwhois.plugin.NwhoisTimerExact {
	using System;
	using System.Diagnostics;
	using System.Collections.Generic;
	using System.IO;
	using System.Threading;
	using nwhois.plugin;

	public sealed class NwhoisTimerExact : IPlugin {
		private const String FileName = "NwhoisTimerExact.bin";
		private const String LogFilePath = @"exact.log";
		private const int RetryPostInterval = 1000;
		private const int RetryPostCount = 10;

		private MainForm form = null;
		private NwhoisTimerExactData pluginData;
		private Boolean enableAlert;

		private event EventHandler OnAlertCall;

		public NwhoisTimerExact() {
#if DEBUG
			var sw = new StreamWriter(LogFilePath) {
				AutoFlush = true,
			};
			Debug.Listeners.Add(new TextWriterTraceListener(sw));
#endif

			Debug.WriteLine("-----------------------開始--------------------------");
			this.OnAlertCall += this.OnAlert;

			// コンストラクタが呼ばれた時点ではHostにアクセスできないため、凌ぐ手段としてHostにアクセスできるまで待機させます。
			var loaded = false;
			Timer timer = null;
			timer = new System.Threading.Timer(obj => {
				Debug.WriteLine(this.Host == null);
				Debug.WriteLine(String.Format("loaded: {0}", loaded));
				if (loaded == false && this.Host != null) {
					Debug.WriteLine("IHostが見つかりました。");
					loaded = true;
					this.Initialize();
					if (timer != null) {
						timer.Dispose();
					}
				}
			}, null, 0, 100);
		}

		#region IPlugin メンバー

		public IPluginHost Host { get; set; }

		public string Name {
			get { return "nwhoisタイマーExact"; }
		}

		public string Description {
			get { return "テスト放送に対応させたタイマーです。"; }
		}

		public bool IsAlive {
			get {
				return false;
			}
		}

		public void OnComment(NicoApi.Chat chat) {
		}

		public void OnLiveStart(string liveId, DateTime baseTime, int commentCount) {
		}

		public void OnLiveStop() {
		}

		public void Run() {
			this.OpenForm();
		}

		#endregion

		private void Initialize() {
			Debug.WriteLine("プラグインの初期化を開始します。");
			this.pluginData = DataManager.Load(this.Host.ApplicationDataFolder, NwhoisTimerExact.FileName);
			var alertTimer = new Timer(p => {
				if (this.OnAlertCall != null) {
					this.OnAlertCall(this.Host, EventArgs.Empty);
				}
			}, null, Timeout.Infinite, Timeout.Infinite);

			var nicoLiveTimer = new NicoLiveInfoIntervalTimer();
			nicoLiveTimer.OnSync += liveInfo => {
				var alertMinute = (Int32)this.pluginData.CallAlertTime;
				var alertTime = liveInfo.EndTime.AddMinutes(-1 * alertMinute);
				var callAlertMillisec = (Int32)alertTime.Subtract(liveInfo.ServerTime).TotalMilliseconds;
				if (callAlertMillisec > 0) {
					alertTimer.Change(callAlertMillisec, Timeout.Infinite);
					Debug.WriteLine(String.Format("アラートを設定しました。{0} ミリ秒後", callAlertMillisec));
				}
			};
			nicoLiveTimer.OnSyncEnd += (sender, e) => {
				alertTimer.Change(Timeout.Infinite, Timeout.Infinite);
			};

			this.pluginData.OnChangeCallAlertTime += param => {
				nicoLiveTimer.ResetSync();
			};

			this.Host.Disposed += (sender, e) => {
				alertTimer.Dispose();
				nicoLiveTimer.Dispose();
			};
			this.Host.UpdatedCommunityInfo += (sender, e) => {
				// 延長時にこのイベントが走るのならば、延長関連をもっとスマートに書けます。
				Debug.WriteLine("放送内容が更新されました。");
			};
			this.Host.LiveStart += (sender, e) => {
				Debug.WriteLine("放送に接続しました。");
				if (this.Host.IsArchive) {
					Debug.WriteLine("アーカイブ放送です。");
					return;
				}

				nicoLiveTimer.SyncStart(this.Host.LiveId, this.Host.CookieContainer);
			};
			this.Host.LiveStop += (sender, e) => {
				Debug.WriteLine("放送から切断されました。");
				if (nicoLiveTimer.IsStart) {
					nicoLiveTimer.SyncStop();
				}
			};

			// 『常に監視』がオンになっている場合、最初から監視します。
			this.enableAlert = this.pluginData.AnytimeWatch;
		}

		private void OpenForm() {
			if (this.form != null) {
				return;
			}

			var form = new MainForm(this.pluginData);
			form.FormClosed += (sender, e) => {
				DataManager.Save(this.Host.ApplicationDataFolder, NwhoisTimerExact.FileName, this.pluginData);
				this.form = null;
			};
			form.WatchStateChangeCheckBox.CheckedChanged += (sender, e) => {
				this.enableAlert = form.WatchStateChangeCheckBox.Checked;
			};
			form.AddCurrentCommunityButton.Click += (sender, e) => {
				if (String.IsNullOrEmpty(this.Host.CommunityId)) {
					return;
				}

				if (String.IsNullOrEmpty(form.CommunityFilterTextBox.Text) == false) {
					form.CommunityFilterTextBox.Text += "/";
				}
				form.CommunityFilterTextBox.Text += this.Host.CommunityId;
			};
			if (this.pluginData.AnytimeWatch) {
				form.WatchStateChangeCheckBox.Checked = true;
			}

			form.Show((System.Windows.Forms.IWin32Window)this.Host.Win32WindowOwner);
			this.form = form;
		}

		private void OnAlert(Object sender, EventArgs e) {
			if (this.enableAlert == false) {
				Debug.WriteLine("監視しない設定になっています。");
				return;
			}

			if (this.Host.CanPostMessage == false) {
				Debug.WriteLine("コメントが投稿できません。");
				return;
			}

			if (this.pluginData.DoComment == false) {
				Debug.WriteLine("コメントは投稿されない設定になっています。");
				return;
			}

			var enableCommunityFilter = this.pluginData.CommunityFilter.Length > 0;
			var hasCommynity = Array.Exists(this.pluginData.CommunityFilter, commynityId => this.Host.CommunityId == commynityId);
			if (enableCommunityFilter && hasCommynity == false) {
				Debug.WriteLine("コミュニティーフィルターに一致しないため、コメントは送信されません。");
				return;
			}

			var message = this.pluginData.PostComment;
			var command = this.pluginData.PostCommand;

			Debug.WriteLine(String.Format(@"コマンド: {0}, メッセージ: {1}", command, message));
			if (this.pluginData.AsOwner && this.Host.IsCaster) {
				for (var i = 0; ; i++) {
					var doPost = this.Host.PostOwnerMessage(message, command);
					if (doPost) {
						Debug.WriteLine("運営者コメントを投稿しました。");
						break;
					}
					if (i == RetryPostCount) {
						break;
					}
					Thread.Sleep(RetryPostInterval);
				}
			} else {
				this.Host.PostMessage(message, command);
				Debug.WriteLine("コメントを投稿しました。");
			}
		}
	}
}
