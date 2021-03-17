using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Services.Notifications.Groups
{
	public class GroupJoinedNotification : GroupNotification
	{

		public string JoinedBy { get; }

		public GroupJoinedNotification(JToken data) : base("GROUP_JOINED", data)
		{
			JoinedBy = data.Value<string>("joinedBy");
		}
	}
}