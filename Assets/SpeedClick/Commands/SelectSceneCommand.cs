using UnityEngine;
using System.Collections;

public class SelectSceneCommand : Command
{
	
	public override void Execute(SIComponent component)
	{
		GameScreen scr = (GameScreen) SpeedImagerDirector.GetScreen(Screens.GameScreen);
		scr.scene = component.GetData<SpeedImagerScene>("scene");
		SpeedImagerDirector.ShowScreen(Screens.GameScreen);
	}

}

