using System;
using Newtonsoft.Json;

namespace Indiebackend.API.Services.Players.Requests
{
	public class RegisterPlayerEmailRequest
	{
		[JsonProperty("name")] public string Name;

		[JsonProperty("email")] public string Email;

		[JsonProperty("password")] public string Password;

		[JsonProperty("displayName")] public string DisplayName;

		[JsonProperty("birthDate")] public DateTime BirthDate;

		[JsonProperty("avatarUrl")] public string AvatarUrl;
	}
}