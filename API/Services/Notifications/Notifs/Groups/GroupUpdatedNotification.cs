using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Services.Notifications.Groups
{
	public class GroupUpdatedNotification : GroupNotification
	{

		public GroupUpdatedNotification(JObject data) : base("GROUP_UPDATED", data)
		{
		}
	}
}