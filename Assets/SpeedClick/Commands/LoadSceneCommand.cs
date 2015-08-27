using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.SpeedClick.Core;
using Alisson.Core;
using System;

public class LoadSceneCommand : Command
{

    public static LoadSceneCommand instance;
	
	public SceneDetailsPanel ScenePanel;
	public RankingPanel Ranking;
	public Scene scene;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public static void Clear()
    {
        instance.scene = null;
        instance.Ranking.Clear();
    }

	public override IEnumerator ExecuteAsCoroutine()
	{
		Scene sc = this.GetData<Scene>("scene");
		if (scene == null || sc.ID != scene.ID)
		{
			scene = sc;
			yield return StartCoroutine(this.ShowSceneDetails());
		}
		else
			this.OpenScene();
        yield return null;
	}

	public IEnumerator ShowSceneDetails()
	{
        User user = scene.Creator;
        if (ScenePanel.alpha != 1)
            ScenePanel.alpha = 1;
		this.ScenePanel.Title.text = scene.Title;
		this.ScenePanel.Properties.text = scene.GetProperties();
		this.ScenePanel.Author.text = "Criada por " + user.Login;
		this.ScenePanel.Instructions.text = scene.Instructions;
		yield return StartCoroutine(this.Ranking.SetScene(scene));
	}

    public void OpenScene()
	{
        if (scene.SourceImages.Count == 0) // If there are no images loaded, send user to Loading page while images are downloaded...
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

}

