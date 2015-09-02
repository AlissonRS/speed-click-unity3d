using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using Assets.SpeedClick.Core;
using Alisson.Core;

public class UserPanel : MonoBehaviour, IObserver<User> {

    public static UserPanel instance;

    private CanvasGroup canvas;

    public Image Avatar;
    public Text Nick;
    public Text Ranking;
    public Text Score;
    public bool IsMainInstance;
    public User Element { get; set; }
    private float Alpha { get { return this.canvas.alpha; } set { this.canvas.alpha = value; } }

	// Use this for initialization
	void Start () {
        if (instance == null && this.IsMainInstance)
            instance = this;
        if (this.canvas == null)
            this.canvas = this.GetComponent<CanvasGroup>();
        this.canvas.alpha = 0;
	}

    public static void Login()
    {
        instance.LoadData(BaseRepository.getById<User>(ServerManager.LoggedUserID));
    }

    public void Hide()
    {
        this.canvas.alpha = 0;
    }

    public void LoadData(User user)
    {
        user.Subscribe(this);
        this.Avatar.sprite = user.GetAvatar(); // If the user doesn't have the avatar atm, an "unknown" avatar will be placed at the panel, while the server tries to load the right avatar. If an Avatar is found, the user notifies all observer objects (including this panel) so they can take some action (eg. update an Image sprite)
        this.LoadComponents(user);
    }

    private void LoadComponents(User user)
    {
        this.Nick.text = user.Login;
        this.Ranking.text = "#" + user.Ranking.ToString();
        this.Score.text = user.Score.ToString() + " pts.";
    }

    public void Show()
    {
        if (this.Element != null)
            this.canvas.alpha = 1;
    }

    public void ReceiveSubjectNotification(User sub)
    {
        this.Element = sub;
        this.Avatar.sprite = sub.Avatar;
        this.LoadComponents(this.Element);
    }

    void OnDestroy()
    {
        if (this.Element != null)
        this.Element.Unsubscribe(this);
    }

    public int LoggedPlayerID()
    {
        return this.Element == null ? 0 : this.Element.ID;
    }
}
