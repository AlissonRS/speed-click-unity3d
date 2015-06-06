using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeedImagerDirector : MonoBehaviour {

	public SpeedImagerScreen currentScreen;

	// Use this for initialization
	void Start () {
//		ShowScreen(currentScreen);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowScreen(SpeedImagerScreen screen) {
		if (currentScreen != null)
			currentScreen.IsVisible = false;
		screen.transform.position = currentScreen.transform.position;
		currentScreen = screen;
		currentScreen.LoadScreen();
		currentScreen.IsVisible = true;
	}

}
