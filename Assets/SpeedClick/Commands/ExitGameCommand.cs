using UnityEngine;
using System.Collections;
using UnityEditor;

public class ExitGameCommand : Command {

	public bool GetUserConfirmation;

	public override void Execute(SIComponent c)
	{
		if (!GetUserConfirmation || SpeedImagerHelpers.IsOnMobile() || EditorUtility.DisplayDialog("Sair", "Deseja realmente sair do aplicativo?", "Sim", "Nao"))
			Application.Quit();
	}

}
