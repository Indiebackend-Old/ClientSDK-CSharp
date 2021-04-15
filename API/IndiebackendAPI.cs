using System;
using System.Threading.Tasks;
using Indiebackend.API.Messaging;
using Indiebackend.API.Services;
using Indiebackend.API.Services.Groups;
using Indiebackend.API.Services.Notifications;
using Indiebackend.API.Services.Profiles;
using Indiebackend.API.Services.Stats;
using Indiebackend.API.Utils;

namespace Indiebackend.API
{
	// ReSharper disable once InconsistentNaming
	public class IndiebackendAPI
	{
		public PlayersApi Players { get; }
		public ProfilesApi Profiles { get; }
		public GroupsApi Groups { get; }
		public StatsApi Stats { get; }

		private readonly HttpUtils _http;

		public IndiebackendAPI(string appId)
		{
			_http = new HttpUtils(Constants.API_GATEWAY, appId);

			// HTTP Services initialization

			Players = new PlayersApi(_http);
			Profiles = new ProfilesApi(_http);
			Groups = new GroupsApi(_http);
			Stats = new StatsApi(_http);
		}

		public HttpUtils GetHttp()
		{
			return _http;
		}
	}
}