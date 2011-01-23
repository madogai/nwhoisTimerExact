namespace nwhois.plugin.NwhoisTimerExact {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.DoCommentCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.AsOwnerCheckBox = new System.Windows.Forms.CheckBox();
			this.PostCommandTextBox = new System.Windows.Forms.TextBox();
			this.PostCommentTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.CommunityFilterTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.AddCurrentCommunityButton = new System.Windows.Forms.Button();
			this.AnytimeWatchCheckBox = new System.Windows.Forms.CheckBox();
			this.EnableAfterExtendCheckBox = new System.Windows.Forms.CheckBox();
			this.StatusBar = new System.Windows.Forms.StatusStrip();
			this.StatusContent = new System.Windows.Forms.ToolStripStatusLabel();
			this.CallAlertSpinner = new System.Windows.Forms.NumericUpDown();
			this.WatchStateChangeCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.StatusBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.CallAlertSpinner)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "残り時間";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(152, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "分のときに";
			// 
			// DoCommentCheckBox
			// 
			this.DoCommentCheckBox.AutoSize = true;
			this.DoCommentCheckBox.Location = new System.Drawing.Point(15, 38);
			this.DoCommentCheckBox.Name = "DoCommentCheckBox";
			this.DoCommentCheckBox.Size = new System.Drawing.Size(85, 16);
			this.DoCommentCheckBox.TabIndex = 2;
			this.DoCommentCheckBox.Text = "コメントをする";
			this.DoCommentCheckBox.UseVisualStyleBackColor = true;
			this.DoCommentCheckBox.CheckedChanged += new System.EventHandler(this.DoCommentCheckBox_CheckedChanged);
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.AsOwnerCheckBox);
			this.panel1.Controls.Add(this.PostCommandTextBox);
			this.panel1.Controls.Add(this.PostCommentTextBox);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Location = new System.Drawing.Point(15, 61);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(358, 66);
			this.panel1.TabIndex = 4;
			// 
			// AsOwnerCheckBox
			// 
			this.AsOwnerCheckBox.AutoSize = true;
			this.AsOwnerCheckBox.Location = new System.Drawing.Point(305, 12);
			this.AsOwnerCheckBox.Name = "AsOwnerCheckBox";
			this.AsOwnerCheckBox.Size = new System.Drawing.Size(48, 16);
			this.AsOwnerCheckBox.TabIndex = 5;
			this.AsOwnerCheckBox.Text = "運営";
			this.AsOwnerCheckBox.UseVisualStyleBackColor = true;
			this.AsOwnerCheckBox.CheckedChanged += new System.EventHandler(this.AsOwnerCheckBox_CheckedChanged);
			// 
			// PostCommandTextBox
			// 
			this.PostCommandTextBox.Location = new System.Drawing.Point(47, 33);
			this.PostCommandTextBox.Name = "PostCommandTextBox";
			this.PostCommandTextBox.Size = new System.Drawing.Size(252, 19);
			this.PostCommandTextBox.TabIndex = 4;
			this.PostCommandTextBox.TextChanged += new System.EventHandler(this.PostCommandTextBox_TextChanged);
			// 
			// PostCommentTextBox
			// 
			this.PostCommentTextBox.Location = new System.Drawing.Point(47, 10);
			this.PostCommentTextBox.Name = "PostCommentTextBox";
			this.PostCommentTextBox.Size = new System.Drawing.Size(252, 19);
			this.PostCommentTextBox.TabIndex = 3;
			this.PostCommentTextBox.TextChanged += new System.EventHandler(this.PostCommentTextBox_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 36);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 12);
			this.label4.TabIndex = 1;
			this.label4.Text = "コマンド";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 13);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 12);
			this.label3.TabIndex = 0;
			this.label3.Text = "コメント";
			// 
			// CommunityFilterTextBox
			// 
			this.CommunityFilterTextBox.Location = new System.Drawing.Point(133, 133);
			this.CommunityFilterTextBox.Name = "CommunityFilterTextBox";
			this.CommunityFilterTextBox.Size = new System.Drawing.Size(240, 19);
			this.CommunityFilterTextBox.TabIndex = 6;
			this.CommunityFilterTextBox.TextChanged += new System.EventHandler(this.CommunityFilterTextBox_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(13, 136);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 12);
			this.label5.TabIndex = 6;
			this.label5.Text = "コミュニティフィルター";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(13, 159);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(204, 12);
			this.label6.TabIndex = 7;
			this.label6.Text = "複数指定する場合は / で区切ってください";
			// 
			// AddCurrentCommunityButton
			// 
			this.AddCurrentCommunityButton.Location = new System.Drawing.Point(233, 154);
			this.AddCurrentCommunityButton.Name = "AddCurrentCommunityButton";
			this.AddCurrentCommunityButton.Size = new System.Drawing.Size(140, 23);
			this.AddCurrentCommunityButton.TabIndex = 7;
			this.AddCurrentCommunityButton.Text = "現在のコミュニティを追加";
			this.AddCurrentCommunityButton.UseVisualStyleBackColor = true;
			// 
			// AnytimeWatchCheckBox
			// 
			this.AnytimeWatchCheckBox.AutoSize = true;
			this.AnytimeWatchCheckBox.Location = new System.Drawing.Point(125, 187);
			this.AnytimeWatchCheckBox.Name = "AnytimeWatchCheckBox";
			this.AnytimeWatchCheckBox.Size = new System.Drawing.Size(69, 16);
			this.AnytimeWatchCheckBox.TabIndex = 8;
			this.AnytimeWatchCheckBox.Text = "常に監視";
			this.AnytimeWatchCheckBox.UseVisualStyleBackColor = true;
			this.AnytimeWatchCheckBox.CheckedChanged += new System.EventHandler(this.AnytimeWatchCheckBox_CheckedChanged);
			// 
			// EnableAfterExtendCheckBox
			// 
			this.EnableAfterExtendCheckBox.AutoSize = true;
			this.EnableAfterExtendCheckBox.Location = new System.Drawing.Point(200, 187);
			this.EnableAfterExtendCheckBox.Name = "EnableAfterExtendCheckBox";
			this.EnableAfterExtendCheckBox.Size = new System.Drawing.Size(93, 16);
			this.EnableAfterExtendCheckBox.TabIndex = 9;
			this.EnableAfterExtendCheckBox.Text = "延長後も適用";
			this.EnableAfterExtendCheckBox.UseVisualStyleBackColor = true;
			this.EnableAfterExtendCheckBox.CheckedChanged += new System.EventHandler(this.EnableAfterExtendCheckBox_CheckedChanged);
			// 
			// StatusBar
			// 
			this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusContent});
			this.StatusBar.Location = new System.Drawing.Point(0, 218);
			this.StatusBar.Name = "StatusBar";
			this.StatusBar.Size = new System.Drawing.Size(384, 22);
			this.StatusBar.SizingGrip = false;
			this.StatusBar.TabIndex = 11;
			this.StatusBar.Text = "statusStrip1";
			// 
			// StatusContent
			// 
			this.StatusContent.Name = "StatusContent";
			this.StatusContent.Size = new System.Drawing.Size(0, 17);
			// 
			// CallAlertSpinner
			// 
			this.CallAlertSpinner.Location = new System.Drawing.Point(63, 11);
			this.CallAlertSpinner.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
			this.CallAlertSpinner.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.CallAlertSpinner.Name = "CallAlertSpinner";
			this.CallAlertSpinner.Size = new System.Drawing.Size(83, 19);
			this.CallAlertSpinner.TabIndex = 1;
			this.CallAlertSpinner.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.CallAlertSpinner.ValueChanged += new System.EventHandler(this.CallAlertSpinner_ValueChanged);
			// 
			// WatchStateChangeCheckBox
			// 
			this.WatchStateChangeCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.WatchStateChangeCheckBox.AutoSize = true;
			this.WatchStateChangeCheckBox.Location = new System.Drawing.Point(299, 183);
			this.WatchStateChangeCheckBox.Name = "WatchStateChangeCheckBox";
			this.WatchStateChangeCheckBox.Size = new System.Drawing.Size(74, 22);
			this.WatchStateChangeCheckBox.TabIndex = 12;
			this.WatchStateChangeCheckBox.Text = "監視スタート";
			this.WatchStateChangeCheckBox.UseVisualStyleBackColor = true;
			this.WatchStateChangeCheckBox.CheckedChanged += new System.EventHandler(this.WatchStateChangeCheckBox_CheckedChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 240);
			this.Controls.Add(this.WatchStateChangeCheckBox);
			this.Controls.Add(this.StatusBar);
			this.Controls.Add(this.CallAlertSpinner);
			this.Controls.Add(this.EnableAfterExtendCheckBox);
			this.Controls.Add(this.AnytimeWatchCheckBox);
			this.Controls.Add(this.AddCurrentCommunityButton);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.CommunityFilterTextBox);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.DoCommentCheckBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "nwhoisタイマーExact";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.StatusBar.ResumeLayout(false);
			this.StatusBar.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.CallAlertSpinner)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		internal System.Windows.Forms.CheckBox AsOwnerCheckBox;
		internal System.Windows.Forms.NumericUpDown CallAlertSpinner;
		internal System.Windows.Forms.TextBox PostCommentTextBox;
		internal System.Windows.Forms.TextBox PostCommandTextBox;
		internal System.Windows.Forms.TextBox CommunityFilterTextBox;
		internal System.Windows.Forms.CheckBox EnableAfterExtendCheckBox;
		internal System.Windows.Forms.CheckBox AnytimeWatchCheckBox;
		internal System.Windows.Forms.Button AddCurrentCommunityButton;
		internal System.Windows.Forms.CheckBox DoCommentCheckBox;
		private System.Windows.Forms.StatusStrip StatusBar;
		internal System.Windows.Forms.ToolStripStatusLabel StatusContent;
		internal System.Windows.Forms.CheckBox WatchStateChangeCheckBox;
	}
}