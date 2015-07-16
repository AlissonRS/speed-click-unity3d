using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Boomlagoon.JSON;

namespace Alisson.Core.Database
{
	public class Connection
	{
		
		public JSONObject data = new JSONObject();

		public virtual List<T> GetAll<T>() where T: BaseObject
		{
			throw new NotImplementedException("");
		}

		public virtual IEnumerator GetData(string controller, Dictionary<string, string> p)
		{
			throw new NotImplementedException("");
		}

		public virtual IEnumerator PostData(string controller, Dictionary<string, object> p)
		{
			throw new NotImplementedException("");
		}


	}
}
