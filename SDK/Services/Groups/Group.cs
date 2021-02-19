using System.Threading.Tasks;
using Indiebackend.API;
using Indiebackend.API.Services.Groups;
using Indiebackend.API.Services.Groups.Requests;
using Indiebackend.API.Services.Groups.Results;

namespace Indiebackend.SDK.Services
{
	public class Group<TPublic, TPrivate> : ApiGroup<TPublic, TPrivate>
	{

		private string _profileToken;
		private IndiebackendAPI _api;

		public Group(IndiebackendAPI api, ApiGroup<TPublic, TPrivate> res, string profileToken)
		{
			_profileToken = profileToken;
			UpdateFromAPI(res);
			_api = api;
		}

		public async Task<Group<TPublic, TPrivate>> SetLeader(string leaderId)
		{
			var res = await _api.Groups.SetLeader<TPublic, TPrivate>(Id, leaderId, _profileToken);
			UpdateFromAPI(res.Group);
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

			UpdateFromAPI(res);

			return this;;
		}

		public async Task<LeaveGroupResult<TPublic, TPrivate>> Leave() {
			return await _api.Groups.Leave<TPublic, TPrivate>(Id, _profileToken);
		}

		public async Task<Group<TPublic, TPrivate>> Refresh() {
			var res = await _api.Groups.Get<TPublic, TPrivate>(Id, _profileToken);
			UpdateFromAPI(res);
			return this;
		}

		private void UpdateFromAPI(ApiGroup<TPublic, TPrivate> res)
		{
			Id = res.Id;
			Name = res.Name;
			Leader = res.Leader;
			Members = res.Members;
			PublicData = res.PublicData;
			PrivateData = res.PrivateData;
		}

	}
}