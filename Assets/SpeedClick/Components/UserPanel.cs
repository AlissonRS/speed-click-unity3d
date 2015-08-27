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
    private static float Alpha { get { return canvas.alpha; } set { canvas.alpha = value; } }

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
        if (canvas == null)
            canvas = this.GetComponent<CanvasGroup>();
        canvas.alpha = 0;
	}

    public static void Login()
    {
        instance.Player = BaseRepository.getById<User>(ServerManager.LoggedUserID);
        instance.Player.Subscribe(instance);
        instance.Avatar.sprite = instance.Player.GetAvatar(); // If the user doesn't have the avatar atm, an "unknown" avatar will be placed at the panel, while the server tries to load the right avatar. If an Avatar is found, the user notifies all observer objects (including this panel) so they can take some action (eg. update an Image sprite)
    }

    public static void Hide()
    {
        canvas.alpha = 0;
    }

    public static void Show()
    {
        if (instance.Player != null)
            canvas.alpha = 1;
    }


    public void ReceiveSubjectNotification(User sub)
    {
        instance.Nick.text = sub.Login;
        instance.Ranking.text = "#" + sub.Ranking.ToString();
        instance.Score.text = sub.Score.ToString() + " pts.";
        instance.Avatar.sprite = sub.GetAvatar();
    }

}
