using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Services.Notifications.Groups
{
	public class GroupLeftNotification : GroupNotification
	{

		public string LeftBy { get; }
		public bool Deleted { get; }

		public GroupLeftNotification(JObject data) : base("GROUP_LEFT", data)
		{
			LeftBy = data["leftBy"].Value<string>();
			Deleted = data["deleted"].Value<bool>();
		}
	}
}