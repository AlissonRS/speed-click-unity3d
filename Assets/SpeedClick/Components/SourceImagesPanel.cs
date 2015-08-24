using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class SourceImagesPanel : MonoBehaviour
{

    public static SourceImagesPanel instance;

	public HorizontalLayoutGroup upperContainer;
	public HorizontalLayoutGroup lowerContainer;
	
	public HorizontalLayoutGroup upperImages;
	public HorizontalLayoutGroup lowerImages;

    public List<Image> Images = new List<Image>();

    void Start()
    {
        if (instance == null)
            instance = this;
        this.ClearImages();
    }

	private void ClearImages()
	{
        Images.Clear();
		foreach (Transform child in this.upperImages.transform)
			GameObject.Destroy(child.gameObject);
		foreach (Transform child in this.lowerImages.transform)
			GameObject.Destroy(child.gameObject);
	}

	public void LoadImages(List<Sprite> imgs)
	{
		this.ClearImages();

        bool useUpperPanel = imgs.Count > Constants.MAX_SOURCE_IMAGES / 2;
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
                if (i < Constants.MAX_SOURCE_IMAGES / 2)
					sourceImagePrefab.transform.SetParent(upperImages.transform, false);
				else
					sourceImagePrefab.transform.SetParent(lowerImages.transform, false);
			}
			else
				sourceImagePrefab.transform.SetParent(lowerImages.transform, false);


            Images.Add(handler.childImage);
			i++;
		}
	}

}

