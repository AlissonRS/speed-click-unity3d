using UnityEngine;
using System.Collections;
using System;

public enum PauseMenuAction
{
	Continue,
	Retry,
	Exit,
	Pause
}

public class PauseGameCommand : Command {


	public GameScreen screen;
	public int fadePercent;
	public PauseMenuAction Action;
	
	public override IEnumerator Execute(SIComponent c)
	{
		switch (Action)
		{
		case PauseMenuAction.Continue: this.Resume(); break;
		case PauseMenuAction.Retry: this.Restart(); break;
		case PauseMenuAction.Exit: this.Exit(); break;
		case PauseMenuAction.Pause: this.Pause(); break;
		default: throw new ArgumentOutOfRangeException();
		}
		yield return null;
	}
	
	public void Exit()
	{
		this.screen.IsPaused = false;
        this.screen.IsLoaded = false;
		this.screen.DoCountDown = true;
		SpeedImagerDirector.ShowScreen(Screens.ScenesScreen);
	}

	public void Pause()
	{
        UserPanel.Alpha = 1;
		this.screen.IsPaused = true;
		SpeedImagerDirector.ShowScreen(Screens.PauseScreen, false);
		screen.Fade(fadePercent / 100f);
	}
	
	public void Restart()
	{
        this.screen.IsPaused = false;
        this.screen.IsLoaded = false;
        this.screen.DoCountDown = true;
		SpeedImagerDirector.ShowScreen(Screens.GameScreen);
	}
	
	public void Resume()
	{
		SpeedImagerDirector.ShowScreen(Screens.GameScreen);
		this.screen.IsPaused = false;
		this.screen.Interactable = true;
		this.screen.Fade(1);
	}

}

