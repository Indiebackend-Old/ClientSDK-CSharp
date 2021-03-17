using System;
using System.Threading.Tasks;
using Indiebackend.API.Utils.Extensions;
using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Messaging
{
	public class MessagingChannel
	{

		public event Action<JToken> OnMessage;
		public event Action<object> OnError;
		public event Action OnUnsubscribed;

		public string ChannelName { get; }

		private readonly MessagingApi _messagingApi;

		public MessagingChannel(MessagingApi messagingApi, string channelName)
		{
			_messagingApi = messagingApi;
			ChannelName = channelName;
		}
		
		public void Publish(object data) {
			
		}

		public async Task Unsubscribe() {
			await _messagingApi.DeleteChannel(ChannelName);
		}

		public void InvokeOnMessage(JToken obj)
		{
			OnMessage?.Invoke(obj);
		}

		public void InvokeOnError(object error)
		{
			OnError?.Invoke(error);
		}

		public void InvokeOnUnsubscribed()
		{
			OnUnsubscribed?.Invoke();
		}

	}
}