using System.Threading.Tasks;
using Indiebackend.API.Services.Stats.Requests;
using Indiebackend.API.Services.Stats.Results;
using Indiebackend.API.Utils;
using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Services.Stats
{
	public class StatsApi : IndiebackendService
	{
		internal StatsApi(HttpUtils http) : base(http, "/stats")
		{
		}

		public Task<GetStatsResult> GetProfile(string token)
		{
			return Get<GetStatsResult>("/profile", token);
		}

		public Task<GetStatsResult> SetProfile(SetStatsRequest rq, string token)
		{
			return Post<GetStatsResult>("/profile", rq, token);
		}
		
	}
}