using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using Assets.SpeedClick.Core;
using Alisson.Core;

public class UserPanel : MonoBehaviour, IObserver<User> {

    public static UserPanel instance;

    public Image Avatar;
    public Text Nick;
    public Text Ranking;
    public Text Score;
    public User Player;

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
        instance.gameObject.SetActive(false);
	}

    internal static void Login()
    {
        instance.Player = BaseRepository.getById<User>(ServerManager.LoggedUserID);
        instance.Player.Subscribe(instance);
        instance.UpdateObserver(instance.Player);
    }


    public void UpdateObserver(User sub)
    {
        instance.Nick.text = sub.Login;
        instance.Ranking.text = "#" + sub.Ranking.ToString();
        instance.Score.text = sub.Score.ToString() + " pts.";
        instance.Avatar.sprite = sub.GetAvatar();
    }

    public User Element
    {
        get
        {
            return instance.Player;
        }
        set
        {
            instance.Player = value;
        }
    }
}
