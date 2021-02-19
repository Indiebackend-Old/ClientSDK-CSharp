using System;
using Indiebackend.API.Utils.Extensions;
using Newtonsoft.Json.Linq;
using ScClient;

namespace Indiebackend.API.Services.Notifications
{
	public class NotificationsListener
	{

		public event Action<Notification> OnNotification;

		private Socket.Channel channel;
		private string channelName;

		public NotificationsListener(Socket.Channel channel, string channelName)
		{
			this.channel = channel;
			this.channelName = channelName;

			channel.Subscribe();

			channel.OnMessage((channelName, data) =>
			{
				if (!(data is JObject))
					return;

				switch ()
				{

					default:
				}

					data.Log($"[Notification - {channelName}]");

				OnNotification?.Invoke(new Notification());
			});
		}

	}

}