using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OrbCreationExtensions;
using UnityEngine.UI;

public class SpeedImagerDirector : MonoBehaviour {


	public static int currentScreenIndex;
	public SessionManager session; // teste

	private static List<SpeedImagerScreen> screens = new List<SpeedImagerScreen>();

	// Use this for initialization
	void Start () {
		SpeedImagerScreen[] scrs = GameObject.FindObjectsOfType<SpeedImagerScreen>();
		foreach (SpeedImagerScreen scr in scrs) {
			var rect = scr.gameObject.GetComponent<RectTransform>();
			rect.offsetMax = rect.offsetMin = new Vector2(0,0);
			scr.IsVisible = false;
		}
		screens = scrs.OrderBy(s => s.ScreenIndex).ToList();

		ShowScreen(Screens.MainScreen, false);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public static SpeedImagerScreen GetCurrentScreen()
	{
		return screens[currentScreenIndex];
	}

	public static SpeedImagerScreen GetScreen(Screens s)
	{
		return screens[(int)s];
	}

	public void ShowScreen(SpeedImagerScreen src)
	{
		ShowScreen(src.ScreenIndex);	
	}
	
	public static void ShowScreen(Screens type)
	{
		ShowScreen(type, true);
	}

	public static void ShowScreen(Screens type, bool hideCurrent)
	{
		SpeedImagerScreen screen = screens[(int)type];
		screens[currentScreenIndex].IsVisible = !hideCurrent;
		if (!hideCurrent)
			screens[currentScreenIndex].Interactable = false;
		currentScreenIndex = (int)type;
		screen.LoadScreen();
		screen.IsVisible = true;
	}

}
