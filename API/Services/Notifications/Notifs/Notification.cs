namespace Indiebackend.API.Services.Notifications
{
	public abstract class Notification
	{
		public string Type { get; }

		protected Notification(string type)
		{
			Type = type;
		}
	}
}