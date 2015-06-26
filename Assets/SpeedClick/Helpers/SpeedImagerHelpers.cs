using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class SpeedImagerHelpers
{

	public static int LastRandomIndex;
	
	private static System.Random randomGen = new System.Random();

	public static bool IsOnMobile()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
		case RuntimePlatform.BlackBerryPlayer:
		case RuntimePlatform.IPhonePlayer:
		case RuntimePlatform.WP8Player:
			return true;
		default: return false;
		}
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
		