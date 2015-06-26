using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;

namespace Alisson.Core
{
	abstract public class BaseObject
	{

		public int ID {	get; set; }

		public BaseObject() {}

		public virtual string getTableName()
		{
			return this.GetType().Name;
		}

		protected void populateProperties(Dictionary<string, object> attr)
		{
//			IEnumerable<PropertyInfo> info = this.GetType().GetProperties();
//			foreach (PropertyInfo prop in info)
//			{
//				string key = prop.Name;
//				if (attr.ContainsKey(key))
//				    prop.SetValue(this, Convert.ChangeType(attr[key], prop.PropertyType));
//			}
		}

		public T UnderlyingObject<T>() where T : BaseObject
		{
			return (T) this;
		}
	}
}

