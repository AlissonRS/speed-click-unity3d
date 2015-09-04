using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OrbCreationExtensions;
using UnityEngine.UI;

public class SpeedClickDirector : MonoBehaviour
{

    public static SpeedClickDirector instance;

    public Screens currentScreenIndex;

    public List<SpeedClickScreen> screens = new List<SpeedClickScreen>();

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
		SpeedClickScreen[] scrs = GameObject.FindObjectsOfType<SpeedClickScreen>();
		foreach (SpeedClickScreen scr in scrs) {
			var rect = scr.gameObject.GetComponent<RectTransform>();
			rect.offsetMax = rect.offsetMin = new Vector2(0,0);
			scr.IsVisible = false;
		}
		screens = scrs.OrderBy(s => s.ScreenIndex).ToList();

		ShowScreen(Screens.MainScreen, false);
	}

	public SpeedClickScreen GetCurrentScreen()
	{
		return instance.screens[(int)currentScreenIndex];
	}

	public SpeedClickScreen GetScreen(Screens s)
	{
		return screens[(int)s];
	}

    public void ShowScreen(SpeedClickScreen screen)
    {
        ShowScreen(screen, true);
    }
	
    public void ShowScreenByType(Screens type)
    {
        ShowScreen(type, true);
    }

	public void ShowScreen(Screens type, bool hideCurrent)
	{
		ShowScreen(screens[(int)type], hideCurrent);
	}
	
	public void ShowScreen(SpeedClickScreen activeScreen, bool hideCurrent)
	{
		if (hideCurrent)
            screens[(int)currentScreenIndex].IsVisible = false;
		else
            screens[(int)currentScreenIndex].SetInteractable(false);
        currentScreenIndex = activeScreen.ScreenIndex;
        activeScreen.IsVisible = true;
		activeScreen.LoadScreen();
	}

    public void OnGUI()
    {
        if (Event.current.type == EventType.KeyUp)
        {
            switch (Event.current.keyCode)
            {
                case KeyCode.Escape: GetCurrentScreen().OnEscape(); break;
                default: break;
            }
        }
    }

}
