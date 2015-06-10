using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class SpeedImagerHelpers
{

	public static int LastRandomIndex;
	
	private static System.Random randomGen = new System.Random();

	public static T GetInstance<T>() where T: UnityEngine.Object
	{
		return (T) GameObject.FindObjectOfType(typeof(T));
	}

	public static T GetRandom<T>(IList<T> list, IList<T> exclude = null)
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
		
