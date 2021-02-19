namespace Indiebackend.API.Services.Groups.Results
{
	public class LeaveGroupResult<TPublic, TPrivate>
	{
		public ApiGroup<TPublic, TPrivate> Group;
		public bool Deleted;
		public string LeftBy;
	}
}