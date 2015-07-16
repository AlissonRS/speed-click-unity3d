using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Alisson.Core;
using Alisson.Core.Repository;
using System.Linq;
using System.Collections.Generic;
using Alisson.Core.Encryption;
using System.Text;

public class LoginCommand : Command
{

	public InputField Login;
	public InputField Password;

	public ServerManager server;

	
	public override IEnumerator Execute(SIComponent component)
	{
		Button btn = (Button) component.gameObject.GetComponent(typeof(Button));
		btn.interactable = false;
		string login = Login.text;
		string password = StringCipher.Encrypt(Password.text, StringCipher.SecretMessage) ;
		yield return StartCoroutine(server.Login(login, password, StringCipher.SecretMessage));
		IEnumerable<User> users = BaseRepository<User>.getAll().Where(u => u.Login == login);
		if (ServerManager.LoggedUserID > 0)
		{
			MessageDialogManager.ShowDialog("Login efetuado com sucesso! Parabens!");
			SpeedImagerDirector.ShowScreen(Screens.MainScreen);
		}
		else
			MessageDialogManager.ShowDialog("Login e/ou senha incorreto(s)!");
		btn.interactable = true;
	}

}

