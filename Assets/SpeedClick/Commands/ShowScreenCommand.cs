using UnityEngine;
using System.Collections;
using System;

public class ShowScreenCommand : Command
{
	
	public SpeedClickScreen TargetScreen;
	
	public override IEnumerator ExecuteAsCoroutine()
	{
        SpeedClickDirector.instance.ShowScreen(TargetScreen, true);
		yield return null;
	}
	
}