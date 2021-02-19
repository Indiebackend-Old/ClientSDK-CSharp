using Indiebackend.API.Utils.Extensions;
using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Services.Notifications.Groups
{
	public class GroupCreatedNotification : GroupNotification
	{

		public GroupCreatedNotification(JObject data) : base("GROUP_CREATED", data)
		{
		}
	}
}