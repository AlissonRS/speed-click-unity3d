using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Assets.SpeedClick.Core;
using System.Linq;

public class GameScreen : SpeedClickScreen {

    public static GameScreen instance;

    public GameStatusDirector Director;

	public Scene scene;

	private int Combo = 0;
	private int Points = 0;
    private float _leftSceneSecs;
	private float LeftTurnSecs
	{
		get { return this.TurnClock.fillAmount * this.scene.TurnLength; }
		set { this.TurnClock.fillAmount = (1 * (value / this.scene.TurnLength)); }
	}
    private float LeftSceneSecs { get { return this._leftSceneSecs; } set { this._leftSceneSecs = value < 0 ? 0 : value; } }
    private float Accuracy;
    private int MissCount;
	private int TurnCount;
    private int MaxCombo;
    private float Speed;
	private int CurrentTargetIndex;
	private bool IsKeyPressed = false;
	private float DecreaseHPAmount = 0;
	private float IncreaseHPAmount = 0;

    public Text AccuracyText;
	public Text ComboText;
	public Text PointsText;
    public Text SceneClock;
    public Text SpeedText;
	public Image TurnClock;
	public Slider HealthBar;
	public Image TargetImage;
	public SourceImagesPanel SourceImagesPanelObject;
	
	public bool DoCountDown = true;
    public bool IsLoaded = false;
	public bool IsPaused { get; set; }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

	public void Hit(SourceImageHandler src)
	{
		if (src.Index == this.CurrentTargetIndex)
		{
			this.Combo++;
			this.HealthBar.value += this.IncreaseHPAmount;
			this.Points += this.scene.Points() * this.Combo;
            this.PointsText.text = this.Points.ToString("D9");
            this.MaxCombo = this.MaxCombo > this.Combo ? this.MaxCombo : this.Combo;
		} else {
            this.MaxCombo = this.MaxCombo > this.Combo ? this.MaxCombo : this.Combo;
            this.MissCount++;
			this.Combo = 0;
			this.HealthBar.value -= this.DecreaseHPAmount;
		}

        float PassedSecs = this.scene.SceneLength - this.LeftSceneSecs;
        if (PassedSecs > 0)
            this.Speed = this.TurnCount / PassedSecs * 60f;

		this.LoadTarget();
	}

	public override void LoadScreen()
	{
        UserPanel.instance.Hide();
		if(this.IsPaused)
            return;
        this.Interactable = false;
        this.IsLoaded = false;
		this.DoCountDown = true;
        this.Accuracy = 0;
		this.Combo = 0;
        this.CurrentTargetIndex = -1;
		this.Points = 0;
        this.MaxCombo = 0;
        this.MissCount = 0;
        this.Speed = 0;
		this.TurnCount = 0;
		this.HealthBar.value = 0;
		this.PointsText.text = this.Points.ToString("D9");
		this.LeftSceneSecs = this.scene.SceneLength;
		this.DecreaseHPAmount = this.scene.DecreaseHPAmount(this.HealthBar.maxValue);
		this.IncreaseHPAmount = this.scene.IncreaseHPAmount(this.HealthBar.maxValue);

        this.SourceImagesPanelObject.LoadImages(this.scene.SourceImages);

        this.Background.sprite = this.scene.GetBackground();

        this.LoadTarget();
        this.IsLoaded = true;
	}

	void LoadTarget()
	{
		this.ComboText.text = String.Format("x {0}",this.Combo);
        this.LeftTurnSecs = this.scene.TurnLength; // Reset turn timer...
        if (this.TurnCount > 0)
            this.Accuracy = (float) (this.TurnCount - this.MissCount) / this.TurnCount * 100f;
		this.TurnCount++;
		this.TargetImage.sprite = SpeedClickHelpers.GetRandom<Sprite>(scene.TargetImages);
        this.CurrentTargetIndex = SpeedClickHelpers.LastRandomIndex;
        this.AccuracyText.text = String.Format(Constants.ACCURACY_FORMAT, this.Accuracy);
        this.SpeedText.text = String.Format(Constants.SPEED_FORMAT, this.Speed);
	}

	void Update()
	{
        if (!IsLoaded || IsPaused)
            return;

        if (this.IsWithinCountdownTime())
            return;

        UpdateRunData(); // like hp, turn time, scene time...

        CheckGameOver();

        CheckTurnEnd();

        CheckSceneEnd();

	}

    private void CheckGameOver()
    {
        if (this.HealthBar.value <= 0)
            this.Director.GameOver(30);
    }

    private void CheckTurnEnd()
    {
        int leftSceneSecs = Convert.ToInt32(Math.Ceiling(this.LeftSceneSecs));
        int leftTurnSecs = Convert.ToInt32(Math.Ceiling(this.LeftTurnSecs));
        this.SceneClock.text = String.Format("{0:0}:{1:00}", Mathf.Floor(leftSceneSecs / 60), leftSceneSecs % 60);

        if (leftTurnSecs <= 0)
        {
            this.HealthBar.value -= this.DecreaseHPAmount;
            this.Combo = 0;

            this.LoadTarget();
        }

    }

    private void CheckSceneEnd()
    {
        if (this.LeftSceneSecs <= 0)
        {
            this.IsLoaded = false;
            this.FinishScene();
        }
    }

    private void UpdateRunData()
    {
        this.LeftTurnSecs -= Time.deltaTime;
        this.LeftSceneSecs -= Time.deltaTime;
        this.HealthBar.value -= (this.DecreaseHPAmount / 3 * Time.deltaTime);
    }

    private bool IsWithinCountdownTime()
    {
        if (!this.DoCountDown)
            return false;
        this.HealthBar.value += (this.HealthBar.maxValue / 1.5f * Time.deltaTime); // 1.5 secs to fill progress bar
        this.DoCountDown = this.HealthBar.value < this.HealthBar.maxValue;
        if (!this.DoCountDown)
            this.Interactable = true;
        return true;

    }

    private void FinishScene()
    {
        int PlayerId;
        if (UserPanel.instance.Element != null)
            PlayerId = UserPanel.instance.Element.ID;
        else
            PlayerId = 0;
        Score score = BaseRepository.getAll<Score>().FirstOrDefault(s => s.PlayerId == PlayerId && s.SceneId == this.scene.ID);
        if (score == null)
            score = BaseRepository.add<Score>();
        score.PlayerId = PlayerId;
        score.Points = this.Points;
        score.SceneId = this.scene.ID;
        score.MaxCombo = this.MaxCombo;
        score.MissCount = this.MissCount;
        score.Ranking = 0;
        score.TurnCount = this.TurnCount - 1; // The last one does not count ;)
        score.Accuracy = this.Accuracy;
        score.Speed = this.Speed;
        ScoreScreen scr = (ScoreScreen)SpeedClickDirector.instance.GetScreen(Screens.ScoreScreen);
        scr.score = score;
        scr.scene = this.scene;
        scr.IsNewPlay = true;
        UserPanel.instance.Show();
        SpeedClickDirector.instance.ShowScreen(scr, true);
    }

	void OnGUI () {
		if (IsPaused || !IsLoaded || !this.IsCurrentScreen()) return;
		if (Event.current.type == EventType.KeyUp)
			this.IsKeyPressed = false;
		if (!this.IsKeyPressed && Event.current.type == EventType.KeyDown)
		{
			this.IsKeyPressed = true;
			GameJoystick.SimulateEvent(KeyboardShortcutConfig.GetGameJoystickButton(Event.current.keyCode));
		}
	}

    public override void OnEscape()
    {
        if (this.IsLoaded)
            Director.Pause(30);
    }

}
