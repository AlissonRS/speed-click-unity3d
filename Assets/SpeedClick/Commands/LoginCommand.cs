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

	
	public override IEnumerator Execute(SIComponent component)
	{
		Button btn = (Button) component.gameObject.GetComponent(typeof(Button));
		btn.interactable = false;
		yield return StartCoroutine(server.Login(Login.text, Password.text, HttpMethodType.Get));
		if (ServerManager.LoggedUserID > 0)
        {
            User user = BaseRepository.getAll<User>().Where(u => u.ID == ServerManager.LoggedUserID).First();
            yield return StartCoroutine(loader.Load(user));
            UserPanel.Login();
            UserPanel.Alpha = 1;
			SpeedImagerDirector.ShowScreen(Screens.MainScreen);
        }
		btn.interactable = true;
	}

}

