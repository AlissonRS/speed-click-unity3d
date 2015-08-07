using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Alisson.Core;
using Alisson.Core.Repository;
using Assets.SpeedClick.Core;

public class SceneRankingItem: BaseObject
{

	public int MaxCombo;
	public int SceneID;
	public int Score;
	public int UserID;

	public User GetUser()
	{
        return BaseRepository.getById<User>(this.UserID);
	}

	public SpeedImagerScene GetScene()
	{
        return BaseRepository.getById<SpeedImagerScene>(this.SceneID);
	}

}

