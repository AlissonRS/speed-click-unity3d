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
		this.lowerContainer.gameObject.SetActive(false);

		List<Image> result = new List<Image>();

		int i = 0;
		foreach(Sprite img in imgs)
		{
			GameObject sourceImagePrefab = (GameObject) Instantiate(Resources.Load("SourceImage"));
			Image srcImage = (Image) sourceImagePrefab.GetComponentInChildren(typeof(Image));
			SourceImageHandler handler = (SourceImageHandler) sourceImagePrefab.GetComponentInChildren(typeof(SourceImageHandler));
			handler.Index = i;
			srcImage.sprite = img;
			sourceImagePrefab.name = String.Format("sourceImage_{0}",i);
			if (i < maxImages / 2)
				sourceImagePrefab.transform.SetParent(upperImages.transform, false);
			else
			{
				lowerContainer.gameObject.SetActive(true);
				sourceImagePrefab.transform.SetParent(lowerImages.transform, false);
			}

			result.Add(srcImage);
			i++;
		}
		return result;
	}

}

