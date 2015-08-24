using Alisson.Core;
using Assets.SpeedClick.Core;
using Boomlagoon.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Score : BaseObject
{
    public float Accuracy { get; set; }
    public int MaxCombo { get; set; }
    public int MissCount { get; set; }
    public Platforms Platform { get; set; }
    public int PlayerId { get; set; }
    public int Points { get; set; }
    public int Ranking { get; set; }
    public int SceneId { get; set; }
    public float Speed { get; set; }
    public int TurnCount { get; set; }

    public override void ParseObjectField(JSONObject json)
    {
        if (json.GetValue("Player") != null)
            this.PlayerId = BaseRepository.add<User>(json.GetValue("Player")).ID;
    }

}
