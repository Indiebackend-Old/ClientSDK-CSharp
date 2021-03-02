using Indiebackend.API.Messaging;
using Indiebackend.API.Utils;
using Indiebackend.API.Utils.Extensions;
using ScClient;
using SuperSocket.ClientEngine;

namespace Indiebackend.API.Services.Notifications
{
	public class NotificationsAPI : IndiebackendService
	{

		private MessagingAPI _messagingAPI;

		public NotificationsAPI(HttpUtils http, MessagingAPI messagingAPI) : base(http, "/notifications")
		{
			_messagingAPI = messagingAPI;
		}

		public NotificationsListener Subscribe(string channelName) {
			var channel = _messagingAPI.GetChannel(channelName);
			return new NotificationsListener(channel, channelName);
		}

	}
}