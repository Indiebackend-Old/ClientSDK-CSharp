using System.Threading.Tasks;
using Indiebackend.API.Services.Profiles.Requests;
using Indiebackend.API.Services.Profiles.Results;
using Indiebackend.API.Utils;

namespace Indiebackend.API.Services.Profiles
{
	public class ProfilesApi : IndiebackendService
	{
		public ProfilesApi(HttpUtils http) : base(http, "/profiles")
		{
		}

		public Task<ApiProfile> Create(CreateProfileRequest rq, string playerToken)
		{
			return Post<ApiProfile>("/", rq, playerToken);
		}

		public Task<ApiProfile[]> List(string playerToken)
		{
			return Get<ApiProfile[]>("/", playerToken);
		}

		public Task<UseProfileResult> Use(string id, string playerToken)
		{
			return Get<UseProfileResult>("/use?id=" + id, playerToken);
		}

		public Task<ApiProfile> Get(string id, string playerToken)
		{
			return Get<ApiProfile>("/" + id, playerToken);
		}

		public Task<DeleteProfileResult> Delete(string id, string playerToken)
		{
			return Delete<DeleteProfileResult>("/" + id, playerToken);
		}

	}
}