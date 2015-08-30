using UnityEngine;
using System.Collections;

public class ExitGameCommand : Command {

	public bool GetUserConfirmation;

	public override IEnumerator ExecuteAsCoroutine()
	{
		if (!GetUserConfirmation || Application.isMobilePlatform)
			Application.Quit();
		yield return null;
	}

}
