using UnityEngine;
using System.Collections;
using Alisson.Core;
using System.Linq;
using System.Collections.Generic;
using Boomlagoon.JSON;
using System;
using Assets.SpeedClick.Core;

public class User : BaseObject, ISubject<User>, ISpritable
{

    public string Login;
    public int Ranking;
    public int Score;
    public Sprite Avatar;
    public bool HasOwnAvatar = false;

    private IList<IObserver<User>> _observers;

    public IList<IObserver<User>> Observers
    {
        get
        {
            return _observers ?? (_observers = new List<IObserver<User>>());
        }
    }

    private static Sprite _unknownAvatar;

    private static Sprite UnknownAvatar
    {
        get
        {
            if (_unknownAvatar == null)
            {
                string place = String.Format("Avatar/00000000");
                _unknownAvatar = Resources.Load<Sprite>(place);
            }
            return _unknownAvatar;
        }
    } // Avatar used by users who don't have an avatar...
    
    public void Start()
    {
        if (UserAvatarLoader.instance != null)
            StartCoroutine(UserAvatarLoader.instance.Load(this));
    }

	public Sprite GetAvatar()
	{
		if (this.Avatar == null)
            this.Avatar = UnknownAvatar;
        return this.Avatar;
	}

    public void Notify()
    {
        foreach (var vo in Observers)
        {
            vo.UpdateObserver(this);
        }
    }

    public void Subscribe(IObserver<User> observer)
    {
        Observers.Add(observer);
    }

    public void Unsubscribe(IObserver<User> observer)
    {
        Observers.Remove(observer);
    }

    public User Element
    {
        get { return this; }
    }

    public void LoadSprite(Sprite sprite)
    {
        this.Avatar = sprite;
        if (this.Avatar != null)
        {
            this.HasOwnAvatar = true;
            this.Notify();
        }
    }
}

