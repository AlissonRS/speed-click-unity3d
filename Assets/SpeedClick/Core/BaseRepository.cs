using Alisson.Core;
using Alisson.Core.Database;
using Boomlagoon.JSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.SpeedClick.Core
{

    public class BaseRepository : MonoBehaviour
    {

        public static BaseRepository instance;

        void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public static T add<T>() where T: BaseObject
        {
            return add<T>(null);
        }

        public static T add<T>(JSONValue json) where T : BaseObject
        {
            T obj = null;
            Type t = typeof(T);
            if (json != null)
            {
                int ID = Convert.ToInt32(json.Obj.GetNumber("ID"));
                if (ID > 0)
                    obj = getById<T>(ID);
            }
            if (obj == null)
                obj = new GameObject().AddComponent<T>();
            if (json != null)
                obj.ParseObject(json);
            if (obj.transform.parent != null)
                return obj;
            string repoName = t.Name + "Repository";
            Transform repo = instance.gameObject.transform.FindChild(repoName);
            if (repo == null)
            {
                repo = new GameObject(repoName).transform;
                repo.parent = instance.transform;
            }
            obj.transform.parent = repo;
            return obj;
        }

        public static void Clear<T>() where T : BaseObject
        {
            Type t = typeof(T);
            string repoName = t.Name + "Repository";
            Transform repo = instance.gameObject.transform.FindChild(repoName);
            if (repo != null)
                repo.ClearChildren();
        }

        public static void ClearAll()
        {
            instance.transform.ClearChildren();
        }

        public static IEnumerable<T> getAll<T>() where T : BaseObject
        {
            Type t = typeof(T);
            string repoName = t.Name + "Repository";
            Transform repo = instance.gameObject.transform.FindChild(repoName);
            if (repo != null)
                return repo.GetComponentsInChildren<T>();
            return new List<T>();
        }

        public static IEnumerator getAllFresh<T>() where T : BaseObject
        {
            Type t = typeof(T);
            Connection conn = ServerManager.getConn();
            yield return instance.StartCoroutine(conn.GetAll(t.Name));
            if (!conn.response.Success || conn.response.DataType != JSONValueType.Array)
                yield break;
            foreach (JSONValue value in conn.response.DataArray)
                add<T>(value);
        }
        public static T getById<T>(int ID) where T : BaseObject
        {
            IEnumerable<T> objs = getAll<T>().Where<T>(u => u.ID == ID);
            if (objs.Count() == 1)
                return objs.First();
            return null;
        }
    }

}
