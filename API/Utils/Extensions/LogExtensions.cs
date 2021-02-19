using System;
using System.Numerics;
using Indiebackend.API.Services;
using Indiebackend.API.Services.Groups;
using Indiebackend.API.Services.Profiles;
using Newtonsoft.Json;

namespace Indiebackend.API.Utils.Extensions
{
	public static class LogExtensions
	{
		private const string PREFIX = "[Indiebackend]";

		public static void Log(this object value, string name = null)
		{
			Print(JsonConvert.SerializeObject(value, Formatting.Indented), name);
		}

		public static void Log(this string value, string name = null)
		{
			Print(value, name);
		}

		public static void Log(this int value, string name = null)
		{
			Print(value, name);
		}

		public static void Log(this float value, string name = null)
		{
			Print(value, name);
		}

		public static void Log(this long value, string name = null)
		{
			Print(value, name);
		}

		public static void Log(this bool value, string name = null)
		{
			Print(value, name);
		}

		public static void Log(this string[] value, string name = null)
		{
			Print(string.Join(",", value), name);
		}

		private static void Print(object value, string name)
		{
			if(string.IsNullOrEmpty(name))
			{
				Console.WriteLine($"{PREFIX} {value}");
				return;
			}

			Console.WriteLine($"{PREFIX} {name} :>> {value}");
		}

	}
}