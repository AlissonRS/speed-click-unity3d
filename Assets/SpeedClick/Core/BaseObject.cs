using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;
using Boomlagoon.JSON;
using Assets.SpeedClick.Core;

namespace Alisson.Core
{
	public class BaseObject: MonoBehaviour
	{

		public int ID;

		public BaseObject() {}

        public virtual void DefineGameObjectName()
        {
            this.gameObject.name = String.Format("{0}_{1}", this.getTableName(), this.ID);
        }

		public virtual void ParseObject(JSONValue json)
		{
			JSONObject jsonObj = json.Obj;
			IEnumerable<FieldInfo> info = this.GetType().GetFields();
			foreach (FieldInfo field in info)
			{
				if (jsonObj.ContainsKey(field.Name))
				{
					TypeCode code = Type.GetTypeCode(field.FieldType);
					switch(code)
					{
					case TypeCode.Int16: field.SetValue(this, Convert.ToInt16(jsonObj.GetNumber(field.Name))); break;
					case TypeCode.Int32: field.SetValue(this, Convert.ToInt32(jsonObj.GetNumber(field.Name))); break;
					case TypeCode.Int64: field.SetValue(this, Convert.ToInt64(jsonObj.GetNumber(field.Name))); break;
					case TypeCode.Single: field.SetValue(this, Convert.ToSingle(jsonObj.GetNumber(field.Name))); break;
					case TypeCode.Double: field.SetValue(this, jsonObj.GetNumber(field.Name)); break;
					case TypeCode.Decimal: field.SetValue(this, Convert.ToDecimal(jsonObj.GetNumber(field.Name))); break;
					case TypeCode.Boolean: field.SetValue(this, jsonObj.GetBoolean(field.Name)); break;
					case TypeCode.DateTime: field.SetValue(this, Convert.ToDateTime(jsonObj.GetNumber(field.Name))); break;
					case TypeCode.String: field.SetValue(this, jsonObj.GetString(field.Name)); break;
                    case TypeCode.Object: this.ParseObjectField(jsonObj.GetValue(field.Name), field); break;
					default: field.SetValue(this, Convert.ChangeType(jsonObj.GetString(field.Name), field.FieldType)); break;
					}
				}
			}
            this.DefineGameObjectName();
		}

        public virtual void ParseObjectField(JSONValue json, FieldInfo field)
        {

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

    }
}

