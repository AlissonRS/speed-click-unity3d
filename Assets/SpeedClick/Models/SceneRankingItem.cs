using UnityEngine;
using System.Collections;
using Alisson.Core;
using Alisson.Core.Repository;

public class SceneRankingItem: BaseObject
{

	public int MaxCombo;
	public int SceneID;
	public int Score;
	public int UserID;
	
	public SceneRankingItem() : base() { }

	public SceneRankingItem (int id, User user, SpeedImagerScene scene, int score, int maxCombo)
	{
		this.ID = id;
		this.UserID = user.ID;
		this.SceneID = scene.ID;
		this.Score = score;
		this.MaxCombo = maxCombo;
	}

	public User GetUser()
	{
		return BaseRepository<User>.getByID(this.UserID);
	}

	public SpeedImagerScene GetScene()
	{
		return BaseRepository<SpeedImagerScene>.getByID(this.SceneID);
	}

}

