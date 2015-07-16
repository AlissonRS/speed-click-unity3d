using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Alisson.Core.Repository;
using Alisson.Core;
using Alisson.Core.Encryption;
using System.Text;

public class RegisterUserCommand : Command {
	
	public Text Login;
	public Text Password;
	public Text PasswordConfirmation;
	
	public ServerManager server;
	
	public override IEnumerator Execute(SIComponent component)
	{
		if (Login.text.Length < 3 || Login.text.Length > 30 )
			SpeedImagerHelpers.DisplayModal("Seu login deve ter de 3 a 30 caracteres!");
		if (Password.text.Length < 5)
			SpeedImagerHelpers.DisplayModal("A senha deve ter ao menos 5 caracteres!");
		if (Password.text != PasswordConfirmation.text)
			SpeedImagerHelpers.DisplayModal("As duas senhas digitadas diferem!");
		string password = StringCipher.Encrypt(Password.text, StringCipher.SecretMessage);
		yield return server.RegisterUser(Login.text, password, StringCipher.SecretMessage);
	}

}
