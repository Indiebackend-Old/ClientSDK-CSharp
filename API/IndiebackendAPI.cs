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
		
		public PlayersApi Players { get; }
		public ProfilesApi Profiles { get; }
		public GroupsApi Groups { get; }

		private readonly HttpUtils http;

		public IndiebackendAPI(string appId)
		{
			http = new HttpUtils(Constants.API_GATEWAY, appId);

			// HTTP Services initialization

			Players = new PlayersApi(http);
			Profiles = new ProfilesApi(http);
			Groups = new GroupsApi(http);

		}

		public HttpUtils GetHttp()
		{
			return http;
		}
		
	}
}