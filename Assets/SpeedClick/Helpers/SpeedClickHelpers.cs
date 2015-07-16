using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Boomlagoon.JSON;

public static class SpeedImagerHelpers
{

	public static int LastRandomIndex;

	private static int ModalID;
	
	private static System.Random randomGen = new System.Random();

	public static void DisplayModal(string text)
	{
		//
	}

	public static JSONValue BuildJSONValue(object value)
	{
		switch (Type.GetTypeCode(value.GetType()))
		{
			case TypeCode.Int16: return new JSONValue(Convert.ToInt16(value));
			case TypeCode.Int32: return new JSONValue(Convert.ToInt32(value));
			case TypeCode.Int64: return new JSONValue(Convert.ToInt64(value));
			case TypeCode.Single: return new JSONValue(Convert.ToSingle(value));
			case TypeCode.Double: return new JSONValue(Convert.ToDouble(value));
			case TypeCode.Decimal: return new JSONValue(Convert.ToDouble(value));
			case TypeCode.Boolean: return new JSONValue(Convert.ToBoolean(value));
			case TypeCode.DateTime: return new JSONValue(Convert.ToDouble(value));
			case TypeCode.String: return new JSONValue(Convert.ToString(value));
			default: return new JSONValue(Convert.ToString(value));
		}
	}

	public static string BuildURLParam(Dictionary<string, string> p)
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string,string> pair in p)
		{
			if (pair.Key != "secretMessage")
				list.Add(pair.Key + "=" + WWW.EscapeURL(pair.Value));
			else
				list.Add(pair.Key + "=" + pair.Value);
		}
		return String.Join("&", list.ToArray());
	}

	public static T GetInstance<T>() where T: UnityEngine.Object
	{
		return (T) GameObject.FindObjectOfType(typeof(T));
	}

	public static T GetRandom<T>(IList<T> list)
	{
		IList<T> range = new List<T>(list);
		if (range.Count() == 0)
			throw new InvalidOperationException("Could not get Random Object from the passed list, because it's empty! ");
		LastRandomIndex = randomGen.Next(0, range.Count());
		return range[LastRandomIndex];
	}
	
	public static T GetRandom<T>(IList<T> list, IList<T> exclude)
	{
		IList<T> range = new List<T>(list);
		if (exclude != null)
		{
			foreach (T item in exclude)
				range.Remove(item);
		}
		if (range.Count() == 0)
			throw new InvalidOperationException("Could not get Random Object from the passed list, because it's empty! ");
		LastRandomIndex = randomGen.Next(0, range.Count());
		return range[LastRandomIndex];
	}

}
		
