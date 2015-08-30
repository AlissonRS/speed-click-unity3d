using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using Boomlagoon.JSON;

namespace Alisson.Core.Database.Connections
{

	public class ServerConnection: Connection
	{

		protected string controller = "";
		protected string method = "";

        public override IEnumerator LoadImageIntoTexture(string file)
        {
            string url = String.Concat(SpeedClickHelpers.GetImagesURL(), file);
            Texture2D texture = new Texture2D(1, 1, TextureFormat.DXT5Crunched, false);
            WWW www = new WWW(url);
            yield return www;

            www.LoadImageIntoTexture(texture);

            this.response = new ResponseData();
            if (!String.IsNullOrEmpty(www.error))
            {
                this.response.Message = "Não foi possível acessar a imagem!";
                yield break;
            }
            if (www.size <= 2)
                yield break;
            else
            {
                //SpeedClickHelpers.MakeTextureMultipleOfFour(texture);
                this.response.DownloadedSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                this.response.Message = "";
                this.response.Success = true;
            }

        }

		public override IEnumerator SendRequest(string controller, HttpMethodType t, Dictionary<string, object> p)
		{
            string url = String.Concat(SpeedClickHelpers.GetApiURL(), controller);
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
					url +=  SpeedClickHelpers.BuildURLParam(p);
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