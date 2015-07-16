using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;
using Boomlagoon.JSON;

namespace Alisson.Core
{
	abstract public class BaseObject
	{

		public int ID;

		public BaseObject() {}

		public virtual string getTableName()
		{
			return this.GetType().Name;
		}

		public JSONObject ToJson()
		{
			JSONObject obj = new JSONObject();
			Type t = this.GetType();
			FieldInfo[] fields = t.GetFields(BindingFlags.Public|BindingFlags.Instance);
			foreach(FieldInfo field in fields)
			{
				if (field.GetValue(this) != null)
					obj.Add(field.Name, SpeedImagerHelpers.BuildJSONValue(field.GetValue(this)));
			}
			return obj;
		}

		public T UnderlyingObject<T>() where T : BaseObject
		{
			return (T) this;
		}
	}
}

