using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Services.Notifications.Groups
{
	public class GroupDeletedNotification : GroupNotification
	{

		public GroupDeletedNotification(JObject data) : base("GROUP_DELETED", data)
		{
		}
	}
}