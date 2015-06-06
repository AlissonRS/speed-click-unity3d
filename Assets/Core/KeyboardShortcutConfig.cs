using System;
using UnityEngine;

public class KeyboardShortcutConfig
{

	public static KeyCode Image1 = KeyCode.F; 
	public static KeyCode Image2 = KeyCode.G; 
	public static KeyCode Image3 = KeyCode.H; 
	public static KeyCode Image4 = KeyCode.J; 
	public static KeyCode Image5 = KeyCode.C; 
	public static KeyCode Image6 = KeyCode.V; 
	public static KeyCode Image7 = KeyCode.N; 
	public static KeyCode Image8 = KeyCode.M; 
	
	public static string GetImageTag(KeyCode key)
	{
		if (key == Image1) return "1";
		if (key == Image2) return "2";
		if (key == Image3) return "3";
		if (key == Image4) return "4";
		if (key == Image5) return "5";
		if (key == Image6) return "6";
		if (key == Image7) return "7";
		if (key == Image8) return "8";
		return "";
	}

}


