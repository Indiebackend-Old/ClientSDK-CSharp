using Indiebackend.API.Structures;

namespace Indiebackend.API.Services.Players.Results
{
	public class LoginPlayerResult : IApiResult
	{
		public string Token;
		public ApiPlayer Player;
	}
}