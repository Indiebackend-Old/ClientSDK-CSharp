using System;
using System.Collections.Generic;
using System.Linq;
using Indiebackend.API.Utils.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Structures.Errors
{
	public class BadRequestException : IndieBackendError
	{
		public (string, string)[] Fields;
		public bool HasInvalidFields;

		public BadRequestException(string body) : base(400, "Bad Request", "")
		{
			var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(body);

			Error = json["error"].ToString();

			object message = json["message"];
			if (message is JArray fields)
			{
				Fields = fields.Values<string>().Select(e =>
				{
					var split = e.Split(" ");
					return (split[0], string.Join(" ", split.Skip(1)));
				}).ToArray();
			}
			else
			{
				Message = message.ToString();
			}

			HasInvalidFields = Fields?.Length > 0;
		}
	}
}