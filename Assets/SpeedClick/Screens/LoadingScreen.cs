using UnityEngine;
using System.Collections;
using Alisson.Core;
using ProgressBar;
using System;

public class LoadingScreen : SpeedImagerScreen
{

    public Scene scene;
    public GameScreen gameScreen;
    public ServerManager server;
    public ProgressBarBehaviour progress;
    
	public override void LoadScreen()
    {
        progress.Value = 0;
        StartCoroutine(LoadImagesFromServer(scene));
    }

    public IEnumerator LoadImagesFromServer(Scene scene)
    {
        int imgsCount = scene.SourceImageCount + scene.TargetImageCount;
        float incValue = 100f / imgsCount;
        for (int i = 0; i < scene.SourceImageCount; i++)
        {
            string url = String.Format("scenes/source/{0}/{1}.png", scene.ID.ToString("D8"), i + 1);
            yield return StartCoroutine(server.LoadImageIntoSprite(scene, url));
            progress.IncrementValue(incValue);
        }
        if (scene.UseCustomTargetImages)
        {
            for (int i = 0; i < scene.TargetImageCount; i++)
            {
                string url = String.Format("scenes/target/{0}/{1}.png", scene.ID.ToString("D8"), i + 1);
                yield return StartCoroutine(server.LoadImageIntoSprite(scene, url));
                progress.IncrementValue(incValue);
            }

            progress.Value = 100;
        }
        gameScreen.scene = this.scene;
        SpeedImagerDirector.ShowScreen(gameScreen, true);
    }

}
