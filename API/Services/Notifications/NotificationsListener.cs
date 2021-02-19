using System;
using Indiebackend.API.Services.Notifications.Groups;
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
					OnNotification?.Invoke(new GroupUpdatedNotification(data));
					break;
				case "GROUP_CREATED":
					OnNotification?.Invoke(new GroupCreatedNotification(data));
					break;
				case "GROUP_DELETED":
					OnNotification?.Invoke(new GroupDeletedNotification(data));
					break;
				case "GROUP_LEFT":
					OnNotification?.Invoke(new GroupLeftNotification(data));
					break;
				case "GROUP_SET_LEADER":
					OnNotification?.Invoke(new GroupSetLeaderNotification(data));
					break;
				case "GROUP_JOINED":
					OnNotification?.Invoke(new GroupJoinedNotification(data));
					break;
				default:
					OnNotification?.Invoke(new BasicNotification(type));
					break;
			}
		}
	}
}