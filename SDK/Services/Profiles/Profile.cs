using System.Threading.Tasks;
using Indiebackend.API;
using Indiebackend.API.Services.Notifications;
using Indiebackend.API.Services.Profiles;
using Indiebackend.API.Services.Profiles.Results;
using Indiebackend.API.Utils.Extensions;

namespace Indiebackend.SDK.Services
{
	public class Profile : ApiProfile
	{

		public Player Owner { get; private set; }
		public Groups Groups { get; private set; }
		public Stats Stats { get; private set; }

		private readonly IndiebackendAPI _api;
		private readonly string _playerToken;
		private NotificationsListener _notifications;

		public Profile(IndiebackendAPI api, ApiProfile profile, Player player)
		{
			_api = api;
			_playerToken = player.Token;
			Owner = player;
			UpdateFromApiResult(profile);
		}

		public async Task<Profile> WithMessaging()
		{
			_notifications = await Owner.Notifications.Subscribe(AppId, "profile", Id);
			_notifications.OnNotification += (notification) => notification.Log("[Notification]");
			return this;
		}

		public async Task<Profile> Refresh()
		{
			ApiProfile res = await _api.Profiles.Get(Id, _playerToken);
			UpdateFromApiResult(res, Token);
			return this;
		}

		public async Task<Profile> Use()
		{
			UseProfileResult res = await _api.Profiles.Use(Id, _playerToken);
			UpdateFromApiResult(res.Profile, res.Token);
			return this;
		}

		public async Task<bool> Delete()
		{
			DeleteProfileResult res = await _api.Profiles.Delete(Id, _playerToken);
			return res.Deleted;
		}

		private void UpdateFromApiResult(ApiProfile profile, string token = null)
		{
			Id = profile.Id;
			AppId = profile.AppId;
			Name = profile.Name;
			OwnerId = profile.OwnerId;
			DisplayName = profile.DisplayName;
			AvatarUrl = profile.AvatarUrl;
			CreatedAt = profile.CreatedAt;
			UpdatedAt = profile.UpdatedAt;
			Token = token ?? Token;
			Groups = new Groups(_api, this);
			Stats = new Stats(_api, this);
		}

	}
}