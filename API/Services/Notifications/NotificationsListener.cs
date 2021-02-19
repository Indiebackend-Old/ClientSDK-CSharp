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

			channel.OnMessage(ParseRawData);
		}

		private void ParseRawData(string channelName, object rawData)
		{
			if (!(rawData is JObject))
				return;

			JObject payload = (JObject)rawData;
			string type = payload["type"].Value<string>();
			JObject data = (JObject)payload["data"];
			switch (type)
			{
				case "GROUP_DATA_UPDATED":
					string groupId = ((JObject)data["group"])["id"].Value<string>();
					OnNotification.Invoke(new GroupUpdatedNotification(groupId));
					break;
				default:
					OnNotification.Invoke(new BasicNotification(type));
					break;
			}
		}
	}
}