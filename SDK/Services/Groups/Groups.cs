using System.Threading.Tasks;
using Indiebackend.API;
using Indiebackend.API.Services.Groups.Requests;

namespace Indiebackend.SDK.Services
{
	public class Groups
	{
		private IndiebackendAPI _api;
		private string _profileToken;

		public Groups(IndiebackendAPI api, string profileToken)
		{
			_api = api;
			_profileToken = profileToken;
		}

		public async Task<Group<TPublic, TPrivate>> Create<TPublic, TPrivate>(CreateGroupRequest rq)
		{
			var res = await _api.Groups.Create<TPublic, TPrivate>(rq, _profileToken);
			return new Group<TPublic, TPrivate>(_api, res, _profileToken);
		}

		public async Task<Group<TPublic, TPrivate>> Get<TPublic, TPrivate>(string groupId)
		{
			var res = await _api.Groups.Get<TPublic, TPrivate>(groupId, _profileToken);
			return new Group<TPublic, TPrivate>(_api, res, _profileToken);
		}

		public async Task<Group<TPublic, TPrivate>> Join<TPublic, TPrivate>(string groupId) {
			var res = await _api.Groups.Join<TPublic, TPrivate>(groupId, _profileToken);
			//TODO: Check for not joined (res.Joined = false)
			return new Group<TPublic, TPrivate>(_api, res.Group, _profileToken);
		}

	}
}