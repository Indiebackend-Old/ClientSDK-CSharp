using Indiebackend.API.Utils.Extensions;
using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Services.Notifications.Groups
{
	public class GroupNotification : Notification
	{

		public string GroupId { get; }

		protected GroupNotification(string type, JToken data) : base(type)
		{
			GroupId = data["group"]?.Value<string>("id");
		}
	}
}