using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SourceImageHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	public int Index = -1;
	public static GameObject currentObject;

	public void OnPointerClick (PointerEventData eventData)
	{
		SpeedImagerHelpers.GetInstance<GameScreen>().Hit(this);
	}
	
	public void OnPointerEnter (PointerEventData eventData)
	{
		currentObject = this.gameObject;
	}
	
	public void OnPointerExit (PointerEventData eventData)
	{
		currentObject = null;
	}
	
}
