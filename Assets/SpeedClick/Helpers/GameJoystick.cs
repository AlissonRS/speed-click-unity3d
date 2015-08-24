using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameJoystick : MonoBehaviour
{

	public static void SimulateEvent(GameJoystickButtons b)
	{
		switch (b)
		{
		case GameJoystickButtons.Undefined: return;
		case GameJoystickButtons.MouseKey: ExecuteEvents.Execute(SourceImageHandler.currentObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler); break;
        default: ExecuteEvents.Execute(SourceImagesPanel.instance.Images[(int)b].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler); break;
		}
	}
	
}

