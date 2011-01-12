using System.Diagnostics;
namespace NwhoisTimerExact {
	using System;
	using System.Text;
	using System.Windows.Forms;
	using System.Xml;
	using System.Xml.XPath;
	using nwhois.plugin;

	public sealed class NwhoisTimerExact : IPlugin {
		private const String FileName = "NwhoisTimerExact.bin";
		private const String NicoLivePlayerInfoUri = @"http://live.nicovideo.jp/api/getplayerstatus";
		private const Int32 Interval = 1000;
		private const String LogFilePath = @"exact.log";

		private MainForm form = null;
		private NwhoisTimerExactData pluginData;
		private Boolean enableAlert;
		private Boolean EnableAlert {
			get {
				return this.enableAlert;
			}
			set {
				if (value == true) {
					this.IsAlertDone = false;
				}
				this.enableAlert = value;
			}
		}
		private event EventHandler OnAlertCall;

		private DateTime? currentEndTime;
		private Boolean isAlertDone;
		private Boolean IsAlertDone {
			get {
				return this.isAlertDone;
			}
			set {
				if (value == false) {
					Debug.WriteLine("アラートの実行フラグをクリアしました。");
				}
				this.isAlertDone = value;
			}
		}

		public NwhoisTimerExact() {
			DefaultTraceListener drl = (DefaultTraceListener)Debug.Listeners["Default"];
			drl.LogFileName = LogFilePath;

			Debug.WriteLine("-----------------------開始--------------------------");
			// コンストラクタが呼ばれた時点ではHostにアクセスできないため、凌ぐ手段としてHostにアクセスできるまで待機させます。
			var loaded = false;
			System.Threading.Timer timer = null;
			timer = new System.Threading.Timer(obj => {
				if (loaded == false) {
					loaded = true;
					this.Initialize();
					timer.Dispose();
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
			if (this.form != null) {
				return;
			}

			var form = new MainForm(this.pluginData);
			form.FormClosed += (sender, e) => {
				this.form = null;
			};
			form.WatchStateChangeCheckBox.CheckedChanged += (sender, e) => {
				this.EnableAlert = form.WatchStateChangeCheckBox.Checked;
			};
			form.CallAlertSpinner.ValueChanged += (sender, e) => {
				this.IsAlertDone = false;
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

			form.Show((IWin32Window)this.Host.Win32WindowOwner);
			this.form = form;
		}

		#endregion

		private void Initialize() {
			Debug.WriteLine("プラグインの初期化を開始します。");
			this.pluginData = DataManager.Load(this.Host.ApplicationDataFolder, NwhoisTimerExact.FileName);
			this.Host.Disposed += (sender, e) => {
				DataManager.Save(this.Host.ApplicationDataFolder, NwhoisTimerExact.FileName, this.pluginData);
			};

			var pluginTimer = new System.Windows.Forms.Timer();
			pluginTimer.Interval = Interval;
			pluginTimer.Tick += OnTick;

			this.OnAlertCall += OnAlert;

			this.Host.UpdatedCommunityInfo += (sender, e) => {
				// 延長時にこのイベントが走るのならば、延長関連をもっとスマートに書けます。
				Debug.WriteLine("放送内容が更新されました。");
			};
			this.Host.LiveStart += (sender, e) => {
				Debug.WriteLine("放送に接続しました。");

				this.IsAlertDone = false;
				this.currentEndTime = null;

				if (this.Host.IsArchive == false) {
					pluginTimer.Start();
					Debug.WriteLine("タイマーを起動しました。");
				}
			};
			this.Host.LiveStop += (sender, e) => {
				Debug.WriteLine("放送から切断されました。");
				if (pluginTimer.Enabled) {
					pluginTimer.Stop();
					Debug.WriteLine("タイマーを停止しました。");
				}
			};

			// 常に監視がオンになっている場合、最初から監視します。
			this.EnableAlert = this.pluginData.AnytimeWatch;
		}

		private void OnTick(Object sender, EventArgs e) {
			if (this.EnableAlert == false) {
				Debug.WriteLine("監視しない設定になっています。");
				return;
			}

			if (this.Host.IsConnected == false) {
				Debug.WriteLine("放送に接続されていません。");
				return;
			}

			if (this.Host.IsArchive) {
				Debug.WriteLine("タイムシフト放送です。");
				return;
			}

			this.ConnectLiveInfoAsync(this.Host.LiveId, xmlDoc => {
				try {
					var serverTimeNode = xmlDoc.SelectSingleNode(@"/getplayerstatus");
					var endTimeNode = xmlDoc.SelectSingleNode(@"/getplayerstatus/stream/end_time");
					if (serverTimeNode == null || endTimeNode == null) {
						Debug.WriteLine("放送の情報を取得できませんでした。");
						return;
					}

					var serverTime = UnixTime.FromUnixTime(Int64.Parse(serverTimeNode.Attributes["time"].Value));
					var endTime = UnixTime.FromUnixTime(Int64.Parse(endTimeNode.InnerText));
					var alertMinute = (Int32)this.pluginData.CallAlertTime;

					if (this.currentEndTime.HasValue == false) {
						this.currentEndTime = endTime;
					}

					var isEndTimeChange = endTime > this.currentEndTime.Value;
					if (this.pluginData.EnableAfterExtend && isEndTimeChange) {
						Debug.WriteLine("延長されました。");
						this.currentEndTime = endTime;
						this.IsAlertDone = false;
					}
					var alertTime = endTime.Subtract(new TimeSpan(0, alertMinute, 0));
					Debug.WriteLine(String.Format("ServerTime : {0}, AlertTime : {1}", serverTime, alertTime));
					if (serverTime >= alertTime) {
						this.OnAlertCall(this.Host, EventArgs.Empty);
					}
				} catch (XPathException) {
					Debug.WriteLine("ログインしていません。");
				}
			});
		}

		private void OnAlert(Object sender, EventArgs e) {
			if (this.Host.CanPostMessage == false) {
				Debug.WriteLine("コメントが投稿できません。");
				return;
			}

			if (this.pluginData.DoComment == false) {
				Debug.WriteLine("コメントは投稿されない設定になっています。");
				return;
			}

			if (this.IsAlertDone) {
				Debug.WriteLine("アラートは既に流れています。");
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
			if (this.pluginData.AsOwner && this.Host.IsCaster) {
				this.Host.PostOwnerMessage(message, command);
			} else {
				this.Host.PostMessage(message, command);
			}

			Debug.WriteLine("アラートを投稿しました。");
			this.IsAlertDone = true;
		}

		private void ConnectLiveInfoAsync(String liveId, Action<XmlDocument> method) {
			using (var webClient = new CookieAwareWebClient(this.Host.CookieContainer)) {
				webClient.Encoding = Encoding.UTF8;
				webClient.QueryString.Add("v", liveId);
				webClient.OpenReadCompleted += (sender, e) => {
					var xmlDoc = new XmlDocument();
					xmlDoc.Load(e.Result);
					method(xmlDoc);
				};
				webClient.OpenReadAsync(new Uri(NicoLivePlayerInfoUri));
			}
		}
	}
}
