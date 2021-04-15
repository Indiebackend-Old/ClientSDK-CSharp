using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Services.Stats.Results
{
	public class GetStatsResult
	{
		[JsonProperty("publicStats")] public JObject PublicStats;
		[JsonProperty("privateStats")] public JObject PrivateStats;
	}
}