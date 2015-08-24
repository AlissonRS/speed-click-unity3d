using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Alisson.Core.Database;
using Alisson.Core.Database.Connections;
using Boomlagoon.JSON;
using System;
using Alisson.Core.Encryption;
using Assets.SpeedClick.Core;

namespace Alisson.Core
{

	public class ServerManager: MonoBehaviour {

        public static ServerManager instance;
		public ServerConnection serverConn;
		public LocalConnection localConn;

		public static int LoggedUserID = 0;

		public static ConnectionType ConnectionType;

        private static List<Connection> conns = new List<Connection>();

		void Awake()
		{
            if (instance == null)
                instance = this;
			ConnectionType = ConnectionType.ServerConn;
			conns.Add(serverConn);
			conns.Add(localConn);
		}

		public static Connection getConn(ConnectionType connType)
        {
            if (!SpeedClickHelpers.IsInternetConnectionAvailable())
                return conns[(int)ConnectionType.LocalConn];
			return conns[(int)connType];
		}

		public static Connection getConn()
		{
			return getConn(ConnectionType);
		}

        public IEnumerator LoadImageIntoSprite(ISpritable spritable, string url)
        {
            yield return StartCoroutine(getConn(ConnectionType.ServerConn).LoadImageIntoTexture(url));
            ResponseData response = getConn(ConnectionType.ServerConn).response;
			if (response.Success)
                spritable.LoadSprite(response.DownloadedSprite);
        }

		public IEnumerator Login(string login, string password, HttpMethodType type)
		{
			string encrypted = StringCipher.Encrypt(password,StringCipher.SecretMessage);
			Dictionary<string,object> p = new Dictionary<string, object>(){
				{"login", login},
				{"password", encrypted}
			};
			yield return StartCoroutine(getConn(ConnectionType.ServerConn).SendRequest("user", type, p));
            ResponseData response = getConn(ConnectionType.ServerConn).response;
			if (response.Success)
            {
                User user = new User();
                JSONValue value = response.Data;
                ServerManager.LoggedUserID = BaseRepository.add<User>(value).ID;
            }
			MessageDialogManager.ShowDialog(response.Message);
		}


        public IEnumerator SendScore(Score score)
        {
            Dictionary<string, object> p = new Dictionary<string, object>(){
				{"ID", -1},
				{"Accuracy", score.Accuracy},
                {"MaxCombo", score.MaxCombo},
                {"MissCount", score.MissCount},
                {"Platform", score.Platform},
                {"PlayerId", score.PlayerId},
                {"Points", score.Points},
                {"Ranking", 0},
                {"SceneId", score.SceneId},
                {"Speed", score.Speed},
                {"TurnCount", score.TurnCount}
			};
            yield return StartCoroutine(getConn(ConnectionType.ServerConn).SendRequest("score", HttpMethodType.Post, p));

            ResponseData response = getConn(ConnectionType.ServerConn).response;
            if (response.Success)
            {
                JSONValue value = response.Data;
                if (value.Obj.GetBoolean("IsNewRecord"))
                {
                    score.ID = Convert.ToInt32(value.Obj.GetNumber("ID"));
                    score.Ranking = Convert.ToInt32(value.Obj.GetNumber("Ranking"));
                }
            }
            else
                MessageDialogManager.ShowDialog(response.Message);
        }
    }

}
