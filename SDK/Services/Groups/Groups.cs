using System.Threading.Tasks;
using Indiebackend.API;
using Indiebackend.API.Services.Groups.Requests;

namespace Indiebackend.SDK.Services
{
	public class Groups
	{
		private readonly IndiebackendAPI _api;
		private readonly string _profileToken;
		private readonly Profile _profile;
		
		public Groups(IndiebackendAPI api, Profile profile)
		{
			_api = api;
			_profileToken = profile.Token;
			_profile = profile;
		}

		public async Task<Group<TPublic, TPrivate>> Create<TPublic, TPrivate>(CreateGroupRequest rq)
		{
			var res = await _api.Groups.Create<TPublic, TPrivate>(rq, _profileToken);
			return new Group<TPublic, TPrivate>(_api, res, _profileToken, _profile.Owner.Notifications);
		}

		public async Task<Group<TPublic, TPrivate>> Get<TPublic, TPrivate>(string groupId)
		{
			var res = await _api.Groups.Get<TPublic, TPrivate>(groupId, _profileToken);
			return new Group<TPublic, TPrivate>(_api, res, _profileToken, _profile.Owner.Notifications);
		}

		public async Task<Group<TPublic, TPrivate>> Join<TPublic, TPrivate>(string groupId) {
			var res = await _api.Groups.Join<TPublic, TPrivate>(groupId, _profileToken);
			//TODO: Check for not joined (res.Joined = false)
			return new Group<TPublic, TPrivate>(_api, res.Group, _profileToken, _profile.Owner.Notifications);
		}

	}
}