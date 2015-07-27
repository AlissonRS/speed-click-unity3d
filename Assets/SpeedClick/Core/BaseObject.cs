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

		public BaseObject(JSONValue value)
		{
			JSONObject json = value.Obj;
			IEnumerable<FieldInfo> info = this.GetType().GetFields();
			foreach (FieldInfo field in info)
			{
				if (json.ContainsKey(field.Name))
				{
					TypeCode code = Type.GetTypeCode(field.FieldType);
					switch(code)
					{
					case TypeCode.Int16: field.SetValue(this, Convert.ToInt16(json.GetNumber(field.Name))); break;
					case TypeCode.Int32: field.SetValue(this, Convert.ToInt32(json.GetNumber(field.Name))); break;
					case TypeCode.Int64: field.SetValue(this, Convert.ToInt64(json.GetNumber(field.Name))); break;
					case TypeCode.Single: field.SetValue(this, Convert.ToSingle(json.GetNumber(field.Name))); break;
					case TypeCode.Double: field.SetValue(this, json.GetNumber(field.Name)); break;
					case TypeCode.Decimal: field.SetValue(this, Convert.ToDecimal(json.GetNumber(field.Name))); break;
					case TypeCode.Boolean: field.SetValue(this, json.GetBoolean(field.Name)); break;
					case TypeCode.DateTime: field.SetValue(this, Convert.ToDateTime(json.GetNumber(field.Name))); break;
					case TypeCode.String: field.SetValue(this, json.GetString(field.Name)); break;
					default: field.SetValue(this, Convert.ChangeType(json.GetString(field.Name), field.FieldType)); break;
					}
				}
			}
		}

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

