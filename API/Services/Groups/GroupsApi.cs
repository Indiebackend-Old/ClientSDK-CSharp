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

		public Task<ApiGroup<TPublic, TPrivate>> Get<TPublic, TPrivate>(string id, string profileToken)
		{
			return Get<ApiGroup<TPublic, TPrivate>>("/" + id, profileToken);
		}

		public async Task<bool> Delete(string id, string profileToken)
		{
			DeleteGroupResult res = await Delete<DeleteGroupResult>("/" + id, profileToken);
			return res.Deleted;
		}

		public Task<JoinGroupResult<TPublic, TPrivate>> Join<TPublic, TPrivate>(string id, string profileToken)
		{
			return Patch<JoinGroupResult<TPublic, TPrivate>>($"/{id}/join", null, profileToken);
		}

		public Task<ApiGroup<TPublic, TPrivate>> SetData<TPublic, TPrivate>(string id, SetGroupDataRequest<TPublic, TPrivate> rq, string profileToken)
		{
			return Patch<ApiGroup<TPublic, TPrivate>>($"/{id}/data", rq, profileToken);
		}
		
	}
}