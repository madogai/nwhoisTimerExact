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
				throw;
			}
		}
	}

	public class NwhoisTimerExactData {
		public Decimal CallAlertTime;
		public Boolean DoComment;
		public String PostComment;
		public String PostCommand;
		public Boolean AsOwner;
		public String[] CommunityFilter;
		public Boolean AnytimeWatch;
		public Boolean EnableAfterExtend;

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
