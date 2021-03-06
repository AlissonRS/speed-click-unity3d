﻿using UnityEngine;
using System.Collections;
using Alisson.Core;
using Assets.SpeedClick.Core;

public class SceneSelectionController : MonoBehaviour, IObserver<Scene> {

    public static SceneSelectionController instance;

    public SceneDetailsPanel ScenePanel;
    public RankingPanel Ranking;
    public ScenesScreen screen;
    public int SelectedSceneID;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public static void Clear()
    {
        instance.SelectedSceneID = 0;
        instance.Ranking.Clear();
    }

    public IEnumerator LoadScene(Scene scene)
    {
        if (scene == null || SelectedSceneID != scene.ID)
        {
            SelectedSceneID = scene.ID;
            yield return StartCoroutine(this.ShowSceneDetails(scene));
        }
        else
            this.OpenScene(scene);
        yield return null;
    }

    public IEnumerator ShowSceneDetails(Scene scene)
    {
        User user = scene.Creator;
        if (ScenePanel.alpha != 1)
            ScenePanel.alpha = 1;
        this.ScenePanel.Title.text = scene.Title;
        this.ScenePanel.Properties.text = scene.GetProperties();
        this.ScenePanel.Author.text = "Criada por " + user.Login;
        this.ScenePanel.Instructions.text = scene.Instructions;
        scene.Subscribe(instance);
        instance.screen.Background.sprite = scene.GetBackground() ?? instance.screen.Background.sprite;
        yield return StartCoroutine(this.Ranking.SetScene(scene));
    }

    public void OpenScene(Scene scene)
    {
        if (scene.SourceImages.Count < scene.SourceImageCount || (scene.UseCustomTargetImages && scene.TargetImages.Count < scene.TargetImageCount )) // If there are no images loaded, send user to Loading page while images are downloaded...
        {
            LoadingScreen scr = (LoadingScreen)SpeedClickDirector.instance.GetScreen(Screens.LoadingScreen);
            scr.scene = scene;
            SpeedClickDirector.instance.ShowScreen(scr, true);
        }
        else // Otherwise, send directly to game screen...
        {
            GameScreen scr = (GameScreen)SpeedClickDirector.instance.GetScreen(Screens.GameScreen);
            scr.scene = scene;
            SpeedClickDirector.instance.ShowScreen(scr, true);
        }
    }

    void OnDestroy()
    {
        if (this.Element != null)
            this.Element.Unsubscribe(this);
    }

    private void LoadLocalSourceImages()
    {
        //		if (Application.platform == RuntimePlatform.Android)
        LoadImagesFromAssets();
        //		else
        //			LoadImagesFromDir();
    }

    private void LoadImagesFromAssets()
    {
        //string place = String.Format("Scenes/{000}", this.ID.ToString("D3"));
        //Sprite[] sprites = Resources.LoadAll <Sprite> (place); 
        //foreach (Sprite sprite in sprites) {
        //    _images.Add(sprite);
        //}
        //		_images = (List<Sprite>) sprites.ToList();
    }

    //	private void LoadImagesFromDir()
    //	{
    //		string path = String.Format("c:/SpeedImager/Scenes/{0}/", this.ID.ToString("D3"));
    //		string url = String.Format("file:///c:/SpeedImager/Scenes/{0}/", this.ID.ToString("D3"));
    //		DirectoryInfo dir = new DirectoryInfo(path);
    //		FileInfo[] info = dir.GetFiles("*.*");
    //		foreach (FileInfo f in info) 
    //		{
    //			// Start a download of the given URL
    //			WWW www = new WWW (url + f.Name);
    //			// Wait for download to complete
    //			//			yield www;
    //			_images.Add(Sprite.Create(www.texture, new Rect(0,0,www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f)));
    //		}
    //	}

    public void ReceiveSubjectNotification(Scene scene)
    {
        if (SceneSelectionController.instance.SelectedSceneID == scene.ID)
            this.screen.Background.sprite = scene.Background;
    }

    public Scene Element
    {
        get
        {
            return BaseRepository.getById<Scene>(this.SelectedSceneID);
        }
        set
        {
            this.SelectedSceneID = value.ID;
        }
    }
}
