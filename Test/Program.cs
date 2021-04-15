using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indiebackend.API;
using Indiebackend.API.Services.Groups;
using Indiebackend.API.Services.Groups.Requests;
using Indiebackend.API.Services.Players.Requests;
using Indiebackend.API.Services.Stats.Requests;
using Indiebackend.API.Services.Stats.Results;
using Indiebackend.API.Structures;
using Indiebackend.API.Structures.Errors;
using Indiebackend.API.Utils.Extensions;
using Newtonsoft.Json.Linq;
using Test.SDK;

namespace Test
{
	internal static class Program
	{
		private const string APP_ID = "exampleapp-hczt5";

		private static IndiebackendAPI _api;

		private static async Task Main(string[] args)
		{
			//await TestSdk.Test();
			_api = new IndiebackendAPI(APP_ID);

			try
			{
				"Login player".Log();
				string playerToken = (await _api.Players.Login(new LoginPlayerEmailRequest()
				{
					Email = "julien.lavocat@gmail.com",
					Password = "220100Jl!"
				})).Token;

				"Getting player profile".Log();

				string profileToken = (await _api.Profiles.Use((await _api.Profiles.List(playerToken))[0].Id,
					playerToken)).Token;

				//GetStatsResult stats = await _api.Stats.GetProfile(profileToken);

				(await _api.Stats.SetProfile(new SetStatsRequest
				{
					PrivateStats = new Dictionary<string, object>
					{
						{"test", true},
						{"private", 1}
					},
					PublicStats = new Dictionary<string, object>
					{
						{"test", false},
						{"private", 0}
					}
				}, profileToken)).Log();
				
				

				// 	await TestGroup(_api, profileToken);
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

		private static async Task TestGroup(IndiebackendAPI api, string profileToken)
		{
			"Testing groups".Log();

			GroupsApi groups = api.Groups;

			var group = await groups.Create<string, string[]>(new CreateGroupRequest()
			{
				PrivateData = "test private data",
				PublicData = new[] {"test", "test 2", "test 3"}
			}, profileToken);

			// group.PrivateData.Log($"Private data ({group.PrivateData.GetType()})");
			// group.PublicData.Log($"Public data ({group.PublicData.GetType()})");

			(await groups.Get<string, string[]>(group.Id, profileToken)).Log();

			(await groups.Join<string, string[]>(group.Id, profileToken)).Updated.Log("Joined ?");

			(await groups.SetData<int, int>(group.Id, new SetGroupDataRequest<int, int>
			{
				PrivateData = 1,
				PublicData = 11
			}, profileToken)).Log();

			(await groups.Delete(group.Id, profileToken)).Log("Deleted ?");
		}
	}
}