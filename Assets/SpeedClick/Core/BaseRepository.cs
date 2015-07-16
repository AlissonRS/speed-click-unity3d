using Alisson.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Alisson.Core.Database;
using UnityEngine;
using Alisson.Core.Database.Connections;

namespace Alisson.Core.Repository
{

    static public class ConcreteList
    {
        static public List<Type> concrete = new List<Type>();
    }

    public abstract class BaseRepository
    {
        public abstract void Clean();
    }

    public class BaseRepository<T>: BaseRepository where T : BaseObject
    {

		protected static List<T> list = new List<T>();

        static BaseRepository()
        {
            ConcreteList.concrete.Add(typeof(BaseRepository<T>));
        }

        public static T add(T obj)
        {
            if (list.FindIndex(o => o.ID == obj.ID) == -1)
                list.Add(obj);
            return obj;
        }

        public override void Clean()
        {
            list.Clear();
        }

        public static int Count(Func<T, bool> predicate)
        {
            if (list.Count == 0)
                getAll();
            return list.Count(predicate);
        }

        public static IEnumerable<T> getAll()
        {
            if (list.Count == 0)
              list = getAllFresh();
            return list;
        }

        public static IEnumerable<T> getAll(Func<T, bool> predicate)
        {
            if (list.Count == 0)
                list = getAllFresh();
            return list.Where(predicate);
        }

        protected static List<T> getAllFresh()
        {
			return ServerManager.getConn().GetAll<T>();
        }

        public static T getByID(int ID)
        {
            return getAll(o => o.ID == ID).First();
        }

        public static T First(Func<T, bool> predicate)
        {
            if (list.Count == 0)
                getAll();
            return list.First(predicate);
        }

        public static void Submit(IEnumerable<T> lst)
        {
            foreach (T o in lst)
                add(o);
        }

        public static void Submit()
        {
            Submit(list);
        }

    }
}