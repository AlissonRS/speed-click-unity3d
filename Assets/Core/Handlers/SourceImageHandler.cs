using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SourceImageHandler : MonoBehaviour, IPointerClickHandler {

	public int Index = -1;

	public void OnPointerClick (PointerEventData eventData)
	{
		SpeedImagerHelpers.GetInstance<GameScreen>().Hit(this);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
