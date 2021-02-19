using Indiebackend.API.Utils.Extensions;
using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Services.Notifications.Groups
{
	public class GroupNotification : Notification
	{

		public string GroupId { get; }

		public GroupNotification(string type, JObject data) : base(type)
		{
			GroupId = ((JObject)data["group"])["id"].Value<string>();
		}
	}
}