namespace Indiebackend.API.Services.Notifications
{
	public class GroupUpdatedNotification : Notification
	{

		public string GroupId { get; }

		public GroupUpdatedNotification(string groupId) : base("GROUP_UPDATED")
		{
			GroupId = groupId;
		}
	}
}