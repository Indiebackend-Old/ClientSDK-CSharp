using System;
using Indiebackend.API.Messaging;
using Indiebackend.API.Services.Notifications.Groups;
using Indiebackend.API.Utils.Extensions;
using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Services.Notifications
{
	public class NotificationsListener
	{
		public event Action<Notification> OnNotification;
		public string ChannelName => _channel.ChannelName;

		private readonly MessagingChannel _channel;

		public NotificationsListener(MessagingChannel channel)
		{
			_channel = channel;
			$"Created notifications listener".Log(channel.ChannelName);
			channel.OnMessage += ParseRawData;
			channel.OnError += (err) => System.Console.WriteLine(err);
		}

		private void ParseRawData(JToken payload)
		{
			string type = payload.Value<string>("type");
			JToken data = payload["data"];
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