using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nwhois.plugin;

namespace NwhoisTimerExactTest {
	internal sealed class DummyPluginHost : IPluginHost {
		public PostedMessage PostedMessage { get; set; }

		#region IPluginHost メンバー

		public string ApplicationDataFolder {
			get { throw new NotImplementedException(); }
		}

		public bool CanPostMessage { get; set; }

		public event EventHandler ChangeNGFilter;

		public NicoApi.Chat[] Chats {
			get { throw new NotImplementedException(); }
		}

		public int Comment {
			get { throw new NotImplementedException(); }
		}

		public string CommunityId { get; set; }

		public string CommunityName {
			get { throw new NotImplementedException(); }
		}

		public bool ConnectLive(string liveId, bool withBrowser) {
			throw new NotImplementedException();
		}

		public bool ConnectLive(string liveId) {
			throw new NotImplementedException();
		}

		public System.Net.CookieContainer CookieContainer {
			get { throw new NotImplementedException(); }
		}

		public int CurrentUser {
			get { throw new NotImplementedException(); }
		}

		public void Disconnect() {
			throw new NotImplementedException();
		}

		public event EventHandler Disposed;

		public Filteringtype FilterState {
			get { throw new NotImplementedException(); }
		}

		public NicoApi.Chat[] GetChatLog(string liveId) {
			throw new NotImplementedException();
		}

		public ILiveLogInfo[] GetLiveLog() {
			throw new NotImplementedException();
		}

		public NicoApi.NgClient[] GetNgClients() {
			throw new NotImplementedException();
		}

		public NicoApi.Chat GetSelectedChat() {
			throw new NotImplementedException();
		}

		public event EventHandler Initialized;

		public bool IsArchive {
			get { throw new NotImplementedException(); }
		}

		public bool IsCaster { get; set;  }

		public bool IsConnected {
			get { throw new NotImplementedException(); }
		}

		public bool IsPremium {
			get { throw new NotImplementedException(); }
		}

		public string LiveId { get; set; }

		public string LiveName {
			get { throw new NotImplementedException(); }
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

		public bool PostOwnerMessage(string message, string command, string name) {
			this.PostedMessage = new PostedMessage {
				IsOwner = true,
				Command = command,
				Message = message,
			};
			return true;
		}

		public bool PostOwnerMessage(string message, string command) {
			this.PostedMessage = new PostedMessage {
				IsOwner = true,
				Command = command,
				Message = message,
			};
			return true;
		}

		public event EventHandler<NicoApi.ChatReceiveEventArgs> ReceiveChat;

		public event EventHandler<NicoApi.ChatResultReceiveEventArgs> ReceiveChatResult;

		public bool Relogin() {
			throw new NotImplementedException();
		}

		public bool SelectChat(int no) {
			throw new NotImplementedException();
		}

		public string SendNGCommand(NicoApi.NgClient.MODE mode, string source, NicoApi.NgClient.TYPE type) {
			throw new NotImplementedException();
		}

		public DateTime ServerStartTime {
			get { throw new NotImplementedException(); }
		}

		public void ShowStatusMessage(string message, bool isErrorMessage) {
			throw new NotImplementedException();
		}

		public DateTime StartTime {
			get { throw new NotImplementedException(); }
		}

		public int TotalUser {
			get { throw new NotImplementedException(); }
		}

		public event EventHandler UpdatedCommunityInfo;

		public string UserId {
			get { throw new NotImplementedException(); }
		}

		public object Win32WindowOwner {
			get { throw new NotImplementedException(); }
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
