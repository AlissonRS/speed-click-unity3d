using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SourceImageHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	public int Index = -1;
	public Image childImage;
	public static GameObject currentObject;

	public void OnPointerClick (PointerEventData eventData)
	{
		SpeedClickHelpers.GetInstance<GameScreen>().Hit(this);
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
