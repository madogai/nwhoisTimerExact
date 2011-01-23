using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace nwhois.plugin.NwhoisTimerExact {
	public partial class MainForm : Form {
		private NwhoisTimerExactData pluginData;

		public MainForm(NwhoisTimerExactData pluginData) {
			InitializeComponent();
			this.pluginData = pluginData;
			this.CallAlertSpinner.Value = pluginData.CallAlertTime;
			this.DoCommentCheckBox.Checked = pluginData.DoComment;
			this.PostCommentTextBox.Text = pluginData.PostComment;
			this.PostCommandTextBox.Text = pluginData.PostCommand;
			this.AsOwnerCheckBox.Checked = pluginData.AsOwner;
			this.CommunityFilterTextBox.Text = String.Join(@"/", pluginData.CommunityFilter);
			this.AnytimeWatchCheckBox.Checked = pluginData.AnytimeWatch;
			this.EnableAfterExtendCheckBox.Checked = pluginData.EnableAfterExtend;
		}

		private void CallAlertSpinner_ValueChanged(object sender, EventArgs e) {
			this.pluginData.CallAlertTime = this.CallAlertSpinner.Value;
		}

		private void DoCommentCheckBox_CheckedChanged(object sender, EventArgs e) {
			this.pluginData.DoComment = this.DoCommentCheckBox.Checked;
		}

		private void PostCommentTextBox_TextChanged(object sender, EventArgs e) {
			this.pluginData.PostComment = this.PostCommentTextBox.Text;
		}

		private void PostCommandTextBox_TextChanged(object sender, EventArgs e) {
			this.pluginData.PostCommand = this.PostCommandTextBox.Text;
		}

		private void AsOwnerCheckBox_CheckedChanged(object sender, EventArgs e) {
			this.pluginData.AsOwner = this.AsOwnerCheckBox.Checked;
		}

		private void CommunityFilterTextBox_TextChanged(object sender, EventArgs e) {
			var trimedText = Regex.Replace(this.CommunityFilterTextBox.Text, @"\s", "");
			this.pluginData.CommunityFilter = String.IsNullOrEmpty(trimedText) ? new String[0] : trimedText.Split('/');
		}

		private void AnytimeWatchCheckBox_CheckedChanged(object sender, EventArgs e) {
			if (this.AnytimeWatchCheckBox.Checked) {
				this.WatchStateChangeCheckBox.Checked = true;
			}
			this.pluginData.AnytimeWatch = this.AnytimeWatchCheckBox.Checked;
		}

		private void EnableAfterExtendCheckBox_CheckedChanged(object sender, EventArgs e) {
			this.pluginData.EnableAfterExtend = this.EnableAfterExtendCheckBox.Checked;
		}

		private void WatchStateChangeCheckBox_CheckedChanged(object sender, EventArgs e) {
			if (this.WatchStateChangeCheckBox.Checked) {
				this.WatchStateChangeCheckBox.Text = "監視ストップ";
			} else {
				this.AnytimeWatchCheckBox.Checked = false;
				this.WatchStateChangeCheckBox.Text = "監視スタート";
			}
		}
	}
}
