using UnityEngine;
using System.Collections;

public class ShowScreenCommand : Command
{
	
	public Screens TargetScreen;
	
	public override void Execute(SIComponent c)
	{
		SpeedImagerDirector.ShowScreen(TargetScreen);
	}
	
}