using UnityEngine;
using System.Collections;
using UnityEditor;

public class ExitGameCommand : Command {

	public bool GetUserConfirmation;

	public override IEnumerator ExecuteAsCoroutine()
	{
		if (!GetUserConfirmation || Application.isMobilePlatform || EditorUtility.DisplayDialog("Sair", "Deseja realmente sair do aplicativo?", "Sim", "Nao"))
			Application.Quit();
		yield return null;
	}

}
