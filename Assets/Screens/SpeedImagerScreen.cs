using UnityEngine;
using System.Collections;

public class SpeedImagerScreen : MonoBehaviour {

	public Screens ScreenIndex;

	public bool Interactable
	{
		get { return gameObject.GetComponent<CanvasGroup>().interactable; }
		set { SetInteractable(value); }
	}

	public bool IsVisible
	{
		get { return gameObject.activeInHierarchy; }
		set { gameObject.SetActive(value);  if (value) { this.Fade(1); this.Interactable = value;} }
	}

	public void Fade(float value)
	{
		this.gameObject.GetComponent<CanvasGroup>().alpha = value;
	}

	public bool IsCurrentScreen()
	{
		return SpeedImagerDirector.GetCurrentScreen().ScreenIndex == this.ScreenIndex;
	}

	public virtual void LoadScreen() {}

	public void SetInteractable(bool value)
	{
		gameObject.GetComponent<CanvasGroup>().interactable = value;
		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = value;
	}

}
