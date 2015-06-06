using System;
using UnityEngine;

public static class SpeedImageHelpers
{
	
	public static T GetInstance<T>() where T: UnityEngine.Object
	{
		return (T) GameObject.FindObjectOfType(typeof(T));
	}

}
		
