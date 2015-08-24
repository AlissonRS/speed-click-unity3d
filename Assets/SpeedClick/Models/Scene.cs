using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using Alisson.Core;
using Boomlagoon.JSON;
using System.Reflection;
using Assets.SpeedClick.Core;

public class Scene: BaseObject, ISpritable
{

	public float HP; // How fast the HP decreases...
	public string Instructions;
	public int SceneLength; // In secs...
	public string Title;
	public float TurnLength; // In secs...
	public int Turns; // How much turns the player has to play before we change the source images...
    public int SourceImageCount; // How many source images we need to get from server.
    public int TargetImageCount; // How many target images we need to get from server. It can be zero if source and target are the same...
    public bool UseCustomTargetImages;
    public User Creator;

    private List<Sprite> _sourceImages = new List<Sprite>();
    private List<Sprite> _targetImages = new List<Sprite>();

	public List<Sprite> SourceImages { get { return this._sourceImages; } }

    public List<Sprite> TargetImages
    {
        get
        {
            if (this.UseCustomTargetImages)
                return this._targetImages;
            return this.SourceImages;
        }
    }

	public float DecreaseHPAmount(float max)
	{
		return max * (this.HP / 10f * 0.3f); // The more the HP, the more it decreases...
	}

	public string GetProperties()
    {
        int tic = this.TargetImageCount == 0 ? this.SourceImageCount : this.TargetImageCount;
        return String.Format("SI {0} - TI {1} - HP {2}\nTL {3} - SL {4} - TC {5}", this.SourceImageCount, tic, this.HP, this.TurnLength, this.SceneLength, this.Turns);
        //return String.Format("SourceImages: {0} - TargetImages: - HP: {1} - TurnLength: {2} - SceneLength: {3} - TurnCount: {4}", this.SourceImageCount, this.TargetImageCount, this.HP, this.TurnLength, this.SceneLength, this.Turns);
	}

	public float IncreaseHPAmount(float max)
	{
		return max * (1f / Convert.ToSingle(Math.Pow(this.HP,2f))); // The more the HP, the less it increases...
	}

    public override void ParseObjectField(JSONObject json)
    {
        if (json.GetValue("Creator") != null)
            this.Creator = BaseRepository.add<User>(json.GetValue("Creator"));
        JSONArray scores = json.GetArray("Scores");
        foreach (JSONValue item in scores)
            BaseRepository.add<Score>(item);
    }

	public int Points()
	{
		return 100;
	}

    public void LoadSprite(Sprite sprite)
    {
        if (this.SourceImages.Count < this.SourceImageCount)
            this.SourceImages.Add(sprite);
        else
            this.TargetImages.Add(sprite);
    }

    public bool SourceImagesLoaded()
    {
        return this.SourceImages.Count == this.SourceImageCount;
    }

    public bool TargetImagesLoaded()
    {
        return this.UseCustomTargetImages ? this.TargetImages.Count == this.TargetImageCount : this.SourceImagesLoaded();
    }

}
