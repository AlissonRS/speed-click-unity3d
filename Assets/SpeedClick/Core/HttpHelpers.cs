using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;

namespace Alisson.Core
{

	public class ResponseData
	{
		
		public JSONObject Data;
		public JSONArray DataArray;
		public JSONValueType DataType;
		public string Message = "";
		public bool Success = false;
		
	}

}

