using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour, IPointerClickHandler
{

	private Command comm;

	private Image buttonImage;
	private Text buttonText;
	private Color defaultTextColor;
	private Color defaultButtonColor; // teste

	public void OnPointerClick (PointerEventData eventData)
	{
		this.ResetProperties();
		if (comm != null)
			comm.Execute();
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
		this.comm = this.gameObject.GetComponent<Command>();
		this.buttonImage = this.gameObject.GetComponent<Image>();
		this.buttonText = this.gameObject.GetComponentInChildren<Text>();
		if (this.buttonImage != null)
			this.defaultButtonColor = this.buttonImage.color;
		if (this.buttonText != null)
			this.defaultTextColor = this.buttonText.color;
	}

}

