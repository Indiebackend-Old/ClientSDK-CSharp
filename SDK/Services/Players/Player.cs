using System;
using System.Threading.Tasks;
using Indiebackend.API;
using Indiebackend.API.Messaging;
using Indiebackend.API.Services.Notifications;
using Indiebackend.API.Services.Players.Results;
using Indiebackend.API.Utils.Extensions;

namespace Indiebackend.SDK.Services
{
	public class Player : API.Services.ApiPlayer
	{

		public bool IsLoggedIn { get; private set; }
		public Profiles Profiles { get; private set; }
		
		public NotificationsApi Notifications { get; private set; }

		private readonly IndiebackendAPI _api;
		private MessagingApi _messaging;
		private NotificationsListener _playerNotifications;

		public Player(IndiebackendAPI api, API.Services.ApiPlayer player, string token = null)
		{
			_api = api;
			UpdateFromApiResult(player, token);
		}

		public async Task<Player> Refresh()
		{
			GetPlayerResult res = await _api.Players.Get(Token);
			UpdateFromApiResult(res.Player, Token);
			return this;
		}

		public async Task<Player> WithMessaging()
		{
			if (string.IsNullOrEmpty(Token))
				throw new Exception("Can't invoke Player.WithMessaging() without being connected");

			_messaging = new MessagingApi(Token);
			
			await _messaging.Connect();
			
			// Subscribe to player channel
			Notifications = new NotificationsApi(null, _messaging);
			_playerNotifications = await Notifications.Subscribe(AppId, "player", Id);
			return this;
		}

		private void UpdateFromApiResult(API.Services.ApiPlayer player, string token = null)
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
			Profiles = IsLoggedIn ? new Profiles(_api, token, this) : null;
		}
		
	}
}