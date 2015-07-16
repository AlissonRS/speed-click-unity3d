using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Alisson.Core.Database;
using Alisson.Core.Database.Connections;
using Boomlagoon.JSON;
using System;
using Alisson.Core.Repository;

namespace Alisson.Core
{

	public class ServerManager: MonoBehaviour {

		public static int LoggedUserID = 0;

		public static Connections ConnectionType;

		private static List<Connection> conns = new List<Connection>();

		void Start()
		{
			ConnectionType = Connections.ServerConn;
			conns.Add(new ServerConnection());
			conns.Add(new LocalConnection());
		}

		public static Connection getConn(Connections connType)
		{
			return conns[(int)connType];
		}

		public static Connection getConn()
		{
			return getConn(ConnectionType);
		}

		public IEnumerator Login(string login, string password, string secretMessage)
		{
			Dictionary<string,string> p = new Dictionary<string, string>(){
				{"login", login},
				{"password", password},
				{"secretMessage", secretMessage}};
			yield return StartCoroutine(getConn(Connections.ServerConn).GetData("user", p));
			JSONObject data = getConn(Connections.ServerConn).data;
			if (data != null && data.ContainsKey("ID"))
			{
				User user = new User(Convert.ToInt32(data.GetNumber("ID")), data.GetString("Login"));
				LoggedUserID = user.ID;
				BaseRepository<User>.add(user);
			}
		}
		
		public IEnumerator RegisterUser(string login, string password, string secretMessage)
		{
			Dictionary<string,object> p = new Dictionary<string, object>(){
				{"login", login},
				{"password", password},
				{"secretMessage", secretMessage}};
			yield return StartCoroutine(getConn(Connections.ServerConn).PostData("user", p));
			JSONObject data = getConn(Connections.ServerConn).data;
			if (data != null)
			{
				User user = new User(Convert.ToInt32(data.GetNumber("ID")), data.GetString("Login"));
				LoggedUserID = user.ID;
				BaseRepository<User>.add(user);
			}
		}

	}

}
