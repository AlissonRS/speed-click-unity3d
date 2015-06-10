using UnityEngine;
using System.Collections;

public class SpeedImagerScreen : MonoBehaviour {

	private bool _isVisible;

	public Screens ScreenIndex;

	public bool IsVisible
	{
		get { return _isVisible; }
		set { 
			gameObject.SetActive(value);
			this._isVisible = value;
		}
	}

	public bool IsCurrentScreen()
	{
		return SpeedImagerDirector.GetCurrentScreen().ScreenIndex == this.ScreenIndex;
	}

	public virtual void LoadScreen() {}

}
