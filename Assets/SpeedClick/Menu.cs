using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{

	private Animator _animator;
	private CanvasGroup _canvasGroup;

	public bool IsOpen
	{
		get { return this._animator.GetBool("IsOpen"); }
		set { this._animator.SetBool("IsOpen", value); }
	}

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_canvasGroup = GetComponent<CanvasGroup>();

	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("IsOpen"))
//			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
//		else
//			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
	}
}

