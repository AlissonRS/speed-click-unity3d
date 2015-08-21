using UnityEngine;
using System.Collections;

namespace Alisson.Core.Database.Connections
{

	public class LocalConnection : Connection
	{

		void Start()
		{
            //BaseRepository<User>.add(new User() { ID = 1, Login = "Alisson" });
		}

		public override IEnumerator GetAll(string model)
		{
            //this.response = new ResponseData();				
            //this.response.Data = json.GetObject("Data");
            //this.response.DataArray = json.GetArray("Data");
            //this.response.Message = json.GetString("Message");
            //this.response.Success = json.GetBoolean("Success");
			yield break;
		}

	}

}