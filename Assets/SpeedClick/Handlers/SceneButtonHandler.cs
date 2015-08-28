using Assets.SpeedClick.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.SpeedClick.Components
{
    public class SceneButtonHandler: MonoBehaviour, IPointerClickHandler
    {

        public int SceneId;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != UnityEngine.EventSystems.PointerEventData.InputButton.Left)
                return;
            Scene scene = BaseRepository.getById<Scene>(SceneId);
            StartCoroutine(SceneSelectionController.instance.LoadScene(scene));
        }
    }
}
