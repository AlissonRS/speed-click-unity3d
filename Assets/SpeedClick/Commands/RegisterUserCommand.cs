using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Alisson.Core;
using Alisson.Core.Encryption;
using System.Text;

public class RegisterUserCommand : Command {
	
	public InputField Login;
	public InputField Password;
	public InputField PasswordConfirmation;
	
	public ServerManager server;
	
	public override IEnumerator Execute(SIComponent component)
	{
		Button btn = (Button) component.gameObject.GetComponent(typeof(Button));
		btn.interactable = false;
		if (Login.text.Length < 3 || Login.text.Length > 30 )
		{
			MessageDialogManager.ShowDialog("Seu login deve ter de 3 a 30 caracteres!");
			yield break;
		}
		if (Password.text.Length < 5)
		{
			MessageDialogManager.ShowDialog("A senha deve ter ao menos 5 caracteres!");
			yield break;
		}
		if (Password.text != PasswordConfirmation.text)
		{
			MessageDialogManager.ShowDialog("As duas senhas digitadas diferem!");
			yield break;
		}
		yield return StartCoroutine(server.Login(Login.text, Password.text, HttpMethodType.Post));

		if (ServerManager.LoggedUserID > 0)
			SpeedImagerDirector.ShowScreen(Screens.MainScreen);
		btn.interactable = true;
	}

}
