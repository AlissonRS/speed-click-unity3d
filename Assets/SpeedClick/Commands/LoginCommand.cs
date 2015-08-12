using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Alisson.Core;
using Alisson.Core.Repository;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Alisson.Core.Encryption;

public class LoginCommand : Command
{

	public InputField Login;
	public InputField Password;

	public ServerManager server;

	
	public override IEnumerator Execute(SIComponent component)
	{
		Button btn = (Button) component.gameObject.GetComponent(typeof(Button));
		btn.interactable = false;
		yield return StartCoroutine(server.Login(Login.text, Password.text, HttpMethodType.Get));
		if (ServerManager.LoggedUserID > 0)
        {
            UserPanel.Login();
            UserPanel.instance.gameObject.SetActive(true);
			SpeedImagerDirector.ShowScreen(Screens.MainScreen);
        }
		btn.interactable = true;
	}

}

