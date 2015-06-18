using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GameScreen : SpeedImagerScreen {

	public SpeedImagerScene scene;

	private int Combo = 0;
	private int Points = 0;
	private float LeftTurnSecs
	{
		get
		{
			return this.TurnClock.fillAmount * this.scene.TurnDuration;
		}
		set
		{
			this.TurnClock.fillAmount = (1 * (value / this.scene.TurnDuration));
		}
	}
	private float LeftSceneSecs = 0;
	private int TurnsCount = 0;
	private static List<Image> _images = new List<Image>();
	private int CurrentTargetIndex = -1;
	private bool DoCountDown = true;
	private bool IsKeyPressed = false;
	private bool IsLoaded = false;
	private float DecreaseHPAmount = 0;
	private float IncreaseHPAmount = 0;

	public Text PointsText;
	public Text SceneClock;
	public Image TurnClock;
	public Slider HealthBar;
	public Image TargetImage;
	public LayoutGroup SourceImages;
	public Text DebugText;

	public static List<Image> images {
		get { return _images; }
		private set { _images = value; }
	}

	public bool IsPaused { get; set; }

	public void Hit(SourceImageHandler src)
	{
		if (src.Index == this.CurrentTargetIndex)
		{
			this.Combo++;
			this.HealthBar.value += this.IncreaseHPAmount;
			this.Points += this.scene.Points();
			this.PointsText.text = this.Points.ToString("D9");
		} else {
			this.Combo = 0;
			this.HealthBar.value -= this.DecreaseHPAmount;
		}
		this.LoadTarget();
	}

	public override void LoadScreen()
	{
		if (this.IsPaused)
		{
//			this.DebugText.text = "PAUSED";
			this.IsPaused = false;
			return;
		}
		this.DoCountDown = true;
		this.Interactable = true;
		this.Combo = 0;
		this.Points = 0;
		this.TurnsCount = 0;
		this.PointsText.text = this.Points.ToString("D9");
		this.LeftSceneSecs = this.scene.SceneDuration;
		this.DecreaseHPAmount = this.scene.DecreaseHPAmount(this.HealthBar.maxValue);
		this.IncreaseHPAmount = this.scene.IncreaseHPAmount(this.HealthBar.maxValue);

		foreach (Transform child in this.SourceImages.transform)
			GameObject.Destroy(child.gameObject);

		int i = 0;
		images.Clear();
		foreach(Sprite img in this.scene.Images)
		{
			GameObject sourceImagePrefab = (GameObject) Instantiate(Resources.Load("SourceImage"));
			Image srcImage = (Image) sourceImagePrefab.GetComponentInChildren(typeof(Image));
			SourceImageHandler handler = (SourceImageHandler) sourceImagePrefab.GetComponentInChildren(typeof(SourceImageHandler));
			handler.Index = i;
			srcImage.sprite = img;
			sourceImagePrefab.name = String.Format("sourceImage_{0}",i);
			srcImage.transform.SetParent(SourceImages.transform, false);
			images.Add(srcImage);
			i++;
		}
//		this.DebugText.text = "LOADING TARGET";

		this.LoadTarget();
		this.IsLoaded = true;

	}

	void LoadTarget()
	{
		this.LeftTurnSecs = this.scene.TurnDuration; // Reset turn timer...
		this.TurnsCount++;
		this.TargetImage.sprite = SpeedImagerHelpers.GetRandom<Image>(images).sprite;
		this.CurrentTargetIndex = SpeedImagerHelpers.LastRandomIndex;
	}

	public void Restart()
	{
		this.IsPaused = false;
		this.IsLoaded = false;
		SpeedImagerDirector.ShowScreen(Screens.GameScreen);
	}

	public void Resume()
	{
		this.IsPaused = false;
		SpeedImagerDirector.GetCurrentScreen().IsVisible = false;
		this.Interactable = true;
		this.Fade(1);
	}

	void Update()
	{
		if (!this.IsLoaded || IsPaused) return;
		if (this.DoCountDown)
		{
			this.HealthBar.value += (this.HealthBar.maxValue / 2 * Time.deltaTime); // 2 secs to fill progress bar
			this.DoCountDown = this.HealthBar.value < this.HealthBar.maxValue;
			return;
		}

		this.LeftTurnSecs -= Time.deltaTime;
		this.LeftSceneSecs -= Time.deltaTime;
		this.HealthBar.value -= (this.HealthBar.maxValue / 30 * Time.deltaTime);
		int leftSceneSecs = Convert.ToInt32(Math.Ceiling(this.LeftSceneSecs));
		int leftTurnSecs = Convert.ToInt32(Math.Ceiling(this.LeftTurnSecs));
		this.SceneClock.text = String.Format("{0:0}:{1:00}", Mathf.Floor(leftSceneSecs/60), leftSceneSecs % 60);
		
		if (leftTurnSecs <= 0)
		{
			this.HealthBar.value -= this.DecreaseHPAmount;
			this.Combo = 0;
			this.LoadTarget();
		}
	}

	void OnGUI () {
		if (!this.IsCurrentScreen() || IsPaused) return;
		if (Event.current.type == EventType.KeyUp)
			this.IsKeyPressed = false;
		if (!this.IsKeyPressed && Event.current.type == EventType.KeyDown)
		{
			this.IsKeyPressed = true;
			GameJoystick.SimulateEvent(KeyboardShortcutConfig.GetGameJoystickButton(Event.current.keyCode));
			if (Event.current.keyCode == KeyCode.Escape)
			{
				IsPaused = true;
				SpeedImagerDirector.ShowScreen(Screens.PauseScreen, 0.1f);
			}
		}
	}

}
