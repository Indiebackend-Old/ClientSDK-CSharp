using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Services.Notifications.Groups
{
	public class GroupSetLeaderNotification : GroupNotification
	{

		public string NewLeader { get; }

		public GroupSetLeaderNotification(JObject data) : base("GROUP_SET_LEADER", data)
		{
			NewLeader = data["newLeader"].Value<string>();
		}
	}
}