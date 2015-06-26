using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Alisson.Core
{

	public class ServerManager : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}
		
		public List<T> GetAll<T>() where T: BaseObject
		{
			return new List<T>();
		}
		
		public List<T> GetAll<T>(Dictionary<string ,object> where) where T: BaseObject
		{
			return new List<T>();
		}

		public BaseObject Get(BaseObject obj)
		{
			return null;
		}

		public void Save(BaseObject obj)
		{

		}

	}

}
