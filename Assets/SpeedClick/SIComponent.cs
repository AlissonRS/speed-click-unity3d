using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class SIComponent : MonoBehaviour, IPointerClickHandler {

	private SIComponent _parent;

	public Commands OnEscape;
	public Commands OnClick;
	public int TabOrder;
	public SIComponent Parent {
		get
		{	if (this._parent == null)
				throw new MissingReferenceException(this.gameObject.name + " does not contain a SIComponent parent object.");
			else
				return this._parent;
		}
		set
		{ this._parent = value; }
	}

	private Dictionary<string, object> data = new Dictionary<string, object>();

	private List<SIComponent> children;

	public void Start()
	{
		this.Parent = this.GetComponentInParent<SIComponent>();
		children = this.GetComponentsInChildren<SIComponent>().ToList();
	}

	public SpeedImagerScreen DiscoverOwnerScreen()
	{
		SpeedImagerScreen screen = this.gameObject.GetComponent<SpeedImagerScreen>();
		if (screen != null)
			return screen;
		return this.Parent.DiscoverOwnerScreen();
	}

	public void DoTab()
	{
		SIComponent nextSibling = this.GetNextVisibleSibling();
		if (nextSibling != null)
			nextSibling.SetFocus();
	}

	public SIComponent GetNextVisibleSibling()
	{
		SIComponent sibling = this.Parent.children.Where(s => s.TabOrder > this.TabOrder && s.IsVisible()).OrderBy(c => c.TabOrder).First();
		if (sibling != null)
			return sibling;
		else
			return this.Parent.children.Where(s => s.TabOrder <= this.TabOrder && s.IsVisible()).OrderBy(c => c.TabOrder).First();
	}

	public T GetData<T>(string key)
	{
		return (T) this.data[key];
	}

	public bool IsVisible()
	{
		CanvasGroup cv = this.gameObject.GetComponent<CanvasGroup>();
		return this.gameObject.activeInHierarchy && cv != null && cv.alpha > 0;
	}

	private Command GetCommand(Commands c)
	{
		Type type;
		switch (c)
		{
		case Commands.ExitGame: type = typeof(ExitGameCommand); break;
		case Commands.PauseGame: type = typeof(PauseGameCommand); break;
		case Commands.LoadScene: type = typeof(LoadSceneCommand); break;
		case Commands.Login: type = typeof(LoginCommand); break;
		case Commands.ShowScreen: type = typeof(ShowScreenCommand); break;
		case Commands.RegisterUser: type = typeof(RegisterUserCommand); break;
		default: return new Command();
		}
		return (Command) this.gameObject.GetComponent(type) ?? (Command) this.gameObject.AddComponent(type);
	}

	public Command LoadCommand<T>() where T: Command
	{
		return (Command) this.gameObject.GetComponent<T>() ?? this.gameObject.AddComponent<T>();
	}

	public void SetData<T>(string key, T data)
	{
		this.data[key] = (object) data;
	}

	public void SetFocus()
	{
		EventSystem.current.SetSelectedGameObject(this.gameObject);
	}

	public void OnGUI()
	{
		if (Event.current.type == EventType.KeyUp)
		{
			if (OnEscape == Commands.Undefined) return;
			switch (Event.current.keyCode)
			{
			case KeyCode.Escape: StartCoroutine(this.GetCommand(OnEscape).Execute(this)); break;
			default: break;
			}
		}
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if (OnClick == Commands.Undefined || !this.gameObject.activeInHierarchy)
			return;
		StartCoroutine(this.GetCommand(OnClick).Execute(this));
	}
}
