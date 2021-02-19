namespace Indiebackend.API.Services.Groups.Results
{
	public class UpdateGroupResult<TPublic, TPrivate>
	{
		public bool Updated;
		public ApiGroup<TPublic, TPrivate> Group;
	}
}