using Newtonsoft.Json;

namespace Indiebackend.API.Services.Profiles.Requests
{
	public class CreateProfileRequest
	{
		[JsonProperty("name")] public string Name;
		[JsonProperty("displayName")] public string DisplayName;
		[JsonProperty("avatarUrl")] public string AvatarUrl;
	}
}