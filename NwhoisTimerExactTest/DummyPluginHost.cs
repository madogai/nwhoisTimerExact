using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nwhois.plugin;

namespace NwhoisTimerExactTest {
	internal sealed class DummyPluginHost : IPluginHost {
		public PostedMessage PostedMessage { get; set; }
		public bool IsPost { get; set; }

		#region IPluginHost メンバー

		public string ApplicationDataFolder {
			get { return ""; }
		}

		public bool CanPostMessage { get; set; }

		public event EventHandler ChangeNGFilter;

		public NicoApi.Chat[] Chats {
			get { return null; }
		}

		public int Comment {
			get { return 0; }
		}

		public string CommunityId { get; set; }

		public string CommunityName {
			get { return null; }
		}

		public bool ConnectLive(string liveId, bool withBrowser) {
			return false;
		}

		public bool ConnectLive(string liveId) {
			return false;
		}

		public System.Net.CookieContainer CookieContainer {
			get { return null; }
		}

		public int CurrentUser {
			get { return 0; }
		}

		public void Disconnect() {

		}

		public event EventHandler Disposed;

		public Filteringtype FilterState {
			get { return Filteringtype.Id; }
		}

		public NicoApi.Chat[] GetChatLog(string liveId) {
			return null;
		}

		public ILiveLogInfo[] GetLiveLog() {
			return null;
		}

		public NicoApi.NgClient[] GetNgClients() {
			return null;
		}

		public NicoApi.Chat GetSelectedChat() {
			return null;
		}

		public event EventHandler Initialized;

		public bool IsArchive {
			get { return false; }
		}

		public bool IsCaster { get; set;  }

		public bool IsConnected {
			get { return false; }
		}

		public bool IsPremium {
			get { return true; }
		}

		public string LiveId { get; set; }

		public string LiveName {
			get { return null; }
		}

		public event EventHandler LiveStart;

		public event EventHandler LiveStop;

		public DateTime LocalStartTime {
			get { throw new NotImplementedException(); }
		}

		public string NwhoisVersion {
			get { throw new NotImplementedException(); }
		}

		public string PluginsFolder { get; set; }

		public void PostMessage(string message, string command) {
			this.PostedMessage = new PostedMessage {
				IsOwner = false,
				Command = command,
				Message = message,
			};
		}

		public Func<bool> GetPostResult;

		public bool PostOwnerMessage(string message, string command) {
			return this.PostOwnerMessage(message, command, null);
		}

		public bool PostOwnerMessage(string message, string command, string name) {
			this.PostedMessage = new PostedMessage {
				IsOwner = true,
				Command = command,
				Message = message,
			};

			var result = GetPostResult != null ? GetPostResult() : true;
			if (result) {
				this.IsPost = true;
			}
			return result;
		}

		public event EventHandler<NicoApi.ChatReceiveEventArgs> ReceiveChat;

		public event EventHandler<NicoApi.ChatResultReceiveEventArgs> ReceiveChatResult;

		public bool Relogin() {
			return false;
		}

		public bool SelectChat(int no) {
			return false;
		}

		public string SendNGCommand(NicoApi.NgClient.MODE mode, string source, NicoApi.NgClient.TYPE type) {
			return null;
		}

		public DateTime ServerStartTime {
			get { return DateTime.Now; }
		}

		public void ShowStatusMessage(string message, bool isErrorMessage) {
		}

		public DateTime StartTime {
			get { return DateTime.Now; }
		}

		public int TotalUser {
			get { return 0; }
		}

		public event EventHandler UpdatedCommunityInfo;

		public string UserId {
			get { return null; }
		}

		public object Win32WindowOwner {
			get { return null; }
		}

		#endregion
	}

	internal class PostedMessage {
		public bool IsOwner;
		public string Command;
		public string Message;

		public override bool Equals(object obj) {
			var other = obj as PostedMessage;
			if (other == null) {
				return false;
			}
			var checkIsOwner = this.IsOwner == other.IsOwner;
			var checkMessage = this.Message == other.Message;
			var checkCommand = this.Command == other.Command;
			return checkIsOwner && checkMessage && checkCommand;
		}

		public override int GetHashCode() {
			return this.IsOwner.GetHashCode() ^ this.Command.GetHashCode() ^ this.Message.GetHashCode();
		}
	}
}
