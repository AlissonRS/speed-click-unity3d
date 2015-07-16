using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Boomlagoon.JSON;


namespace Alisson.Core.Extensions
{

	public static class SpeedClickExtensions
	{

		public static bool IsNullOrWhiteSpace(this string str)
		{
			return String.IsNullOrEmpty(str) || str.Trim().Length == 0;
		}

		public static JSONObject ToJson(this Dictionary<string, object> d)
		{
			JSONObject obj = new JSONObject();
			foreach(KeyValuePair<string, object> pair in d)
				obj.Add(pair.Key, SpeedImagerHelpers.BuildJSONValue(pair.Value));
			return obj;
		}

	}

}