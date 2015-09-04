using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Alisson.Core;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Assets.SpeedClick.Core;
using UnityEngine.EventSystems;

public class LoginCommand : Command
{

	public InputField Login;
	public InputField Password;

	public ServerManager server;

    public UserAvatarLoader loader;
	
	public override IEnumerator ExecuteAsCoroutine()
	{
		Button btn = (Button) this.gameObject.GetComponent(typeof(Button));
		btn.interactable = false;
        if (Login.text.Length < 3 || Login.text.Length > 30)
        {
            MessageDialogManager.ShowDialog("Por favor, preencha seu login!");
            EventSystem.current.SetSelectedGameObject(Login.gameObject);
            btn.interactable = true;
            yield break;
        }
        if (Password.text.Length < 5)
        {
            MessageDialogManager.ShowDialog("Por favor, digite sua senha!");
            EventSystem.current.SetSelectedGameObject(Password.gameObject);
            btn.interactable = true;
            yield break;
        }
		yield return StartCoroutine(server.Login(Login.text, Password.text, HttpMethodType.Get));
		if (ServerManager.LoggedUserID > 0)
        {
            UserPanel.Login();
            UserPanel.instance.Show();
            SpeedClickDirector.instance.ShowScreenByType(Screens.MainScreen);
        }
		btn.interactable = true;
	}

}

