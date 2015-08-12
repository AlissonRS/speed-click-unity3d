using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using Boomlagoon.JSON;
using Alisson.Core.Repository;

namespace Alisson.Core.Database.Connections
{

	public class ServerConnection: Connection
	{

		protected string host = "http://52.25.19.44/api/";
		protected string controller = "";
		protected string method = "";

		public override IEnumerator SendRequest(string controller, HttpMethodType t, Dictionary<string, object> p)
		{
			string url = host + controller;
            WWW www = null;
			if (p != null)
			{
				if (t == HttpMethodType.Post)
                {
                    WWWForm form = new WWWForm();
					foreach(KeyValuePair<string, object> pair in p)
                        form.AddField(pair.Key, pair.Value.ToString());
                    www = new WWW(url, form);
				}
				else if (t == HttpMethodType.Get)
				{
					url += "?";
					url +=  SpeedImagerHelpers.BuildURLParam(p);
				}
			}
            if (www == null)
                www = new WWW(url);
			yield return www;

			this.response = new ResponseData();
			if (!String.IsNullOrEmpty(www.error))
			{
				this.response.Message = www.text;
				yield break;
			}
			if (www.size <= 2)
				yield break;
			else
			{
				JSONObject json = JSONObject.Parse(www.text);
                this.response.Data = json.GetValue("Data");
                this.response.DataType = this.response.Data.Type;
				this.response.DataArray = json.GetArray("Data");
				this.response.Message = json.GetString("Message");
				this.response.Success = json.GetBoolean("Success");
			}
		}

		public override IEnumerator GetAll(string model)
		{
			yield return StartCoroutine(this.SendRequest(model, HttpMethodType.Get, null));
		}

	}

}