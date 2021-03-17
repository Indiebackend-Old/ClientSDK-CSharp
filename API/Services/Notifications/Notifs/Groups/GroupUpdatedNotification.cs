using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Services.Notifications.Groups
{
	public class GroupUpdatedNotification : GroupNotification
	{

		public GroupUpdatedNotification(JToken data) : base("GROUP_UPDATED", data)
		{
		}
	}
}