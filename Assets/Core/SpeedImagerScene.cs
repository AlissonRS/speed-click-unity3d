using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEditor;
using System.Linq;

public class SpeedImagerScene {

	public int ID;
	public string Name;
	public double HP; // How fast the HP decreases...
	public double ImageChangeRate; // How long in milisecs it takes to change the turn...
	public int Turns; // How much turns the player has to play before we change the source images...
	public int TurnDuration; // In secs...
	public int SceneDuration; // In secs...
	
	private List<Sprite> _images;

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
		this.LoadImages();
	}

	private void LoadImages()
	{
		string place = String.Format("SceneImages/{000}", this.ID);
		UnityEngine.Object[] sprites =(UnityEngine.Object[])AssetDatabase.LoadAllAssetsAtPath(place);
		_images = (List<Sprite>) sprites.ToList();
	}

}
