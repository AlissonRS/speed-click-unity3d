using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Alisson.Core.Database;
using Alisson.Core.Database.Connections;
using Boomlagoon.JSON;
using System;
using Alisson.Core.Repository;
using Alisson.Core.Encryption;

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

		void Start()
		{
			StartCoroutine(LoadUsers());
		}

		
		IEnumerator LoadUsers()
		{
			Connection conn = ServerManager.getConn();
			yield return StartCoroutine(conn.GetAll("user"));
			if (!conn.response.Success || conn.response.DataType != JSONValueType.Array)
				yield break;
			foreach(JSONValue value in conn.response.DataArray)
			{
				User user = new User(value);
				BaseRepository<User>.add(user);
			}
		}


		public static Connection getConn(ConnectionType connType)
		{
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
				JSONObject data = getConn(ConnectionType.ServerConn).response.Data;
				User user = new User(Convert.ToInt32(data.GetNumber("ID")), data.GetString("Login"));
				LoggedUserID = user.ID;
				BaseRepository<User>.add(user);
			}
			MessageDialogManager.ShowDialog(getConn(ConnectionType.ServerConn).response.Message);
		}

	}

}
