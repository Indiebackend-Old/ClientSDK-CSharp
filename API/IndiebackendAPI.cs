using System;
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
		public PlayersApi Players { get; }
		public ProfilesApi Profiles { get; }
		public GroupsApi Groups { get; }

		public NotificationsAPI Notifications { get; }

		public IndiebackendAPI(string appId)
		{
			HttpUtils http = new HttpUtils(appId);

			// Services initialization

			Players = new PlayersApi(http);
			Profiles = new ProfilesApi(http);
			Groups = new GroupsApi(http);
		}
	}
}