using System.Collections.Generic;
using Newtonsoft.Json;

namespace Indiebackend.API.Structures.Errors
{
	public class NotFoundException : IndieBackendError
	{
		public NotFoundException(string body) : base(404, "Not Found", "")
		{
			var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(body);
			
			Message = json["message"];
			Error = json["error"];
		}
	}
}