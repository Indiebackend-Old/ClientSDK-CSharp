using Newtonsoft.Json;

namespace Indiebackend.API.Services.Groups.Requests
{
	public class CreateGroupRequest
	{
		[JsonProperty("name")] public string Name;
		[JsonProperty("members")] public string[] Members;
		[JsonProperty("privateData")] public object PublicData;
		[JsonProperty("publicData")] public object PrivateData;
	}
}