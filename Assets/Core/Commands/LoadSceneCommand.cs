using UnityEngine;
using System.Collections;

public class LoadSceneCommand : Command
{
	
	public SpeedImagerScene scene;

	public override void Execute()
	{
		GameScreen scr = (GameScreen) SpeedImagerDirector.GetScreen(Screens.GameScreen);
		scr.scene = scene;
		SpeedImagerDirector.ShowScreen(Screens.GameScreen);
	}

}

