using Indiebackend.API.Services.Notifications;
using Indiebackend.API.Utils.Extensions;
using ScClient;

namespace Indiebackend.API.Messaging
{
	public class MessagingAPI
	{

		private Socket _socket;

		public MessagingAPI(string host)
		{
			host.Log();
			_socket = new Socket(host);
			_socket.SetListerner(new SocketListener());

			_socket.SetReconnectStrategy(new ReconnectStrategy().SetMaxAttempts(30));
			_socket.Connect();
		}

		public MessagingChannel GetChannel(string channelName) {
			return new MessagingChannel(_socket.CreateChannel(channelName));
		}

	}
}