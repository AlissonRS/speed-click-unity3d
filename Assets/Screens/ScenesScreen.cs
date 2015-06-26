using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class ScenesScreen : SpeedImagerScreen {

	public List<SpeedImagerScene> scenes = new List<SpeedImagerScene>();
	public LayoutGroup scenesContainer; // Set in Unity Designer
	public Button BackButton;

	// Use this for initialization
	void Start () {
		
	}

	public override void LoadScreen ()
	{
		foreach (Transform child in scenesContainer.transform)
			GameObject.Destroy(child.gameObject);
		for (int i = 0; i < 4; i++) // This should come from a database or http
		{
			SpeedImagerScene scene = new SpeedImagerScene(i + 1);
			scene.Name = String.Format("Scene {00}", scene.ID);
			scene.HP = (i+1) * 0.7f + 3;
			scene.Turns = i;
			scene.TurnDuration = 5.2f - ((i+1f) * 0.2f);
			scene.SceneDuration = 30;
			scenes.Add(scene);
			GameObject sceneButton = (GameObject) Instantiate(Resources.Load("Prefabs/SceneButton"));
			Text text = (Text) sceneButton.GetComponentInChildren(typeof(Text));
			text.text = scene.Name;
			sceneButton.transform.SetParent(scenesContainer.transform, false);
			sceneButton.GetComponent<SIComponent>().SetData<SpeedImagerScene>("scene", scene);
		}
	}

}
