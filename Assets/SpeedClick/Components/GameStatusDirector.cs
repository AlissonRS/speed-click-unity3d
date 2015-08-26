using UnityEngine;
using System.Collections;
using System;

public class GameStatusDirector: MonoBehaviour
{

	public GameScreen screen;

	public void Exit()
	{
		this.screen.IsPaused = false;
        this.screen.IsLoaded = false;
		this.screen.DoCountDown = true;
        screen.IsVisible = false;
        SpeedClickDirector.instance.ShowScreenByType(Screens.ScenesScreen);
	}

	public void Pause(int fadePercent)
	{
        UserPanel.Show();
		this.screen.IsPaused = true;
        SpeedClickDirector.instance.ShowScreen(Screens.PauseScreen, false);
		screen.Fade(fadePercent / 100f);
	}
	
	public void Restart()
	{
        this.screen.IsPaused = false;
        this.screen.IsLoaded = false;
        this.screen.DoCountDown = true;
        SpeedClickDirector.instance.ShowScreenByType(Screens.GameScreen);
	}
	
	public void Resume()
	{
        SpeedClickDirector.instance.ShowScreenByType(Screens.GameScreen);
		this.screen.IsPaused = false;
		this.screen.Interactable = true;
		this.screen.Fade(1);
	}

}

