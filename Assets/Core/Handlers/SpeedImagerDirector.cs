using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpeedImagerDirector : MonoBehaviour {

	public static int currentScreenIndex;

	private static List<SpeedImagerScreen> screens = new List<SpeedImagerScreen>();

	// Use this for initialization
	void Start () {
		SpeedImagerScreen[] scrs = GameObject.FindObjectsOfType<SpeedImagerScreen>();
		screens = scrs.OrderBy(s => s.ScreenIndex).ToList();
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public static SpeedImagerScreen GetCurrentScreen()
	{
		return screens[currentScreenIndex];
	}

	public void ShowScreen(SpeedImagerScreen src) {
		ShowScreen(src.ScreenIndex);	
	}

	public static void ShowScreen(Screens type, bool hideCurrent = true) {
		SpeedImagerScreen currentScreen = screens[currentScreenIndex];
		SpeedImagerScreen screen = screens[(int)type];
		currentScreen.IsVisible = !hideCurrent;
		screen.transform.position = currentScreen.transform.position;
		currentScreenIndex = (int)type;
		screen.LoadScreen();
		screen.IsVisible = true;
	}

}
