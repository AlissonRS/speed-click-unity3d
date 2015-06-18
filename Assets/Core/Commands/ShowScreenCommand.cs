using UnityEngine;
using System.Collections;

public class ShowScreenCommand : Command
{
	
	public Screens TargetScreen;

	public override void Execute()
	{
		SpeedImagerDirector.ShowScreen(TargetScreen);
	}

}

