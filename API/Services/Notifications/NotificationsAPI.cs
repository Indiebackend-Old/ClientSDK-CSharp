using Indiebackend.API.Utils;
using Indiebackend.API.Utils.Extensions;
using ScClient;
using SuperSocket.ClientEngine;

namespace Indiebackend.API.Services.Notifications
{
	public class NotificationsAPI : IndiebackendService
	{

		private Socket _socket;

		public NotificationsAPI(HttpUtils http, string host) : base(http, "/notifications")
		{
			// Technically this service has no use for http... but maybe in the future ?
			host.Log();
			_socket = new Socket(host);
			_socket.SetListerner(new SocketListener());

			_socket.SetReconnectStrategy(new ReconnectStrategy().SetMaxAttempts(30));
			_socket.Connect();
		}

		public NotificationsListener Subscribe(string channelName) {
			var channel = _socket.CreateChannel(channelName);
			return new NotificationsListener(channel, channelName);
		}

	}
}