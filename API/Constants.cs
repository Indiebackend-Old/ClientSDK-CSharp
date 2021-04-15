namespace Indiebackend.API
{
	internal static class Constants
	{
#if DEBUG
		public const string MESSAGING_GATEWAY = "ws://localhost:8000";
		public const string API_GATEWAY = "http://localhost:3000";
#endif
#if TEST
		public const string MESSAGING_GATEWAY = "ws://messaging.api.dev.indiebackend.com";
		public const string API_GATEWAY = "http://clients.api.dev.indiebackend.com";
#endif
#if RELEASE
		public const string MESSAGING_GATEWAY = "ws://messaging.api.prod.indiebackend.com";
		public const string API_GATEWAY = "http://clients.api.prod.indiebackend.com";
#endif
	}
}