using UnityEngine;
using System.Collections;
using Alisson.Core;
using ProgressBar;
using System;

public class LoadingScreen : SpeedClickScreen, IObserver<Scene>
{

    public Scene scene;
    public GameScreen gameScreen;
    public ServerManager server;
    public ProgressBarBehaviour progress;

	public override void LoadScreen()
    {
        progress.Value = 0;
        this.scene.Subscribe(this);
        this.Background.sprite = this.scene.GetBackground() ?? this.Background.sprite;
        StartCoroutine(LoadImagesFromServer(this.scene));
    }

    public IEnumerator LoadImagesFromServer(Scene scene)
    {
        int total = scene.UseCustomTargetImages ? scene.SourceImageCount + scene.TargetImageCount : scene.SourceImageCount;
        int alreadyLoaded = scene.UseCustomTargetImages ? scene.SourceImages.Count + scene.TargetImages.Count : scene.SourceImages.Count;
        int imgsCount = total - alreadyLoaded;
        float incValue = 100f / imgsCount;
        for (int i = 0; i < imgsCount; i++)
        {
            yield return StartCoroutine(server.LoadImageIntoSprite(scene.GetImageUrl(), scene.LoadSprite));
            progress.IncrementValue(incValue);
        }
        gameScreen.scene = this.scene;
        SpeedClickDirector.instance.ShowScreen(gameScreen, true);
    }

    void OnDisable()
    {
        progress.Value = 0;
    }


    public void ReceiveSubjectNotification(Scene sub)
    {
        this.Background.sprite = sub.Background;
    }

    public Scene Element
    {
        get
        {
            return this.scene;
        }
        set
        {
            this.scene = value;
        }
    }

    void OnDestroy()
    {
        if (this.Element != null)
            this.Element.Unsubscribe(this);
    }

}
