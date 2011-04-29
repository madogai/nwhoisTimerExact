namespace nwhois.plugin.NwhoisTimerExact {
	using System;
	using System.Text;
	using System.Net;
	using System.Threading;
	using System.Diagnostics;
	using System.Xml.XPath;
	using System.Xml;
	using System.Net.Sockets;

	internal sealed class NicoLiveInfoIntervalTimer : IDisposable {
		private const String NicoLivePlayerInfoUri = @"http://live.nicovideo.jp/api/getplayerstatus";
		private const Int32 DefaultSyncInterval = 5 * 60 * 1000;

		public UInt32 SyncNicoLiveServerInterval { get; set; }
		public Boolean IsStart { get; private set; }

		private CookieContainer cookieContainer;
		private Timer syncTimer;
		private String liveId;

		public event EventHandler OnSyncStart;
		public event EventHandler OnSyncEnd;
		public event NicoLiveSyncEventHandler OnSync;

		public delegate void NicoLiveSyncEventHandler(NicoLiveInfo liveInfo);

		public NicoLiveInfoIntervalTimer() {
			this.SyncNicoLiveServerInterval = DefaultSyncInterval;
			this.syncTimer = new Timer(OnTick, null, Timeout.Infinite, Timeout.Infinite);
		}

		public void SyncStart(String liveId, CookieContainer cookieContainer) {
			if (this.IsStart) {
				Debug.WriteLine("既に同期タイマーが開始されています。");
				return;
			}

			this.liveId = liveId;
			this.cookieContainer = cookieContainer;
			this.syncTimer.Change(0, this.SyncNicoLiveServerInterval);
			this.IsStart = true;
			Debug.WriteLine("ニコ生との同期タイマーを開始しました。");
			if (this.OnSyncStart != null) {
				this.OnSyncStart(this, EventArgs.Empty);
			}
		}

		public void SyncStop() {
			if (this.IsStart == false) {
				return;
			}

			this.syncTimer.Change(Timeout.Infinite, Timeout.Infinite);
			this.IsStart = false;
			Debug.WriteLine("ニコ生との同期タイマーを終了しました。");
			if (this.OnSyncEnd != null) {
				this.OnSyncEnd(this, EventArgs.Empty);
			}
		}

		public void ResetSync() {
			if (this.IsStart) {
				this.syncTimer.Change(0, this.SyncNicoLiveServerInterval);
			}
		}

		private void OnTick(Object obj) {
			if (this.IsStart == false) {
				return;
			}
			if (this.liveId == null) {
				return;
			}
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			this.ConnectLiveInfoAsync(this.liveId, xmlDoc => {
				if (stopwatch.IsRunning) {
					stopwatch.Stop();
				}

				try {
					var rootNode = xmlDoc.SelectSingleNode(@"/getplayerstatus");
					if (rootNode.Attributes["status"].Value != "ok") {
						Debug.WriteLine("放送の情報を取得できませんでした。");
						return;
					};

					if (this.OnSync != null) {
						var liveInfo = new NicoLiveInfo(xmlDoc, stopwatch.ElapsedMilliseconds);
						this.OnSync(liveInfo);
						Debug.WriteLine("ニコ生サーバと同期しました。");
					}
				} catch (XPathException) {
					Debug.WriteLine("XMLの解析に失敗しました。");
				}
			});
		}

		private void ConnectLiveInfoAsync(String liveId, Action<XmlDocument> method) {
			using (var webClient = new CookieAwareWebClient(this.cookieContainer)) {
				try {
					webClient.Encoding = Encoding.UTF8;
					webClient.QueryString.Add("v", liveId);
					webClient.OpenReadCompleted += (sender, e) => {
						var xmlDoc = new XmlDocument();
						xmlDoc.Load(e.Result);
						method(xmlDoc);
					};
					webClient.OpenReadAsync(new Uri(NicoLivePlayerInfoUri));
				} catch (SocketException e) {
					Debug.Write(e.Message);
				} catch (WebException e) {
					Debug.Write(e.Message);
				}
			}
		}

		#region IDisposable メンバー

		public void Dispose() {
			if (this.syncTimer != null) {
				this.syncTimer.Dispose();
			}
		}

		#endregion
	}

	internal sealed class NicoLiveInfo {
		public DateTime ServerTime { get; private set; }
		public DateTime StartTime { get; private set; }
		public DateTime EndTime { get; private set; }
		public DateTime BaseTime { get; private set; }
		public DateTime OpenTime { get; private set; }

		public NicoLiveInfo(XmlDocument xmlDoc, Int64 syncDelay) {
			try {
				var rootNode = xmlDoc.SelectSingleNode(@"/getplayerstatus");
				var startTimeNode = rootNode.SelectSingleNode(@"/getplayerstatus/stream/start_time");
				var endTimeNode = rootNode.SelectSingleNode(@"/getplayerstatus/stream/end_time");
				var baseTimeNode = rootNode.SelectSingleNode(@"/getplayerstatus/stream/base_time");
				var openTimeNode = rootNode.SelectSingleNode(@"/getplayerstatus/stream/open_time");

				this.ServerTime = UnixTime.FromUnixTime(Int64.Parse(rootNode.Attributes["time"].Value)).AddSeconds(-1 * (syncDelay / 1000));
				this.StartTime = UnixTime.FromUnixTime(Int64.Parse(startTimeNode.InnerText));
				this.EndTime = UnixTime.FromUnixTime(Int64.Parse(endTimeNode.InnerText));
				this.BaseTime = UnixTime.FromUnixTime(Int64.Parse(baseTimeNode.InnerText));
				this.OpenTime = UnixTime.FromUnixTime(Int64.Parse(openTimeNode.InnerText));

				Debug.WriteLine(syncDelay);
				Debug.WriteLine(UnixTime.FromUnixTime(Int64.Parse(rootNode.Attributes["time"].Value)));
				Debug.WriteLine(this.ServerTime);

			} catch (NullReferenceException e) {
				throw new ArgumentException("XMLの形式が不正です。", e);
			}
		}
	}
}
