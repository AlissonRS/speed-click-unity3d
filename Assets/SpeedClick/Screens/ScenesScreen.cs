using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using Assets.SpeedClick.Core;

public class ScenesScreen : SpeedClickScreen {

	public override void LoadScreen ()
	{
        LoadSceneCommand.Clear();
        //IEnumerable<Scene> scenesList = BaseRepository.getAll<Scene>();
        //foreach (Scene scene in scenesList)
        //{z
        //    scenes.Add(scene);
        //    GameObject sceneButton = (GameObject) Instantiate(Resources.Load("Prefabs/SceneButton"));
        //    Text text = (Text) sceneButton.GetComponentInChildren(typeof(Text));
        //    text.text = scene.Title;
        //    sceneButton.transform.SetParent(scenesContainer.transform, false);
        //    LoadSceneCommand load = sceneButton.GetComponent<LoadSceneCommand>();
        //    load.ScenePanel = this.ScenePanel;
        //    load.Ranking = this.Ranking;
        //    sceneButton.GetComponent<SIComponent>().SetData<Scene>("scene", scene);
        //}
	}

}
