using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Alisson.Core.Repository;
using Assets.SpeedClick.Core;

public class LoadSceneCommand : Command
{
	
	public SceneDetailsPanel ScenePanel;
	public RankingPanel Ranking;
	public static SpeedImagerScene scene;

	public override IEnumerator Execute(SIComponent component)
	{
		SpeedImagerScene sc = component.GetData<SpeedImagerScene>("scene");
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
        User user = BaseRepository.getById<User>(scene.UserID);
		this.ScenePanel.Title.text = scene.Title;
		this.ScenePanel.Properties.text = scene.GetProperties();
		this.ScenePanel.Author.text = "Criada por " + user.Login;
		this.ScenePanel.Instructions.text = scene.Instructions;
		yield return StartCoroutine(this.Ranking.SetScene(scene));
	}

	public void OpenScene()
	{
		GameScreen scr = (GameScreen) SpeedImagerDirector.GetScreen(Screens.GameScreen);
		scr.scene = scene;
		SpeedImagerDirector.ShowScreen(Screens.GameScreen);
	}

}

