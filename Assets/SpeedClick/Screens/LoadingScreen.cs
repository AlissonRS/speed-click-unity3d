using UnityEngine;
using System.Collections;
using Alisson.Core;
using ProgressBar;
using System;

public class LoadingScreen : SpeedClickScreen
{

    public Scene scene;
    public GameScreen gameScreen;
    public ServerManager server;
    public ProgressBarBehaviour progress;

	public override void LoadScreen()
    {
        progress.SetFillerSizeAsPercentage(0);
        progress.Value = 0;
        StartCoroutine(LoadImagesFromServer(scene));
    }

    public IEnumerator LoadImagesFromServer(Scene scene)
    {
        int imgsCount = scene.SourceImageCount + scene.TargetImageCount;
        float incValue = 100f / imgsCount;
        for (int i = 0; i < imgsCount; i++)
        {
            yield return StartCoroutine(server.LoadImageIntoSprite(scene));
            progress.IncrementValue(incValue);
        }
        gameScreen.scene = this.scene;
        SpeedClickDirector.instance.ShowScreen(gameScreen, true);
    }

}
