using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Alisson.Core;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Alisson.Core.Encryption;
using Assets.SpeedClick.Core;

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
		yield return StartCoroutine(server.Login(Login.text, Password.text, HttpMethodType.Get));
		if (ServerManager.LoggedUserID > 0)
        {
            User user = BaseRepository.getAll<User>().Where(u => u.ID == ServerManager.LoggedUserID).First();
            UserPanel.Login();
            UserPanel.Show();
            SpeedClickDirector.instance.ShowScreenByType(Screens.MainScreen);
        }
		btn.interactable = true;
	}

}

