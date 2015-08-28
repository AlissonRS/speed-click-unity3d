using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;


[Serializable]
public class OnTabEvent : UnityEvent { }

public class OnTabHandler : MonoBehaviour {

    [SerializeField]
    private OnTabEvent OnTab;

    public void SetFocus(GameObject obj)
    {
        EventSystem.current.SetSelectedGameObject(obj);
    }

    public void OnGUI()
    {
        if (Event.current.type == EventType.KeyUp)
        {
            switch (Event.current.keyCode)
            {
                case KeyCode.Tab: OnTab.Invoke(); break;
                default: break;
            }
        }
    }


}
