using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Indiebackend.API.Services.Notifications;
using Indiebackend.API.Utils.Extensions;
using Newtonsoft.Json.Linq;
using SocketIOClient;

namespace Indiebackend.API.Messaging
{
	public class MessagingApi
	{

		private readonly SocketIO _socket;
		private readonly Dictionary<string, TaskCompletionSource<bool>> _taskCompletionSources;
		private readonly Dictionary<string, MessagingChannel> _messagingChannels;

		public MessagingApi(string token)
		{
			_taskCompletionSources = new Dictionary<string, TaskCompletionSource<bool>>();
			_messagingChannels = new Dictionary<string, MessagingChannel>();

			_socket = new SocketIO(Constants.MESSAGING_GATEWAY, new SocketIOOptions
			{
				EIO = 4,
				Query = new Dictionary<string, string>
				{
					{"token", token}
				}
			});

			_socket.OnDisconnected += (sender, e) => e.Log("Disconnected");
			_socket.OnReconnectFailed += (sender, e) => e.Log("ReconnectFailed");
			_socket.OnReconnecting += (sender, e) => e.Log("Reconnecting");
			_socket.OnConnected += (sender, e) => SetTaskCompleted("internal", "socketioConnectedAwaiter", true);
			_socket.OnError += (sender, e) => e.Log("Error");
			_socket.OnReceivedEvent += (sender, args) =>
			{
				JToken response = args.Response.GetValue();
				string channel = args.Event;
				switch (args.Event)
				{
					case "subscribed":
						string newChannel = response.Value<string>("newChannel");
						SetTaskCompleted("subscribe", newChannel, true);
						break;
					case "subscribe_error":
						string errorChannel = response.Value<string>("channel");
						"Error".Log(errorChannel);
						SetTaskException("subscribe", errorChannel, new Exception(response.Value<string>("error")));
						break;
					default:
						if (_messagingChannels.ContainsKey(channel))
							_messagingChannels[channel].InvokeOnMessage(response);
						break;
				}
			};
		}

		public async Task Connect()
		{
			await _socket.ConnectAsync();
			await AwaitTaskCompletion("internal", "socketioConnectedAwaiter");
		}

		public async Task<MessagingChannel> GetChannel(string channelName)
		{
			if (_messagingChannels.ContainsKey(channelName))
				return _messagingChannels[channelName];

			await _socket.EmitAsync("subscribe", channelName);

			await AwaitTaskCompletion("subscribe", channelName);

			MessagingChannel messagingChannel = new MessagingChannel(this, channelName);
			_messagingChannels.Add(channelName, messagingChannel);
			return messagingChannel;
		}

		public async Task DeleteChannel(string channelName)
		{
			await _socket.EmitAsync("unsubscribe", channelName);
			await AwaitTaskCompletion("unsubscribe", channelName);
			_messagingChannels.Remove(channelName);
		}

		private async Task AwaitTaskCompletion(string type, string name)
		{
			_taskCompletionSources.Add(type + name, new TaskCompletionSource<bool>());
			await _taskCompletionSources[type + name].Task;
			}

		private void SetTaskCompleted(string type, string name, bool result)
		{
			_taskCompletionSources[type + name].SetResult(result);
		}

		private void SetTaskException(string type, string name, Exception result)
		{
			_taskCompletionSources[type + name].SetException(result);
		}
	}
}