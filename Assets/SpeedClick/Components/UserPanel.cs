using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using Assets.SpeedClick.Core;
using Alisson.Core;

public class UserPanel : MonoBehaviour, IObserver<User> {

    public static UserPanel instance;

    private static CanvasGroup canvas;

    public Image Avatar;
    public Text Nick;
    public Text Ranking;
    public Text Score;
    public User Player;
    public static float Alpha { get { return canvas.alpha; } set { canvas.alpha = value; } }

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
        if (canvas == null)
            canvas = this.GetComponent<CanvasGroup>();
        canvas.alpha = 0;
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
