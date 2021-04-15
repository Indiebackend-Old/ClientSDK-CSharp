using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indiebackend.API;
using Indiebackend.API.Messaging;
using Indiebackend.API.Utils.Extensions;

namespace Indiebackend.SDK.Services
{
	public class Profiles
	{
		private readonly IndiebackendAPI _api;
		private readonly Player _player;

		public Profiles(IndiebackendAPI api, Player playerApi)
		{
			_api = api;
			_player = playerApi;
		}

		public async Task<Profile> Create(string name, string displayName = null, string avatarUrl = null) {
			var res = await _api.Profiles.Create(new API.Services.Profiles.Requests.CreateProfileRequest {
				Name = name,
				DisplayName = displayName,
				AvatarUrl = avatarUrl
			}, _player.Token);

			return new Profile(_api, res, _player);
		}

		public async Task<List<Profile>> List()
		{
			var res = await _api.Profiles.List(_player.Token);
			return res.Select(e => new Profile(_api, e, _player)).ToList();
		}

		public async Task<Profile> Get(string profileId) {
			var res = await _api.Profiles.Get(profileId, _player.Token);
			return new Profile(_api, res, _player);
		}

	}
}