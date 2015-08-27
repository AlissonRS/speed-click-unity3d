﻿using UnityEngine;
using System.Collections;

public class SpeedClickScreen : MonoBehaviour {

    public Screens ScreenIndex;
    public Screens PreviousScreenIndex;


	public bool Interactable
	{
		get { return gameObject.GetComponent<CanvasGroup>().interactable; }
		set { SetInteractable(value); }
	}

	public bool IsVisible
	{
		get { return gameObject.activeInHierarchy; }
		set { gameObject.SetActive(value);  if (value) { this.Fade(1); this.SetInteractable(value);} }
	}

	public void Fade(float value)
	{
		this.gameObject.GetComponent<CanvasGroup>().alpha = value;
	}

	public bool IsCurrentScreen()
	{
        return SpeedClickDirector.instance.GetCurrentScreen().ScreenIndex == this.ScreenIndex;
	}

	public virtual void LoadScreen() {}

	public void SetInteractable(bool value)
	{
		gameObject.GetComponent<CanvasGroup>().interactable = value;
		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = value;
	}

    public virtual void OnEscape()
    {
        if (this.IsCurrentScreen())
            return;
        SpeedClickDirector.instance.ShowScreen(this);
    }

}