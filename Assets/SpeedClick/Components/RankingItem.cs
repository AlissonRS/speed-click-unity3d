using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Alisson.Core;
using UnityEngine.EventSystems;

public class RankingItem : MonoBehaviour, IObserver<User>
{

	public Image Avatar;
	public Text Nick;
    public Text Score;
    public Text FullCombo;
    public Score score;
    public Scene scene;

    public User Element { get; set; }

    public void ReceiveSubjectNotification(User sub)
    {
        this.Avatar.sprite = sub.Avatar;
    }

    void OnDestroy()
    {
        if (this.Element != null)
        this.Element.Unsubscribe(this);
    }

    public void Load()
    {
        ScoreScreen scr = (ScoreScreen)SpeedClickDirector.instance.GetScreen(Screens.ScoreScreen);
        scr.score = this.score;
        scr.scene = this.scene;
        scr.IsNewPlay = false;
        SpeedClickDirector.instance.ShowScreen(scr, true);
    }
}

