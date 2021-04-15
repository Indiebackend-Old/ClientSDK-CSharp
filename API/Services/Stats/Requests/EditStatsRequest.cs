using Newtonsoft.Json;

namespace Indiebackend.API.Services.Stats.Requests
{
	public class EditStatsRequest
	{

		[JsonProperty("privateStats")] public object PrivateStats;
		[JsonProperty("publicStats")] public object PublicStats;

	}
}