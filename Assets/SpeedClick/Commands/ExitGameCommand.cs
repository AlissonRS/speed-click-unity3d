using UnityEngine;
using System.Collections;

public class ExitGameCommand : Command {

	public bool GetUserConfirmation;

	public override IEnumerator ExecuteAsCoroutine()
	{
		Application.Quit();
		yield return null;
	}

}
