using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Alisson.Core.Repository;
using System.Linq;

public class RankingPanel : MonoBehaviour
{
	
	public VerticalLayoutGroup RankingList;

	public void SetScene(SpeedImagerScene scene)
	{
		foreach (Transform child in RankingList.transform)
			GameObject.Destroy(child.gameObject);

		foreach(SceneRankingItem ranking in BaseRepository<SceneRankingItem>.getAll().OrderByDescending(r => r.Score))
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

