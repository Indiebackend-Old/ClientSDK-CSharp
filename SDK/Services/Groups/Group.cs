using System.Threading.Tasks;
using Indiebackend.API;
using Indiebackend.API.Services.Groups;
using Indiebackend.API.Services.Groups.Requests;
using Indiebackend.API.Services.Groups.Results;
using Indiebackend.API.Services.Notifications;
using Indiebackend.API.Utils.Extensions;

namespace Indiebackend.SDK.Services
{
	public class Group<TPublic, TPrivate> : ApiGroup<TPublic, TPrivate>
	{

		private readonly string _profileToken;
		private readonly IndiebackendAPI _api;
		private readonly NotificationsApi _notificationsApi;
		private NotificationsListener _notifications;

		public Group(IndiebackendAPI api, ApiGroup<TPublic, TPrivate> res, string profileToken, NotificationsApi notifications)
		{
			_profileToken = profileToken;
			UpdateFromApi(res);
			_api = api;
			_notificationsApi = notifications;
		}

		public async Task<Group<TPublic, TPrivate>> WithMessaging()
		{
			_notifications = await _notificationsApi.Subscribe(AppId, "group", Id);
			_notifications.OnNotification += (notification) => notification.Log($"[{Id}] [Notification]");
			return this;
		}

		public async Task<Group<TPublic, TPrivate>> SetLeader(string leaderId)
		{
			var res = await _api.Groups.SetLeader<TPublic, TPrivate>(Id, leaderId, _profileToken);
			UpdateFromApi(res.Group);
			return this;
		}

		public async Task<bool> Delete()
		{
			return await _api.Groups.Delete(Id, _profileToken);
		}

		public async Task<Group<TPublic, TPrivate>> SetPublicData(TPublic publicData)
		{
			return await SetData(publicData, PrivateData);
		}

		public async Task<Group<TPublic, TPrivate>> SetPrivateData(TPrivate privateData)
		{
			return await SetData(PublicData, privateData);
		}

		public async Task<Group<TPublic, TPrivate>> SetData(TPublic publicData, TPrivate privateData)
		{
			var res = await _api.Groups.SetData<TPublic, TPrivate>(Id, new SetGroupDataRequest<TPublic, TPrivate> {
				PrivateData = privateData,
				PublicData = publicData
			}, _profileToken);

			UpdateFromApi(res);

			return this;;
		}

		public async Task<LeaveGroupResult<TPublic, TPrivate>> Leave() {
			return await _api.Groups.Leave<TPublic, TPrivate>(Id, _profileToken);
		}

		public async Task<Group<TPublic, TPrivate>> Refresh() {
			var res = await _api.Groups.Get<TPublic, TPrivate>(Id, _profileToken);
			UpdateFromApi(res);
			return this;
		}

		private void UpdateFromApi(ApiGroup<TPublic, TPrivate> res)
		{
			Id = res.Id;
			AppId = res.AppId;
			Name = res.Name;
			Leader = res.Leader;
			Members = res.Members;
			PublicData = res.PublicData;
			PrivateData = res.PrivateData;
		}

	}
}