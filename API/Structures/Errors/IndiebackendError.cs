using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Indiebackend.API.Structures
{
	[JsonConverter(typeof(IndieBackendErrorConverter))]
	public class IndieBackendError : Exception
	{
		public int StatusCode;
		public string Error;
		public new string Message;
		public IErrorDetails Details;

		public IndieBackendError(int statusCode, string error, string message, IErrorDetails details = null)
		{
			StatusCode = statusCode;
			Error = error;
			Message = message;
			Details = details;
		}
		
	}

	public class IndieBackendErrorConverter : JsonConverter
	{

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return string.Empty;
			if (reader.TokenType == JsonToken.String) return serializer.Deserialize(reader, objectType);

			JObject obj = JObject.Load(reader);

			IndieBackendError err = new IndieBackendError(-100, null, null);

			if (obj["status"] != null)
				err.Error = obj["status"].ToString();
			if (obj["message"] != null)
				err.Message = obj["message"].ToString();

			if (obj["details"] == null)
			{
				err.Details = null;
				return err;
			}

			if (err.Message != "Invalid payload") return err;
				
			JArray arr = (JArray)obj["details"];

			err.Details = new InvalidPayload()
			{
				Field = arr[0]["field"]?.ToString(),
				Error = arr[0]["error"]?.ToString(),
			};

			return err;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
		public override bool CanConvert(Type objectType)
		{
			throw new NotImplementedException();
		}
	}

	public interface IErrorDetails { }

	public class InvalidPayload : IErrorDetails {
		public string Field;
		public string Error;
	}

}