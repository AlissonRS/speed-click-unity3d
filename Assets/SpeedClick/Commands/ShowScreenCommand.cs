using UnityEngine;
using System.Collections;
using System;

public class ShowScreenCommand : Command
{
	
	public SpeedImagerScreen TargetScreen;
	
	public override IEnumerator Execute(SIComponent c)
	{
		SpeedImagerDirector.ShowScreen(TargetScreen, true);
		yield return null;
	}
	
}