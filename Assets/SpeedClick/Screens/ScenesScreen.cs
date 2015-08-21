using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using Assets.SpeedClick.Core;

public class ScenesScreen : SpeedImagerScreen {
	
	public SceneDetailsPanel ScenePanel;
	public RankingPanel Ranking;
	public List<SpeedImagerScene> scenes = new List<SpeedImagerScene>();
	public LayoutGroup scenesContainer; // Set in Unity Designer

	public override void LoadScreen ()
	{
		foreach (Transform child in scenesContainer.transform)
			GameObject.Destroy(child.gameObject);
        User user = BaseRepository.add<User>();
        user.ID = 1;
        user.Login = "Test User";
        IEnumerable<SpeedImagerScene> scenesList = BaseRepository.getAll<SpeedImagerScene>();
		foreach (SpeedImagerScene scene in scenesList) // This should come from a database or http
		{
//			scene.Title = String.Format("Scene {00}", scene.ID);
//			scene.UserID = user.ID;
//			scene.HP = (i+1) * 0.7f + 3;
//			scene.Turns = i;
//			scene.TurnLength = 5.2f - ((i+1f) * 0.2f);
//			scene.SceneLength = 30;

//			for (int j = 1; j < 5; j++)
//				BaseRepository<SceneRankingItem>.add(new SceneRankingItem(j, user, scene, j * 3324, j * 27));

			scenes.Add(scene);
			GameObject sceneButton = (GameObject) Instantiate(Resources.Load("Prefabs/SceneButton"));
			Text text = (Text) sceneButton.GetComponentInChildren(typeof(Text));
			text.text = scene.Title;
			sceneButton.transform.SetParent(scenesContainer.transform, false);
			LoadSceneCommand load = sceneButton.GetComponent<LoadSceneCommand>();
			load.ScenePanel = this.ScenePanel;
			load.Ranking = this.Ranking;
			sceneButton.GetComponent<SIComponent>().SetData<SpeedImagerScene>("scene", scene);
		}
	}

}
