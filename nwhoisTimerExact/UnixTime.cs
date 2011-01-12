namespace NwhoisTimerExact {
	using System;

	internal sealed class UnixTime {
		private readonly static DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public static long ToUnixTime(DateTime dateTime) {
			if (dateTime.Kind != DateTimeKind.Utc) {
				dateTime = dateTime.ToUniversalTime();
			}
			return (long)dateTime.Subtract(UnixEpoch).TotalSeconds;
		}

		public static DateTime FromUnixTime(Int64 unixTime) {
			return UnixEpoch.AddSeconds(unixTime);
		}
	}
}
