namespace nwhois.plugin.NwhoisTimerExact {
	using System;
	using System.IO;
	using System.Xml;
	using System.Xml.Serialization;
	using System.Windows.Forms;

	internal sealed class DataManager {
		public static NwhoisTimerExactData Load(String folderPath, String fileName) {
			var filePath = Path.Combine(folderPath, fileName);
			if (File.Exists(filePath) == false) {
				return new NwhoisTimerExactData();
			}
			NwhoisTimerExactData model;
			try {
				var serializer = new XmlSerializer(typeof(NwhoisTimerExactData));
				using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
					model = (NwhoisTimerExactData)serializer.Deserialize(fs);
				}
			} catch (InvalidOperationException) {
				MessageBox.Show("ファイルの読み込みに失敗しました。");
				model = new NwhoisTimerExactData();
			} catch (XmlException) {
				MessageBox.Show("ファイルの読み込みに失敗しました。");
				model = new NwhoisTimerExactData();
			}
			return model;
		}

		public static void Save(String folderPath, String fileName, NwhoisTimerExactData model) {
			try {
				var filePath = Path.Combine(folderPath, fileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create)) {
					new XmlSerializer(typeof(NwhoisTimerExactData)).Serialize(fileStream, model);
				}
			} catch (InvalidOperationException) {
				MessageBox.Show("ファイルの保存に失敗しました。");
			}
		}
	}

	[Serializable]
	public class NwhoisTimerExactData {
		public Decimal callAlertTime;
		public Decimal CallAlertTime {
			get {
				return this.callAlertTime;
			}
			set {
				this.callAlertTime = value;
				if (this.OnChangeCallAlertTime != null) {
					this.OnChangeCallAlertTime(value);
				}
			}
		}
		public Boolean doComment;
		public Boolean DoComment { get; set; }
		public String PostComment { get; set; }
		public String PostCommand { get; set; }
		public Boolean AsOwner { get; set; }
		public String[] communityFilter;
		public String[] CommunityFilter { get; set; }
		public Boolean AnytimeWatch { get; set; }
		public Boolean EnableAfterExtend { get; set; }

		public event ChangeTimeEventHandler OnChangeCallAlertTime;

		public delegate void ChangeTimeEventHandler(Decimal param);

		public NwhoisTimerExactData() {
			this.CallAlertTime = 1;
			this.DoComment = false;
			this.PostComment = String.Empty;
			this.PostCommand = String.Empty;
			this.AsOwner = false;
			this.CommunityFilter = new String[0];
			this.AnytimeWatch = false;
			this.EnableAfterExtend = false;
		}
	}
}
