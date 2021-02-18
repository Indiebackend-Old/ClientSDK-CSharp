using System.Threading.Tasks;
using Indiebackend.API.Structures;
using Indiebackend.API.Utils;
using Indiebackend.API.Utils.Extensions;

namespace Indiebackend.API.Services
{
	public class IndiebackendService
	{

		private readonly HttpUtils _http;
		private readonly string _prefix;

		protected IndiebackendService(HttpUtils http, string prefix)
		{
			_http = http;
			_prefix = prefix;
		}

		protected Task<T> Get<T>(string url, string token = null) 
		{
			return _http.Get<T>(_prefix + url, token);
		}

		protected Task<T> Post<T>(string url, object body, string token = null)
		{
			return _http.Post<T>(_prefix + url, body, token);
		}

		protected Task<T> Delete<T>(string url, string token = null)
		{
			return _http.Delete<T>(_prefix + url, token);
		}

		protected Task<T> Delete<T>(string url, object body, string token = null)
		{
			return _http.Delete<T>(_prefix + url, body, token);
		}

		protected Task<T> Patch<T>(string url, object body, string token = null)
		{
			return _http.Patch<T>(_prefix + url, body, token);
		}
		
	}
}