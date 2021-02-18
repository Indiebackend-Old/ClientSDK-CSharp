using Newtonsoft.Json;

namespace Indiebackend.API.Services.Groups.Requests
{
	public class SetGroupDataRequest<TPublic, TPrivate>
	{

		[JsonProperty("publicData")] public TPublic PublicData;
		[JsonProperty("privateData")] public TPrivate PrivateData;

	}
}