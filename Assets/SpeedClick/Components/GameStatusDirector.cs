using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GameStatusDirector: MonoBehaviour
{

    public GameScreen screen;
    public Text Title;
    public Button Continuar;
    public bool IsGameOver;

    private DateTime LastPausedTime;

    void Start()
    {
        this.IsGameOver = false;
        this.LastPausedTime = DateTime.UtcNow;
    }

	public void Exit()
	{
		this.screen.IsPaused = false;
        this.screen.IsLoaded = false;
		this.screen.DoCountDown = true;
        screen.IsVisible = false;
        SpeedClickDirector.instance.ShowScreenByType(Screens.ScenesScreen);
	}

	public void Pause(int fadePercent)
    {
        TimeSpan span = (DateTime.UtcNow - this.LastPausedTime);
        if (span.TotalSeconds < 3)
            return;
        this.IsGameOver = false;
        this.Continuar.gameObject.SetActive(true);
        this.Title.text = "Jogo Pausado!";
        UserPanel.instance.Show();
		this.screen.IsPaused = true;
        SpeedClickDirector.instance.ShowScreen(Screens.PauseScreen, false);
		screen.Fade(fadePercent / 100f);
	}

    public void GameOver(int fadePercent)
    {
        this.Continuar.gameObject.SetActive(false);
        this.IsGameOver = true;
        this.Title.text = "Você Falhou!";
        UserPanel.instance.Show();
        this.screen.IsPaused = true;
        SpeedClickDirector.instance.ShowScreen(Screens.PauseScreen, false);
        screen.Fade(fadePercent / 100f);
    }
	
	public void Restart()
	{
        this.screen.IsPaused = false;
        this.screen.IsLoaded = false;
        this.screen.DoCountDown = true;
        SpeedClickDirector.instance.ShowScreenByType(Screens.GameScreen);
	}
	
	public void Resume()
    {
        this.LastPausedTime = DateTime.UtcNow;
        SpeedClickDirector.instance.ShowScreenByType(Screens.GameScreen);
		this.screen.IsPaused = false;
		this.screen.Interactable = true;
		this.screen.Fade(1);
	}


    public void OnEscape()
    {
        if (this.IsGameOver)
            this.Exit();
        else
            this.Resume();
    }
}

