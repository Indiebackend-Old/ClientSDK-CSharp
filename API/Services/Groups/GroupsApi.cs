using System.Threading.Tasks;
using Indiebackend.API.Services.Groups.Requests;
using Indiebackend.API.Services.Groups.Results;
using Indiebackend.API.Utils;

namespace Indiebackend.API.Services.Groups
{
	public class GroupsApi : IndiebackendService
	{
		public GroupsApi(HttpUtils http) : base(http, "/groups")
		{
		}

		public Task<ApiGroup<TPublic, TPrivate>> Create<TPublic, TPrivate>(CreateGroupRequest rq, string profileToken)
		{
			return Post<ApiGroup<TPublic, TPrivate>>("/", rq, profileToken);
		}

		public Task<ApiGroup<TPublic, TPrivate>> Get<TPublic, TPrivate>(string groupId, string profileToken)
		{
			return Get<ApiGroup<TPublic, TPrivate>>("/" + groupId, profileToken);
		}

		public async Task<bool> Delete(string groupId, string profileToken)
		{
			DeleteGroupResult res = await Delete<DeleteGroupResult>("/" + groupId, profileToken);
			return res.Deleted;
		}

		public Task<UpdateGroupResult<TPublic, TPrivate>> Join<TPublic, TPrivate>(string groupId, string profileToken)
		{
			return Patch<UpdateGroupResult<TPublic, TPrivate>>($"/{groupId}/join", null, profileToken);
		}

		public Task<ApiGroup<TPublic, TPrivate>> SetData<TPublic, TPrivate>(string groupId, SetGroupDataRequest<TPublic, TPrivate> rq, string profileToken)
		{
			return Patch<ApiGroup<TPublic, TPrivate>>($"/{groupId}/data", rq, profileToken);
		}

		public Task<UpdateGroupResult<TPublic, TPrivate>> SetLeader<TPublic, TPrivate>(string groupId, string leaderId, string profileToken)
		{
			return Patch<UpdateGroupResult<TPublic, TPrivate>>($"/{groupId}/leader?leader={leaderId}", null, profileToken);
		}

		public Task<LeaveGroupResult<TPublic, TPrivate>> Leave<TPublic, TPrivate>(string groupId, string profileToken)
		{
			return Patch<LeaveGroupResult<TPublic, TPrivate>>($"/{groupId}/leave", null, profileToken);
		}

	}
}