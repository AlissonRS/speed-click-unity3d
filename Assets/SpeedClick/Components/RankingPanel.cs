using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using Assets.SpeedClick.Core;
using System.Collections.Generic;

public class RankingPanel : MonoBehaviour
{
	
	public VerticalLayoutGroup RankingList;

	public IEnumerator SetScene(Scene scene)
	{
		foreach (Transform child in RankingList.transform)
			GameObject.Destroy(child.gameObject);

        IEnumerable<SceneRankingItem> items = BaseRepository.getAll<SceneRankingItem>();
        if (items.Count() == 0 || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            yield return StartCoroutine(BaseRepository.getAllFresh<SceneRankingItem>());
            items = BaseRepository.getAll<SceneRankingItem>();
        }
        foreach (SceneRankingItem ranking in items.OrderByDescending(r => r.Score))
		{
			GameObject obj = (GameObject) Instantiate(Resources.Load("Prefabs/RankingItem"));
			RankingItem item = obj.GetComponent<RankingItem>();
			User user = ranking.GetUser();
			item.Avatar.sprite = user.GetAvatar();
			item.Nick.text = user.Login;
			item.Score.text = string.Format("Pts. {0} - ({0}x)", ranking.Score, ranking.MaxCombo);
			item.transform.SetParent(RankingList.transform, false);
		}

	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

