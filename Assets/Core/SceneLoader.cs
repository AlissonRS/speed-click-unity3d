using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SceneLoader : MonoBehaviour, IPointerClickHandler {

	public SpeedImagerScene scene;

	public void OnPointerClick (PointerEventData eventData)
	{
		GameScreen scr = SpeedImageHelpers.GetInstance<GameScreen>();
		scr.scene = scene;
		SpeedImageHelpers.GetInstance<SpeedImagerDirector>().ShowScreen(scr);
	}

}
