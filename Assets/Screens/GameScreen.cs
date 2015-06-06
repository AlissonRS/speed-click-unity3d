using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEngine.EventSystems;

public class GameScreen : SpeedImagerScreen {

	public SpeedImagerScene scene;

	public Text Points;
	public Text SceneClock;
	public Text TurnClock;
	public Slider HealthBar;
	public Image TargetImage;
	public LayoutGroup SourceImages;

	
	public override void LoadScreen()
	{
		this.TurnClock.text = Convert.ToString(scene.TurnDuration);
		this.SceneClock.text = String.Format("{0:0}:{1:00}", Mathf.Floor(scene.SceneDuration/60), scene.SceneDuration % 60);
		this.HealthBar.value = this.HealthBar.maxValue;

		foreach (Transform child in SourceImages.transform)
			GameObject.Destroy(child.gameObject);

		int i = 1;
		foreach(Sprite img in scene.Images)
		{
			GameObject sourceImage = (GameObject) Instantiate(Resources.Load("SourceImage"));
			Image srcImage = (Image) sourceImage.GetComponentInChildren(typeof(Image));
			srcImage.sprite = img;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		if (Event.current.isKey)
		{
			string tag = KeyboardShortcutConfig.GetImageTag(Event.current.keyCode);
			if (tag != "")
			{
				GameObject img = GameObject.FindGameObjectWithTag(tag);
				EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
				eventSystem.SetSelectedGameObject(img.gameObject, new BaseEventData(eventSystem));
			}
		}
	}

}
