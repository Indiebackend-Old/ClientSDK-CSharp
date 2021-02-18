using System.Threading.Tasks;
using Indiebackend.API.Services.Players.Requests;
using Indiebackend.API.Services.Players.Results;
using Indiebackend.API.Utils;
using Indiebackend.API.Utils.Extensions;

namespace Indiebackend.API.Services
{
	public class PlayersApi : IndiebackendService
	{
		public PlayersApi(HttpUtils http) : base(http, "/players")
		{
		}

		public Task<RegisterPlayerResult> Register(RegisterPlayerEmailRequest rq)
		{
			return Post<RegisterPlayerResult>("/register", rq);
		}

		public Task<LoginPlayerResult> Login(LoginPlayerEmailRequest rq)
		{
			return Post<LoginPlayerResult>("/login", rq);
		}

		public Task<GetPlayerResult> Get(string playerToken)
		{
			return Get<GetPlayerResult>("/", playerToken);
		}
	}
}