namespace Indiebackend.API.Services.Groups
{
	public class ApiGroup<TPublic, TPrivate>
	{
		public string Id;
		public string Name;
		public string Leader;
		public string[] Members;
		public TPublic PublicData;
		public TPrivate PrivateData;
	}
}