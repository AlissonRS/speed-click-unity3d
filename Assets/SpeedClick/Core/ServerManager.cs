using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Alisson.Core.Database;
using Alisson.Core.Database.Connections;
using Boomlagoon.JSON;
using System;
using Alisson.Core.Repository;
using Alisson.Core.Encryption;
using Assets.SpeedClick.Core;

namespace Alisson.Core
{

	public class ServerManager: MonoBehaviour {

		public ServerConnection serverConn;
		public LocalConnection localConn;

		public static int LoggedUserID = 0;

		public static ConnectionType ConnectionType;

        private static List<Connection> conns = new List<Connection>();

		void Awake()
		{
			ConnectionType = ConnectionType.ServerConn;
			conns.Add(serverConn);
			conns.Add(localConn);
		}

        IEnumerator Start()
        {
            yield return StartCoroutine(BaseRepository.getAllFresh<User>());
            foreach (User user in BaseRepository.getAll<User>())
                Debug.Log(user.Login);
        }

		public static Connection getConn(ConnectionType connType)
        {
            if (!SpeedImagerHelpers.IsInternetConnectionAvailable())
                return conns[(int)ConnectionType.LocalConn];
			return conns[(int)connType];
		}

		public static Connection getConn()
		{
			return getConn(ConnectionType);
		}

		public IEnumerator Login(string login, string password, HttpMethodType type)
		{
			string encrypted = StringCipher.Encrypt(password,StringCipher.SecretMessage);
			Dictionary<string,object> p = new Dictionary<string, object>(){
				{"login", login},
				{"password", encrypted}
			};
			yield return StartCoroutine(getConn(ConnectionType.ServerConn).SendRequest("user", type, p));
			if (getConn(ConnectionType.ServerConn).response.Success)
            {
                User user = new User();
                JSONValue value = getConn(ConnectionType.ServerConn).response.Data;
                JSONArray scores = value.Obj.GetArray("Scores");
                foreach (JSONValue item in scores)
                    BaseRepository.add<Score>(item);
                ServerManager.LoggedUserID = BaseRepository.add<User>(value).ID;
            }
			MessageDialogManager.ShowDialog(getConn(ConnectionType.ServerConn).response.Message);
		}

	}

}
