using UnityEngine;
using System.Collections;

namespace Alisson.Core.Database.Connections
{

	public class LocalConnection : Connection
	{
		#region implemented abstract members of Connection

		public override IEnumerator GetAll(string model)
		{
			yield break;
		}

		#endregion



	}

}