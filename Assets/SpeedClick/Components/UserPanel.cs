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
    public User Element { get; set; }
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
        instance.Element = BaseRepository.getById<User>(ServerManager.LoggedUserID);
        instance.LoadData();
        instance.Element.Subscribe(instance);
        instance.Avatar.sprite = instance.Element.GetAvatar(); // If the user doesn't have the avatar atm, an "unknown" avatar will be placed at the panel, while the server tries to load the right avatar. If an Avatar is found, the user notifies all observer objects (including this panel) so they can take some action (eg. update an Image sprite)
    }

    public static void Hide()
    {
        canvas.alpha = 0;
    }

    public void LoadData()
    {
        instance.Nick.text = instance.Element.Login;
        instance.Ranking.text = "#" + instance.Element.Ranking.ToString();
        instance.Score.text = instance.Element.Score.ToString() + " pts.";
    }

    public static void Show()
    {
        if (instance.Element != null)
            canvas.alpha = 1;
    }


    public void ReceiveSubjectNotification(User sub)
    {
        instance.Element = sub;
        instance.Avatar.sprite = sub.Avatar;
        instance.LoadData();
    }

    void OnDestroy()
    {
        if (this.Element != null)
        this.Element.Unsubscribe(this);
    }

}
