using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Alisson.Core;

public class RankingItem : MonoBehaviour, IObserver<User>
{

	public Image Avatar;
	public Text Nick;
	public Text Score;



    public void ReceiveSubjectNotification(User sub)
    {
        this.Avatar.sprite = sub.Avatar;
    }

}

