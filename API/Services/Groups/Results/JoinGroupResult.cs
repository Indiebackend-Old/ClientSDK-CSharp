namespace Indiebackend.API.Services.Groups.Results
{
	public class JoinGroupResult<TPublic, TPrivate>
	{
		public bool Updated;
		public ApiGroup<TPublic, TPrivate> Group;
	}
}