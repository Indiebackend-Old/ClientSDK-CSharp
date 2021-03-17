using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Indiebackend.API.Structures;
using Indiebackend.API.Structures.Errors;
using Indiebackend.API.Utils.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Utils
{
	public class HttpUtils
	{

		private readonly string _appId;
		private readonly string _host;
		private readonly HttpClient _client = new HttpClient();

		public HttpUtils(string host, string appId)
		{
			_host = host;
			_appId = appId;
		}

		public Task<T> Get<T>(string url, string token = null)
		{
			return Request<T>(HttpMethod.Get, url, token);
		}

		public Task<T> Post<T>(string url, object body, string token = null)
		{
			return Request<T>(HttpMethod.Post, url, token, body);
		}

		public Task<T> Delete<T>(string url, string token = null)
		{
			return Request<T>(HttpMethod.Delete, url, token);
		}

		public Task<T> Delete<T>(string url, object body, string token = null)
		{
			return Request<T>(HttpMethod.Delete, url, token, body);
		}

		public Task<T> Patch<T>(string url, object body, string token = null)
		{
			return Request<T>(HttpMethod.Patch, url, token, body);
		}

		private async Task<T> Request<T>(HttpMethod method, string url, string token, object body = null)
		{
			// (method + " - " + API_GATEWAY + url).Log("Request url");
			// if(body != null) JsonConvert.SerializeObject(body).Log("Body");

			using HttpRequestMessage request = new HttpRequestMessage(method, _host + url);
			if (body != null)
				request.Content =
					new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

			request.Headers.Add("AppId", _appId);

			if (token != null)
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

			//TODO: Use cancellation token ?
			using HttpResponseMessage response = await _client.SendAsync(request);

			await EnsureValidResponse(response);

			return DeserializeJsonFromStream<T>(await response.Content.ReadAsStreamAsync());
		}

		//Method from: https://johnthiriet.com/efficient-api-calls/
		private static T DeserializeJsonFromStream<T>(Stream stream)
		{
			if (stream == null || !stream.CanRead)
				return default;

			using StreamReader sr = new StreamReader(stream);
			using JsonTextReader jtr = new JsonTextReader(sr);
			JsonSerializer js = new JsonSerializer();
			return js.Deserialize<T>(jtr);
		}

		private static async Task EnsureValidResponse(HttpResponseMessage res)
		{
			if (res.StatusCode == HttpStatusCode.OK || res.StatusCode == HttpStatusCode.Created) return;

			string body = await res.Content.ReadAsStringAsync();

			switch (res.StatusCode)
			{
				case HttpStatusCode.NotFound:
					throw new NotFoundException(body);
				case HttpStatusCode.BadRequest:
					throw new BadRequestException(body);
				case HttpStatusCode.Forbidden:
					throw new IndieBackendError((int) res.StatusCode, "Forbidden", body);
				case HttpStatusCode.Unauthorized:
					throw new IndieBackendError((int) res.StatusCode, "Unauthorized", body);
				default:
					throw new IndieBackendError((int) res.StatusCode, "Unknown", body);
			}
		}
	}
}