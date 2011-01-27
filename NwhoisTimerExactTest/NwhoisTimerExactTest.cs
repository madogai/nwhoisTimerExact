namespace NwhoisTimerExactTest {
	using nwhois.plugin.NwhoisTimerExact;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System;
	using nwhois.plugin;

	/// <summary>
	///NwhoisTimerExactTest のテスト クラスです。すべての
	///NwhoisTimerExactTest 単体テストをここに含めます
	///</summary>
	[TestClass()]
	public class NwhoisTimerExactTest {

		[TestMethod()]
		[DeploymentItem("nwhoisTimerExact.dll")]
		public void OnAlertFailCommunityFilter() {
			// Arrange
			var target = CreateTestInstance();
			target.pluginData.CommunityFilter = new[] { "co317507" };
			((DummyPluginHost)target.Host).CommunityId = "co123";

			// Act
			target.OnAlert(this, EventArgs.Empty);
			var postedMessage = ((DummyPluginHost)target.Host).PostedMessage;

			// Assert
			Assert.IsNull(postedMessage);
		}

		[TestMethod()]
		[DeploymentItem("nwhoisTimerExact.dll")]
		public void OnAlertSuccessCommunityFilter() {
			// Arrange
			var target = CreateTestInstance();
			target.pluginData.CommunityFilter = new[] { "co317507" };
			((DummyPluginHost)target.Host).CommunityId = "co317507";
			target.pluginData.PostCommand = "command";
			target.pluginData.PostComment = "comment";

			// Act
			target.OnAlert(this, EventArgs.Empty);
			var postedMessage = ((DummyPluginHost)target.Host).PostedMessage;

			// Assert
			Assert.AreEqual(new PostedMessage {
				IsOwner = false,
				Command = "command",
				Message = "comment",
			}, postedMessage);
		}


		[TestMethod()]
		[DeploymentItem("nwhoisTimerExact.dll")]
		public void OnAlertSuccessNormalPost() {
			// Arrange
			var target = CreateTestInstance();
			target.pluginData.PostCommand = "command";
			target.pluginData.PostComment = "comment";

			// Act
			target.OnAlert(this, EventArgs.Empty);
			var postedMessage = ((DummyPluginHost)target.Host).PostedMessage;

			// Assert
			Assert.AreEqual(new PostedMessage {
				IsOwner = false,
				Command = "command",
				Message = "comment",
			}, postedMessage);
		}

		[TestMethod()]
		[DeploymentItem("nwhoisTimerExact.dll")]
		public void OnAlertSuccessPostOwner() {
			// Arrange
			var target = CreateTestInstance();
			target.pluginData.AsOwner = true;
			target.pluginData.PostCommand = "command";
			target.pluginData.PostComment = "comment";
			target.Host = new DummyPluginHost {
				CanPostMessage = true,
				IsCaster = true,
			};

			// Act
			target.OnAlert(this, EventArgs.Empty);
			var postedMessage = ((DummyPluginHost)target.Host).PostedMessage;

			// Assert
			Assert.AreEqual(new PostedMessage {
				IsOwner = true,
				Command = "command",
				Message = "comment",
			}, postedMessage);
		}

		private NwhoisTimerExact_Accessor CreateTestInstance() {
			var host = new DummyPluginHost {
				CanPostMessage = true,
			};
			var data = new NwhoisTimerExactData {
				DoComment = true,
			};
			var target = new NwhoisTimerExact_Accessor {
				Host = host,
				pluginData = data,
				enableAlert = true,
			};
			return target;
		}
	}
}
