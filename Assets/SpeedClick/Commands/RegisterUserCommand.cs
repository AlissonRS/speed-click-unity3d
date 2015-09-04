using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Alisson.Core;
using System.Text;
using UnityEngine.EventSystems;

public class RegisterUserCommand : Command {
	
	public InputField Login;
	public InputField Password;
	public InputField PasswordConfirmation;
	
	public ServerManager server;
	
	public override IEnumerator ExecuteAsCoroutine()
	{
		Button btn = (Button) this.gameObject.GetComponent(typeof(Button));
		btn.interactable = false;
		if (Login.text.Length < 3 || Login.text.Length > 30 )
		{
            MessageDialogManager.ShowDialog("Seu login deve ter de 3 a 30 caracteres!");
            EventSystem.current.SetSelectedGameObject(Login.gameObject);
            btn.interactable = true;
			yield break;
		}
		if (Password.text.Length < 5)
		{
            MessageDialogManager.ShowDialog("A senha deve ter ao menos 5 caracteres!");
            EventSystem.current.SetSelectedGameObject(Password.gameObject);
            btn.interactable = true;
			yield break;
		}
		if (Password.text != PasswordConfirmation.text)
		{
            MessageDialogManager.ShowDialog("As duas senhas digitadas diferem!");
            EventSystem.current.SetSelectedGameObject(Password.gameObject);
            btn.interactable = true;
			yield break;
		}
		yield return StartCoroutine(server.Login(Login.text, Password.text, HttpMethodType.Post));

		if (ServerManager.LoggedUserID > 0)
        {
            UserPanel.Login();
            UserPanel.instance.Show();
            SpeedClickDirector.instance.ShowScreenByType(Screens.MainScreen);
        }
		btn.interactable = true;
	}

}
