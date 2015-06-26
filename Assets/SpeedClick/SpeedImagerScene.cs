﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;

public class SpeedImagerScene {

	public int ID;
	public string Name;
	public float HP; // How fast the HP decreases...
	public int Turns; // How much turns the player has to play before we change the source images...
	public float TurnDuration; // In secs...
	public int SceneDuration; // In secs...
	
	private List<Sprite> _images = new List<Sprite>();

	public List<Sprite> Images
	{
		get {
			if (this._images.Count == 0)
				this.LoadImages();
			return this._images;
		}
	}

	public SpeedImagerScene(int ID)
	{
		this.ID = ID;
	}
	
	public float DecreaseHPAmount(float max)
	{
		return max * (this.HP / 10f * 0.3f); // The more the HP, the more it decreases...
	}

	public float IncreaseHPAmount(float max)
	{
		return max * (1f / Convert.ToSingle(Math.Pow(this.HP,2f))); // The more the HP, the less it increases...
	}

	private void LoadImages()
	{
//		if (Application.platform == RuntimePlatform.Android)
			LoadImagesFromAssets();
//		else
//			LoadImagesFromDir();
	}

	private void LoadImagesFromAssets()
	{
		string place = String.Format("Scenes/{000}", this.ID.ToString("D3"));
		Sprite[] sprites = Resources.LoadAll <Sprite> (place); 
		foreach (Sprite sprite in sprites) {
			_images.Add(sprite);
		}
//		_images = (List<Sprite>) sprites.ToList();
	}

//	private void LoadImagesFromDir()
//	{
//		string path = String.Format("c:/SpeedImager/Scenes/{0}/", this.ID.ToString("D3"));
//		string url = String.Format("file:///c:/SpeedImager/Scenes/{0}/", this.ID.ToString("D3"));
//		DirectoryInfo dir = new DirectoryInfo(path);
//		FileInfo[] info = dir.GetFiles("*.*");
//		foreach (FileInfo f in info) 
//		{
//			// Start a download of the given URL
//			WWW www = new WWW (url + f.Name);
//			// Wait for download to complete
//			//			yield www;
//			_images.Add(Sprite.Create(www.texture, new Rect(0,0,www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f)));
//		}
//	}

	public int Points()
	{
		return 100;
	}

}