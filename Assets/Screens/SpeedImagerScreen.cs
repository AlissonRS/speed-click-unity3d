using UnityEngine;
using System.Collections;

public class SpeedImagerScreen : MonoBehaviour {

	private bool _isVisible;

	public bool IsVisible
	{
		get { return _isVisible; }
		set { 
			gameObject.SetActive(value);
			this._isVisible = value;
		}
	}

	public virtual void LoadScreen() {}

}
