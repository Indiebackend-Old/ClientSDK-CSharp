using System;
using System.Threading.Tasks;
using Indiebackend.API;
using Indiebackend.API.Utils.Extensions;
using Indiebackend.SDK.Services;

namespace Indiebackend.SDK
{
	public class Indiebackend
	{

		public Players Players { get; }

		private IndiebackendAPI _api;

		public Indiebackend(string appId)
		{
			_api = new IndiebackendAPI(appId);

			Players = new Players(_api);
		}

	}
}