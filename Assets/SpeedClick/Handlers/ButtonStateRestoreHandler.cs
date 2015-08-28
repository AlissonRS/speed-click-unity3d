using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonStateRestoreHandler : MonoBehaviour, IPointerClickHandler
{

	private Image buttonImage;
	private Text buttonText;
	private Color defaultTextColor;
	private Color defaultButtonColor;

    void OnEnable()
    {
        this.ResetProperties();
    }

	public void OnPointerClick (PointerEventData eventData)
	{
		this.ResetProperties();
	}
	
	private void ResetProperties()
	{
        if (this.buttonImage != null)
            this.buttonImage.color = defaultButtonColor;
        if (this.buttonText != null)
            this.buttonText.color = defaultTextColor;
	}

	void Start()
	{
		this.buttonImage = this.gameObject.GetComponent<Image>();
		this.buttonText = this.gameObject.GetComponentInChildren<Text>();
		if (this.buttonImage != null)
			this.defaultButtonColor = this.buttonImage.color;
		if (this.buttonText != null)
			this.defaultTextColor = this.buttonText.color;
	}

}

