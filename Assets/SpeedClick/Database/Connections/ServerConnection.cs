using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using Boomlagoon.JSON;
using Alisson.Core.Extensions;

namespace Alisson.Core.Database.Connections
{

	public class ServerConnection : Connection
	{

		protected string host = "http://52.25.19.44/api/";
		protected string controller = "";
		protected string method = "";

		public override IEnumerator GetData(string controller, Dictionary<string, string> p)
		{
			string url = host + controller;
			if (p != null)
			{
				url += "?";
				url +=  SpeedImagerHelpers.BuildURLParam(p);
			}
			WWW www = new WWW(url);
			yield return www;

			if (www.size <= 2)
				yield return null;
			else
			{
				data = JSONObject.Parse(www.text);
			}
		}

		public override IEnumerator PostData(string controller, Dictionary<string, object> p)
		{
			string url = host + controller;
			JSONObject dat = p.ToJson();

			WWWForm form = new WWWForm();
			form.AddField("data", dat.ToString());

			WWW www = new WWW(url, form);
			yield return www;
		}

		
		public override List<T> GetAll<T>()
		{
			Type t = typeof(T);
			this.GetData(t.Name, null);
			List<T> list = new List<T>();
			return list;
		}

		public Color GetColor(string obj, string key)
		{
			JSONObject jsonObj = data.GetObject(obj);
			JSONObject jsonColor = jsonObj.GetObject(key);

			float r = (float)(Convert.ToDecimal(jsonColor.GetNumber("a")));
			return new Color();

		}

	}

}