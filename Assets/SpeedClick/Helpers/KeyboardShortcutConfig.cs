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
	public static KeyCode MouseKey = KeyCode.X;
	
	public static GameJoystickButtons GetGameJoystickButton(KeyCode key)
	{
		if (key == Image1) return GameJoystickButtons.Image1;
		if (key == Image2) return GameJoystickButtons.Image2;
		if (key == Image3) return GameJoystickButtons.Image3;
		if (key == Image4) return GameJoystickButtons.Image4;
		if (key == Image5) return GameJoystickButtons.Image5;
		if (key == Image6) return GameJoystickButtons.Image6;
		if (key == Image7) return GameJoystickButtons.Image7;
		if (key == Image8) return GameJoystickButtons.Image8;
		if (key == MouseKey) return GameJoystickButtons.MouseKey;
		return GameJoystickButtons.Undefined;
	}

}


