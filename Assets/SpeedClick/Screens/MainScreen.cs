using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Alisson.Core;

public class MainScreen : SpeedClickScreen
{

	public Button btnLogin;
	public Button btnRegister;

	public override void LoadScreen ()
	{
		if (ServerManager.LoggedUserID > 0)
		{
			btnLogin.gameObject.SetActive(false);
			btnRegister.gameObject.SetActive(false);
		}
		else
		{
			btnLogin.gameObject.SetActive(true);
			btnRegister.gameObject.SetActive(true);
		}

	}

    public override void OnEscape()
    {
        Application.Quit();
    }

}

