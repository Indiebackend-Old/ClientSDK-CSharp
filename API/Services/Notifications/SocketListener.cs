using Indiebackend.API.Utils.Extensions;
using ScClient;
using SuperSocket.ClientEngine;

namespace Indiebackend.API.Services.Notifications
{
	class SocketListener : IBasicListener
	{

		private const string LOG_PREFIX = "NotificationsAPI";

		public void OnConnected(Socket socket)
		{
			"Connected to notifications API".Log(LOG_PREFIX);
		}

		public void OnDisconnected(Socket socket)
		{
			"Disconnected from notifications API".Log(LOG_PREFIX);
		}

		public void OnConnectError(Socket socket, ErrorEventArgs e)
		{
			"An error occured while connecting to Notifications API".Log(LOG_PREFIX);
		}

		public void OnAuthentication(Socket socket, bool status)
		{
			$"Authenticated state changed to: {status}".Log(LOG_PREFIX);
		}

		public void OnSetAuthToken(string token, Socket socket)
		{
			"Auth token set".Log(LOG_PREFIX);
		}
	}
}