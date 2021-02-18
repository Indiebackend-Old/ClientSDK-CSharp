using Newtonsoft.Json;

namespace Indiebackend.API.Services.Profiles
{
	public class ApiProfile
	{
		public string Id { get; set; }
		public string AppId { get; set; }
		[JsonProperty("owner")] public string Owner { get; set; }
		public string Name { get; set; }
		public string DisplayName { get; set; }
		public string AvatarUrl { get; set; }
		public string CreatedAt { get; set; }
		public string UpdatedAt { get; set; }
		public string Token;
	}
}