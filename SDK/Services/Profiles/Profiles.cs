using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indiebackend.API;
using Indiebackend.API.Utils.Extensions;

namespace Indiebackend.SDK.Services
{
	public class Profiles
	{

		private string _playerToken;
		private IndiebackendAPI _api;

		public Profiles(IndiebackendAPI api, string token)
		{
			_playerToken = token;
			_api = api;
		}

		public async Task<Profile> Create(string name, string displayName = null, string avatarUrl = null) {
			var res = await _api.Profiles.Create(new API.Services.Profiles.Requests.CreateProfileRequest {
				Name = name,
				DisplayName = displayName,
				AvatarUrl = avatarUrl
			}, _playerToken);

			return new Profile(_api, _playerToken, res);
		}

		public async Task<List<Profile>> List()
		{
			var res = await _api.Profiles.List(_playerToken);
			return res.Select(e => new Profile(_api, _playerToken, e)).ToList();
		}

		public async Task<Profile> Get(string profileId) {
			var res = await _api.Profiles.Get(profileId, _playerToken);
			return new Profile(_api, _playerToken, res);
		}

	}
}