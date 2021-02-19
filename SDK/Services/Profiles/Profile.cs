using System.Threading.Tasks;
using Indiebackend.API;
using Indiebackend.API.Services.Notifications;
using Indiebackend.API.Services.Profiles;
using Indiebackend.API.Utils.Extensions;

namespace Indiebackend.SDK.Services
{
	public class Profile : ApiProfile
	{

		public Groups Groups { get; private set; }

		private IndiebackendAPI _api;
		private string _playerToken;
		private NotificationsListener _notifications;

		public Profile(IndiebackendAPI api, string playerToken, ApiProfile profile)
		{
			_api = api;
			_playerToken = playerToken;
			UpdateFromAPIResult(profile);
			_notifications = _api.Notifications.Subscribe(Id);
			_notifications.OnNotification += (notification) => notification.Log("[Notification]");
		}

		public async Task<Profile> Refresh()
		{
			ApiProfile res = await _api.Profiles.Get(Id, _playerToken);
			UpdateFromAPIResult(res, Token);
			return this;
		}

		public async Task<Profile> Use()
		{
			var res = await _api.Profiles.Use(Id, _playerToken);
			UpdateFromAPIResult(res.Profile, res.Token);
			return this;
		}

		public async Task<bool> Delete()
		{
			var res = await _api.Profiles.Delete(Id, _playerToken);
			return res.Deleted;
		}

		private void UpdateFromAPIResult(ApiProfile profile, string token = null)
		{
			Id = profile.Id;
			AppId = profile.AppId;
			Name = profile.Name;
			Owner = profile.Owner;
			DisplayName = profile.DisplayName;
			AvatarUrl = profile.AvatarUrl;
			CreatedAt = profile.CreatedAt;
			UpdatedAt = profile.UpdatedAt;
			Token = token ?? Token;
			Groups = new Groups(_api, Token);
		}

	}
}