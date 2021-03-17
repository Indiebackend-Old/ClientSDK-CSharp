using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Services.Notifications.Groups
{
	public class GroupLeftNotification : GroupNotification
	{

		public string LeftBy { get; }
		public bool Deleted { get; }

		public GroupLeftNotification(JToken data) : base("GROUP_LEFT", data)
		{
			LeftBy = data.Value<string>("leftBy");
			Deleted = data.Value<bool>("deleted");
		}
	}
}