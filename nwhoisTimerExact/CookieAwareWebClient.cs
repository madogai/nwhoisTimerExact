namespace NwhoisTimerExact {
	using System;
	using System.Net;

	internal sealed class CookieAwareWebClient : WebClient {
		private CookieContainer m_container;

		public CookieAwareWebClient(CookieContainer container) {
			this.m_container = container;
		}

		protected override WebRequest GetWebRequest(Uri address) {
			WebRequest request = base.GetWebRequest(address);
			if (request is HttpWebRequest) {
				(request as HttpWebRequest).CookieContainer = m_container;
			}
			return request;
		}

	}
}
