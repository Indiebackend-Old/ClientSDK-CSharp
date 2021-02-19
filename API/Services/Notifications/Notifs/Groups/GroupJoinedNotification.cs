using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Services.Notifications.Groups
{
	public class GroupJoinedNotification : GroupNotification
	{

		public string JoinedBy { get; }

		public GroupJoinedNotification(JObject data) : base("GROUP_JOINED", data)
		{
			JoinedBy = data["joinedBy"].Value<string>();
		}
	}
}