using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using Assets.SpeedClick.Core;
using System.Collections.Generic;
using Alisson.Core;

public class RankingPanel : MonoBehaviour
{

    public ServerManager server;
	public VerticalLayoutGroup RankingList;

    void Start()
    {
        this.Clear();
    }

	public IEnumerator SetScene(Scene scene)
	{
        this.Clear();

        IEnumerable<Score> items = scene.GetScores();
        if (items.Count() == 0 || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            yield return StartCoroutine(server.LoadScores(scene));
            items = scene.GetScores();
        }
        List<Score> scores = items.ToList();
        foreach (Score score in scores.OrderByDescending(r => r.Points))
		{
			GameObject obj = (GameObject) Instantiate(Resources.Load("Prefabs/RankingItem"));
			RankingItem item = obj.GetComponent<RankingItem>();
            User user = score.GetUser();
            user.Subscribe(item); // If the user changes his/her avatar, the ranking item will be updated...
			item.Avatar.sprite = user.GetAvatar();
			item.Nick.text = user.Login;
            item.Score.text = string.Format("Pts. {0} - ({1}x)", score.Points, score.MaxCombo);
			item.transform.SetParent(RankingList.transform, false);
		}

	}

    public void Clear()
    {
        foreach (Transform child in RankingList.transform)
            GameObject.Destroy(child.gameObject);
    }

}

