using Newtonsoft.Json;

namespace Indiebackend.API.Services.Players.Requests
{
	public class LoginPlayerEmailRequest
	{
		[JsonProperty("email")] public string Email;
		[JsonProperty("password")] public string Password;
	}
}