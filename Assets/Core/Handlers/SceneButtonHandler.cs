using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SceneButtonHandler : MonoBehaviour, IPointerClickHandler {

	public SpeedImagerScene scene;

	public void OnPointerClick (PointerEventData eventData)
	{
		GameScreen scr = SpeedImagerHelpers.GetInstance<GameScreen>();
		scr.scene = scene;
		SpeedImagerDirector.ShowScreen(scr.ScreenIndex);
	}

}
