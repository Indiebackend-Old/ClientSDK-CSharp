using System.Threading.Tasks;
using Indiebackend.API;
using Indiebackend.API.Services.Stats.Requests;
using Indiebackend.API.Services.Stats.Results;

namespace Indiebackend.SDK.Services
{
	public class Stats
	{
		
		private readonly IndiebackendAPI _api;
		private readonly string _profileToken;
		private readonly Profile _profile;
		public Stats(IndiebackendAPI api, Profile profile)
		{
			_api = api;
			_profile = profile;
			_profileToken = profile.Token;
		}

		public Task<GetStatsResult> Get()
		{
			return _api.Stats.GetProfile(_profileToken);
		}

		public Task<GetStatsResult> Set(EditStatsRequest rq)
		{
			return _api.Stats.SetProfile(rq, _profileToken);
		}

		public Task<GetStatsResult> Update(EditStatsRequest rq)
		{
			return _api.Stats.UpdateProfile(rq, _profileToken);
		}
		
	}
}