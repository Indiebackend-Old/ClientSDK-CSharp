using System.Threading.Tasks;
using System.Linq;
using Indiebackend.API.Services.Players.Requests;
using Indiebackend.API.Structures;
using Indiebackend.API.Structures.Errors;
using Indiebackend.API.Utils.Extensions;
using Indiebackend.SDK.Services;
using Indiebackend.API.Services.Groups.Requests;

namespace Test.SDK
{
	public class TestSDK
	{
		private const string APP_ID = "exampleapp-zq8rq";

		private static Indiebackend.SDK.Indiebackend _api;

		public static async Task Test() {
			_api = new Indiebackend.SDK.Indiebackend(APP_ID);

			try
			{

				"Registering player...".Log();

				Player player = await _api.Players.LoginWithEmail(new LoginPlayerEmailRequest {
					Email = "julien.lavocat@gmail.com",
					Password = "220100Jl!",
				});

				var profile = await (await player.Profiles.List()).First().Use();
				await profile.Use();

				Group<object, object> group = await profile.Groups.Get<object, object>("c22ThIHM1DVXJHkHeaah");

				group.Log();

				(await group.SetPublicData("This is where the fun begin !")).PublicData.Log();
				(await group.SetPrivateData("I am your father")).PrivateData.Log();

				group.Log();

				(await group.SetData("Oh no !", "Anyway")).Log();
			}
			catch (IndieBackendError e)
			{
				if (e is BadRequestException exception)
				{
					if (exception.HasInvalidFields)
						$"[{exception.Error}] - {exception.Fields.First()}".Log();
					else
						$"[{exception.Error}] - {exception.Message}".Log();

					return;
				}

				$"[{e.Error}] - (HTTP Code: {e.StatusCode}) {e.Message}".Log();
			}
		}

	}
}