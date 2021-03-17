using System;
using System.Threading.Tasks;
using Indiebackend.API.Messaging;
using Indiebackend.API.Services;
using Indiebackend.API.Services.Groups;
using Indiebackend.API.Services.Notifications;
using Indiebackend.API.Services.Profiles;
using Indiebackend.API.Utils;

namespace Indiebackend.API
{
	// ReSharper disable once InconsistentNaming
	public class IndiebackendAPI
	{

		// private const string API_GATEWAY = "http://dev.api.indiebackend.com";
		// private const string SOCKETCLUSTER_HOST = "ws://dev.api.indiebackend.com/messaging/socketcluster/";

		private const string API_GATEWAY = "http://localhost:3000";


		public PlayersApi Players { get; }
		public ProfilesApi Profiles { get; }
		public GroupsApi Groups { get; }
		public NotificationsApi Notifications { get; private set; }
		public MessagingApi Messaging { get; private set; }

		public IndiebackendAPI(string appId)
		{
			HttpUtils http = new HttpUtils(API_GATEWAY, appId);

			// HTTP Services initialization

			Players = new PlayersApi(http);
			Profiles = new ProfilesApi(http);
			Groups = new GroupsApi(http);

		}

	}
}