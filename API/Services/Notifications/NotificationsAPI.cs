using System.Threading.Tasks;
using Indiebackend.API.Messaging;
using Indiebackend.API.Utils;

namespace Indiebackend.API.Services.Notifications
{
	public class NotificationsApi : IndiebackendService
	{

		private readonly MessagingApi _messagingApi;

		public NotificationsApi(HttpUtils http, MessagingApi messagingApi) : base(http, "/notifications")
		{
			_messagingApi = messagingApi;
		}

		public async Task<NotificationsListener> Subscribe(string appId, string type, string id)
		{
			string channelName = $"{appId}/{type}/{id}";
			return new NotificationsListener(await _messagingApi.GetChannel(channelName));
		}

		public async Task GetPersistantNotifications(string token)
		{
			
		}
		
	}
}