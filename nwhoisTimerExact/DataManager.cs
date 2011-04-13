namespace nwhois.plugin.NwhoisTimerExact {
	using System;
	using System.IO;
	using System.Xml;
	using System.Xml.Serialization;
	using System.Windows.Forms;
	using System.Collections.Generic;
	using System.Diagnostics;

	internal sealed class DataManager {
		private static readonly string LoadErrorMessage = "ファイルの読み込みに失敗したため、設定を初期化します。";
		private static readonly string SaveErrorMessage = "ファイルの保存に失敗しました。";

		public static NwhoisTimerExactData Load(String folderPath, String fileName) {
			var filePath = Path.Combine(folderPath, fileName);
			if (File.Exists(filePath) == false) {
				return new NwhoisTimerExactData();
			}

			NwhoisTimerExactData model = new NwhoisTimerExactData();
			try {
				var xmlDoc = new XmlDocument();
				xmlDoc.Load(filePath);
				model.CallAlertTime = Decimal.Parse(xmlDoc.SelectSingleNode("/NwhoisTimerExactData/CallAlertTime").InnerText);
				model.DoComment = Boolean.Parse(xmlDoc.SelectSingleNode("/NwhoisTimerExactData/DoComment").InnerText);
				model.PostComment = xmlDoc.SelectSingleNode("/NwhoisTimerExactData/PostComment").InnerText;
				model.PostCommand = xmlDoc.SelectSingleNode("/NwhoisTimerExactData/PostCommand").InnerText;
				model.AsOwner = Boolean.Parse(xmlDoc.SelectSingleNode("/NwhoisTimerExactData/AsOwner").InnerText);
				IList<String> filterList = new List<string>();
				foreach (XmlNode filter in xmlDoc.SelectNodes("/NwhoisTimerExactData/CommunityFilters/CommunityFilter")) {
					filterList.Add(filter.InnerText);
				}
				model.CommunityFilter = filterList;
				model.AnytimeWatch = Boolean.Parse(xmlDoc.SelectSingleNode("/NwhoisTimerExactData/AnytimeWatch").InnerText);
			} catch (InvalidOperationException e) {
				MessageBox.Show(LoadErrorMessage);
				Debug.WriteLine(e.StackTrace);
			    model = new NwhoisTimerExactData();
			} catch (XmlException e) {
				MessageBox.Show(LoadErrorMessage);
				Debug.WriteLine(e.StackTrace);
			    model = new NwhoisTimerExactData();
			} catch (FormatException e) {
				MessageBox.Show(LoadErrorMessage);
				Debug.WriteLine(e.StackTrace);
			    model = new NwhoisTimerExactData();
			} catch (ArgumentNullException e) {
				MessageBox.Show(LoadErrorMessage);
				Debug.WriteLine(e.StackTrace);
			    model = new NwhoisTimerExactData();
			}
			return model;
		}

		public static void Save(String folderPath, String fileName, NwhoisTimerExactData model) {
			try {
				var filePath = Path.Combine(folderPath, fileName);
				var xmlDoc = new XmlDocument();
				xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
				XmlNode root = xmlDoc.CreateElement("NwhoisTimerExactData");

				var CallAlertTime = xmlDoc.CreateElement("CallAlertTime");
				CallAlertTime.InnerText = model.CallAlertTime.ToString();
				root.AppendChild(CallAlertTime);

				var DoComment = xmlDoc.CreateElement("DoComment");
				DoComment.InnerText = model.DoComment.ToString();
				root.AppendChild(DoComment);

				var PostComment = xmlDoc.CreateElement("PostComment");
				PostComment.InnerText = model.PostComment;
				root.AppendChild(PostComment);

				var PostCommand = xmlDoc.CreateElement("PostCommand");
				PostCommand.InnerText = model.PostCommand;
				root.AppendChild(PostCommand);

				var AsOwner = xmlDoc.CreateElement("AsOwner");
				AsOwner.InnerText = model.AsOwner.ToString();
				root.AppendChild(AsOwner);

				var CommunityFilters = xmlDoc.CreateElement("CommunityFilters");
				foreach (var community in model.CommunityFilter) {
					var CommunityFilter = xmlDoc.CreateElement("CommunityFilter");
					CommunityFilter.InnerText = community;
					CommunityFilters.AppendChild(CommunityFilter);
				}
				root.AppendChild(CommunityFilters);

				var AnytimeWatch = xmlDoc.CreateElement("AnytimeWatch");
				AnytimeWatch.InnerText = model.AnytimeWatch.ToString();
				root.AppendChild(AnytimeWatch);

				xmlDoc.AppendChild(root);
				xmlDoc.Save(filePath);
			} catch (InvalidOperationException e) {
				MessageBox.Show(SaveErrorMessage);
				Debug.WriteLine(e.StackTrace);
			}
		}
	}

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
		public IList<String> communityFilter;
		public IList<String> CommunityFilter { get; set; }
		public Boolean AnytimeWatch { get; set; }

		public event Action<Decimal> OnChangeCallAlertTime;

		public NwhoisTimerExactData() {
			this.CallAlertTime = 1;
			this.DoComment = false;
			this.PostComment = String.Empty;
			this.PostCommand = String.Empty;
			this.AsOwner = false;
			this.CommunityFilter = new List<String>();
			this.AnytimeWatch = false;
		}
	}
}
