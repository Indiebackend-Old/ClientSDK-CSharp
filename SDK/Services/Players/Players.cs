using System.Threading.Tasks;
using Indiebackend.API;
using Indiebackend.API.Services;
using Indiebackend.API.Services.Players.Requests;

namespace Indiebackend.SDK.Services
{
	public class Players
	{

		public Profiles Profiles;

		private IndiebackendAPI _api;

		public Players(IndiebackendAPI api)
		{
			_api = api;
		}

		public async Task<Player> RegisterWithEmail(RegisterPlayerEmailRequest rq)
		{
			var res = await _api.Players.Register(rq);
			return new Player(_api, res.Player);
		}

		public async Task<Player> LoginWithEmail(LoginPlayerEmailRequest rq) {
			var res = await _api.Players.Login(rq);
			return new Player(_api, res.Player, res.Token);
		}

	}
}