using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Services.Notifications.Groups
{
	public class GroupDeletedNotification : GroupNotification
	{

		public GroupDeletedNotification(JToken data) : base("GROUP_DELETED", data)
		{
		}
	}
}