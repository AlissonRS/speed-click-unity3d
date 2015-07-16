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
			return this.TurnClock.fillAmount * this.scene.TurnLength;
		}
		set
		{
			this.TurnClock.fillAmount = (1 * (value / this.scene.TurnLength));
		}
	}
	private float LeftSceneSecs = 0;
	private int TurnsCount = 0;
	private static List<Image> _images = new List<Image>();
	private int CurrentTargetIndex = -1;
	private bool IsKeyPressed = false;
	private float DecreaseHPAmount = 0;
	private float IncreaseHPAmount = 0;
	
	public Text ComboText;
	public Text PointsText;
	public Text SceneClock;
	public Image TurnClock;
	public Slider HealthBar;
	public Image TargetImage;
	public SourceImagesPanel SourceImages;

	public static List<Image> images {
		get { return _images; }
		private set { _images = value; }
	}
	
	public bool DoCountDown = true;
	public bool IsLoaded = false;
	public bool IsPaused { get; set; }

	public void Hit(SourceImageHandler src)
	{
		if (src.Index == this.CurrentTargetIndex)
		{
			this.Combo++;
			this.HealthBar.value += this.IncreaseHPAmount;
			this.Points += this.scene.Points() * this.Combo;
			this.PointsText.text = this.Points.ToString("D9");
		} else {
			this.Combo = 0;
			this.HealthBar.value -= this.DecreaseHPAmount;
		}
		this.LoadTarget();
	}

	public override void LoadScreen()
	{
		if(this.IsPaused) return;
		this.Interactable = false;
		this.DoCountDown = true;
		this.Interactable = true;
		this.Combo = 0;
		this.Points = 0;
		this.TurnsCount = 0;
		this.HealthBar.value = 0;
		this.PointsText.text = this.Points.ToString("D9");
		this.LeftSceneSecs = this.scene.SceneLength;
		this.DecreaseHPAmount = this.scene.DecreaseHPAmount(this.HealthBar.maxValue);
		this.IncreaseHPAmount = this.scene.IncreaseHPAmount(this.HealthBar.maxValue);

		images.Clear();
		images.AddRange(this.SourceImages.LoadImages(this.scene.Images));

		this.Interactable = true;
		this.LoadTarget();
		this.IsLoaded = true;

	}

	void LoadTarget()
	{
		this.ComboText.text = String.Format("x {0}",this.Combo);
		this.LeftTurnSecs = this.scene.TurnLength; // Reset turn timer...
		this.TurnsCount++;
		this.TargetImage.sprite = SpeedImagerHelpers.GetRandom<Image>(images).sprite;
		this.CurrentTargetIndex = SpeedImagerHelpers.LastRandomIndex;
	}

	void Update()
	{
		if (!this.IsLoaded || IsPaused) return;
		if (this.DoCountDown)
		{
			this.HealthBar.value += (this.HealthBar.maxValue / 1.5f * Time.deltaTime); // 1.5 secs to fill progress bar
			this.DoCountDown = this.HealthBar.value < this.HealthBar.maxValue;
			return;
		}

		this.LeftTurnSecs -= Time.deltaTime;
		this.LeftSceneSecs -= Time.deltaTime;
		this.HealthBar.value -= (this.DecreaseHPAmount / 3 * Time.deltaTime);
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
		}
	}

}
