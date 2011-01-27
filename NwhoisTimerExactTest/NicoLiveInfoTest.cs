using nwhois.plugin.NwhoisTimerExact;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;

namespace NwhoisTimerExactTest {
	/// <summary>
	///NicoLiveInfoTest のテスト クラスです。すべての
	///NicoLiveInfoTest 単体テストをここに含めます
	///</summary>
	[TestClass()]
	public class NicoLiveInfoTest {
		/// <summary>
		///NicoLiveInfo コンストラクター のテスト
		///</summary>
		[TestMethod()]
		public void Success() {
			// Arrange
			var xmlText = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><getplayerstatus status=\"ok\" time=\"1295900669\"><stream><base_time>1295899356</base_time><open_time>1295899356</open_time><end_time>1295901162</end_time><start_time>1295899362</start_time></stream></getplayerstatus>";
			var xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xmlText);

			// Act
			var actual = new NicoLiveInfo(xmlDoc, 1500);

			// Assert
			Assert.AreEqual(new DateTime(2011, 1, 24, 20, 24, 28), actual.ServerTime);
			Assert.AreEqual(new DateTime(2011, 1, 24, 20, 02, 36), actual.BaseTime);
			Assert.AreEqual(new DateTime(2011, 1, 24, 20, 02, 36), actual.OpenTime);
			Assert.AreEqual(new DateTime(2011, 1, 24, 20, 02, 42), actual.StartTime);
			Assert.AreEqual(new DateTime(2011, 1, 24, 20, 32, 42), actual.EndTime);
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void FormatError() {
			// Arrange
			var xmlText = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><getplayerstatus status=\"ok\" time=\"1295900669\"><stream><base_time>1295899356</base_time><open_time>1295899356</open_time><end_time>1295901162</end_time></stream></getplayerstatus>";
			var xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xmlText);

			// Act
			var actual = new NicoLiveInfo(xmlDoc, 1000);
		}
	}
}
