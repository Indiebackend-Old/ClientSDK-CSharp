using System;
using System.Threading.Tasks;
using Indiebackend.API;
using Indiebackend.API.Services;
using Indiebackend.API.Services.Players.Results;
using Indiebackend.API.Utils.Extensions;

namespace Indiebackend.SDK.Services
{
	public class Player : API.Services.ApiPlayer
	{

		public bool IsLoggedIn { get; private set; }
		public Profiles Profiles { get; private set; }

		private IndiebackendAPI _api;

		public Player(IndiebackendAPI api, API.Services.ApiPlayer player, string token = null)
		{
			_api = api;
			UpdateFromAPIResult(player, token);
		}

		public async Task<Player> Refresh()
		{
			GetPlayerResult res = await _api.Players.Get(Token);
			UpdateFromAPIResult(res.Player, Token);
			return this;
		}

		private void UpdateFromAPIResult(API.Services.ApiPlayer player, string token = null)
		{
			Id = player.Id;
			AppId = player.AppId;
			Name = player.Name;
			DisplayName = player.DisplayName;
			Email = player.Email;
			BirthDate = player.BirthDate;
			AvatarUrl = player.AvatarUrl;
			IsVerified = player.IsVerified;
			CreatedAt = player.CreatedAt;
			UpdatedAt = player.UpdatedAt;
			Token = token ?? Token;
			IsLoggedIn = !string.IsNullOrEmpty(token);
			Profiles = IsLoggedIn ? new Profiles(_api, token) : null;
		}

	}
}