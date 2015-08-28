using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.SpeedClick.Components;

public class ScenesContainer : MonoBehaviour {

    public static ScenesContainer instance;
    private static LayoutGroup container;

    public SceneDetailsPanel ScenePanel;
    public RankingPanel Ranking;
    public Text BackgroundText;
    public List<Scene> scenes = new List<Scene>();

    public void Awake()
    {
        if (instance == null)
            instance = this;
        container = instance.GetComponent<LayoutGroup>();
        foreach (Transform child in container.transform)
            GameObject.Destroy(child.gameObject);
    }

	public void AddScene(Scene scene)
    {
        BackgroundText.text = "";
        scenes.Add(scene);
        GameObject sceneButton = (GameObject)Instantiate(Resources.Load("Prefabs/SceneButton"));
        Text text = (Text)sceneButton.GetComponentInChildren(typeof(Text));
        text.text = scene.Title;
        sceneButton.transform.SetParent(container.transform, false);
        SceneButtonHandler h = sceneButton.GetComponent<SceneButtonHandler>();
        h.SceneId = scene.ID;
    }

}
