using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Boomlagoon.JSON;
using Alisson.Core;

namespace Alisson.Core.Database
{
	public abstract class Connection: MonoBehaviour
	{
		
		public ResponseData response = new ResponseData();

        public virtual IEnumerator SendRequest(string controller, HttpMethodType t, Dictionary<string, object> p)
		{
			yield break;
		}

        public abstract IEnumerator GetAll(string model);

	}
}
