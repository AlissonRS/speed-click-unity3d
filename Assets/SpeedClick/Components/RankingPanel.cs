using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using Assets.SpeedClick.Core;
using System.Collections.Generic;
using Alisson.Core;
using System;

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
        IEnumerable<Score> items = scene.GetScores();
        if (items.Count() == 0 || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            yield return StartCoroutine(server.LoadScores(scene));
            items = scene.GetScores();
        }
        if (SceneSelectionController.instance.SelectedSceneID == scene.ID)
        {
            this.Clear();
            List<Score> scores = items.ToList();
            foreach (Score score in scores.OrderBy(r => r.Ranking))
            {
                if (score.PlayerId == 0)
                    continue;
                GameObject obj = (GameObject)Instantiate(Resources.Load("Prefabs/RankingItem"));
                RankingItem item = obj.GetComponent<RankingItem>();
                User user = score.GetUser();
                user.Subscribe(item); // If the user changes his/her avatar, the ranking item will be updated...
                item.Avatar.sprite = user.GetAvatar();
                item.Nick.text = user.Login;
                item.Score.text = String.Format("Pts. {0} - ({1}x)", score.Points, score.MaxCombo);
                item.FullCombo.text = score.MissCount == 0 ? "Full Combo" : "";
                item.transform.SetParent(RankingList.transform, false);
            }
        }
        yield break;
	}

    public void Clear()
    {
        foreach (Transform child in RankingList.transform)
            GameObject.Destroy(child.gameObject);
    }

}

