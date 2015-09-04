using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Alisson.Core.Database;
using Alisson.Core.Database.Connections;
using Boomlagoon.JSON;
using System;
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

        public IEnumerator LoadImageIntoSprite(string url, Action<Sprite> callback = null)
        {
            if (url.IsNullOrWhiteSpace())
                yield break;
            yield return StartCoroutine(getConn(ConnectionType.ServerConn).LoadImageIntoTexture(url));
            ResponseData response = getConn(ConnectionType.ServerConn).response;
            if (response.Success && callback != null)
                callback(response.DownloadedSprite);
        }

		public IEnumerator Login(string login, string password, HttpMethodType type)
		{
			Dictionary<string,object> p = new Dictionary<string, object>(){
				{"login", login},
				{"password", password}
			};
			yield return StartCoroutine(getConn(ConnectionType.ServerConn).SendRequest("user", type, p));
            ResponseData response = getConn(ConnectionType.ServerConn).response;
			if (response.Success)
            {
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
                score.ID = Convert.ToInt32(value.Obj.GetNumber("ID"));
                score.Ranking = Convert.ToInt32(value.Obj.GetNumber("Ranking"));
                if (value.Obj.GetValue("Player") != null)
                    score.GetUser().ParseObject(value.Obj.GetValue("Player"));
            }
            else
                MessageDialogManager.ShowDialog(response.Message);
        }

        public IEnumerator LoadScores(Scene scene)
        {
            string url = String.Format("scene/{0}/scores/top/{1}", scene.ID, 10);
            yield return StartCoroutine(getConn(ConnectionType.ServerConn).SendRequest(url, HttpMethodType.Get, null));

            ResponseData response = getConn(ConnectionType.ServerConn).response;
            if (!response.Success || response.DataType != JSONValueType.Array)
                yield break;
            foreach (JSONValue value in response.DataArray)
               BaseRepository.add<Score>(value);
        }
    }

}
