using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class SourceImagesPanel : MonoBehaviour
{

	public HorizontalLayoutGroup upperContainer;
	public HorizontalLayoutGroup lowerContainer;
	
	public HorizontalLayoutGroup upperImages;
	public HorizontalLayoutGroup lowerImages;

	private static int maxImages = 10;

	private void ClearImages()
	{
		foreach (Transform child in this.upperImages.transform)
			GameObject.Destroy(child.gameObject);
		foreach (Transform child in this.lowerImages.transform)
			GameObject.Destroy(child.gameObject);
	}

	public List<Image> LoadImages(List<Sprite> imgs)
	{
		this.ClearImages();

		List<Image> result = new List<Image>();

		bool useUpperPanel = imgs.Count > maxImages / 2;
		this.upperImages.gameObject.SetActive(useUpperPanel);

		int i = 0;
		foreach(Sprite img in imgs)
		{
			GameObject sourceImagePrefab = (GameObject) Instantiate(Resources.Load("Prefabs/SourceImage"));
			SourceImageHandler handler = (SourceImageHandler) sourceImagePrefab.GetComponentInChildren(typeof(SourceImageHandler));
			handler.Index = i;
			handler.childImage.sprite = img;
			sourceImagePrefab.name = String.Format("sourceImage_{0}",i);
			if (useUpperPanel)
			{
				if (i < maxImages / 2)
					sourceImagePrefab.transform.SetParent(upperImages.transform, false);
				else
					sourceImagePrefab.transform.SetParent(lowerImages.transform, false);
			}
			else
				sourceImagePrefab.transform.SetParent(lowerImages.transform, false);


			result.Add(handler.childImage);
			i++;
		}
		return result;
	}

}

