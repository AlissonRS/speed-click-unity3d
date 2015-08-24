using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Assets.SpeedClick.Core;
using Alisson.Core;

public class ScoreScreen : SpeedImagerScreen {

    public ServerManager server;

    public Score score;
    public Scene scene;

    public Text Accuracy;
    public Text MaxCombo;
    public Text MissCount;
    public Text Points;
    public Text Ranking;
    public Text Speed;
    public Text TurnCount;

    public Text SceneName;
    public Text PlayedBy;

    public override void LoadScreen()
    {
        this.SceneName.text = this.scene.Title;
        if (this.score.PlayerId == 0)
        {
            this.Ranking.text = "Necessário Logar para enviar...";
            this.PlayedBy.text = "Jogado por Anônimo";
        }
        else
        {
            StartCoroutine(this.SendScore());
            User player = BaseRepository.getById<User>(this.score.PlayerId);
            this.PlayedBy.text = "Jogado por " + player.Login;
            this.Ranking.text = "Submetendo pontuação...";
        }

        this.Accuracy.text = String.Format(Constants.ACCURACY_FORMAT, this.score.Accuracy);
        this.MaxCombo.text = String.Format("{0}", this.score.MaxCombo);
        this.MissCount.text = String.Format("{0}", this.score.MissCount);
        this.Points.text = String.Format("{0:n0}", this.score.Points);
        this.Speed.text = String.Format(Constants.SPEED_FORMAT, this.score.Speed);
        this.TurnCount.text = String.Format("{0}", this.score.TurnCount);
    }

    public IEnumerator SendScore()
    {
        this.score.Platform = SpeedClickHelpers.ConvertPlatformType(Application.platform);
        yield return StartCoroutine(server.SendScore(this.score));
    }

    void Update()
    {
        if (score.Ranking > 0)
            this.Ranking.text = String.Format("{0}", this.score.Ranking);
    }

}
