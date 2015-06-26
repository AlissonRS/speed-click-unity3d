using UnityEngine;
using System.Collections;

public class PauseGameCommand : Command {
	
	public GameScreen screen;
	public int fadePercent;
	
	public override void Execute(SIComponent c)
	{
		screen.Fade(fadePercent / 100f);
		if (screen.IsPaused)
			SpeedImagerDirector.ShowScreen(Screens.GameScreen);
		else
			SpeedImagerDirector.ShowScreen(Screens.PauseScreen);
		screen.IsPaused = !screen.IsPaused;
	}

}

