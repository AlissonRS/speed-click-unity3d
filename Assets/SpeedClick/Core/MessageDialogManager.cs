using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageDialogManager : MonoBehaviour
{

	public float _messageCurrentLifeTime;

	private CanvasGroup canvasGroup;

	public bool show;

	public int fadeTime;

	public int messageLifeTime;

	private float alpha
	{
		get { return this.canvasGroup.alpha; }
		set
		{
			if (value > 1)
				this.canvasGroup.alpha = 1;
			else if (value < 0)
			{
				this.canvasGroup.alpha = 0;
				this.gameObject.SetActive(false);
			}
			else
				this.canvasGroup.alpha = value;
		}
	}

	private float MessageCurrentLifeTime
	{
		get { return this._messageCurrentLifeTime; }
		set
		{
			if (value >= this.messageLifeTime)
			{
				this.show = false;
				this._messageCurrentLifeTime = 0;
			}
			else
				this._messageCurrentLifeTime = value;
		}
	}
	
	public static MessageDialogManager instance;

	public Text message;

	public void Start()
	{
		if (instance == null)
			instance = this;
		instance.show = false;
		instance.fadeTime = 1;
		instance.messageLifeTime = 3;
		instance.MessageCurrentLifeTime = 0;
		instance.canvasGroup = instance.GetComponent<CanvasGroup>();
		instance.alpha = 0;
		instance.gameObject.SetActive(false);
	}

	public static void ShowDialog(string message)
	{
		instance.message.text = message;
		instance.gameObject.SetActive(true);
		instance.show = true;
	}

	public void Update()
	{
		if (instance == null) return;
		if (instance.alpha == 0 && !instance.show) // If it's invisible, we proceed only if we wanna show...
			return;
		if (instance.alpha == 1 && instance.show && instance.MessageCurrentLifeTime <= instance.messageLifeTime) // When message is fully shown, we start counting its current life time
		{
			instance.MessageCurrentLifeTime += Time.deltaTime; // When it surpasses the messageLimeTime, it sets itself to zero and change show flag to false, so we won't increase it anymore, keeping zero...
			return;
		}
		float value = (1 / instance.fadeTime * Time.deltaTime); // we calculate the value amount to be increased/decreased.
		if (instance.show)
			instance.alpha += value;
		else
			instance.alpha -= value;
	}

}

