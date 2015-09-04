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
    public float Accuracy;
    public int MaxCombo;
    public int MissCount;
    public Platforms Platform;
    public int PlayerId;
    public int Points;
    public int Ranking;
    public int SceneId;
    public float Speed;
    public int TurnCount;

    public override void ParseObjectField(JSONObject json)
    {
        if (json.GetValue("Player") != null)
            this.PlayerId = BaseRepository.add<User>(json.GetValue("Player")).ID;
    }


    public User GetUser()
    {
        return BaseRepository.getById<User>(this.PlayerId);
    }

}
