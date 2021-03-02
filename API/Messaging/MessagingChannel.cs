using System;
using Newtonsoft.Json.Linq;
using ScClient;

namespace Indiebackend.API.Messaging
{
	public class MessagingChannel
	{

		public event Action<JObject> OnMessage;
		public event Action OnSubscribed;
		public event Action<object> OnError;
		public event Action OnUnsubscribed;

		public string ChannelName { get; }

		private Socket.Channel _channel;

		public MessagingChannel(Socket.Channel channel)
		{
			_channel = channel;
			ChannelName = channel.GetChannelName();

			channel.Subscribe((channelName, error, data) => {
				if(error == null)
					OnSubscribed?.Invoke();
				else
					OnError?.Invoke(error);
			});

			channel.OnMessage((channelName, data) => {
				if(data is JObject)
					OnMessage?.Invoke((JObject) data);
				else
					System.Console.WriteLine("Got a non-json message " + data);
			});
		}

		public void Publish(object data) {
			_channel.Publish(data);
		}

		public void Unsubscribe() {
			_channel.Unsubscribe((channelName, error, data) => OnUnsubscribed?.Invoke());
		}

	}
}