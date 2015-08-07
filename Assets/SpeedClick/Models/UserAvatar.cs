using UnityEngine;
using System.Collections;
using Alisson.Core;
using System;
using Alisson.Core.Repository;
using Assets.SpeedClick.Core;

public class UserAvatar: BaseObject
{

	public int UserID;
	public Sprite Avatar;
    
	public void Load(User user, Sprite Avatar)
	{
		this.UserID = user.ID;
		if (Avatar == null)
			this.LoadSprite();
	}

	private void LoadSprite()
	{
		string place = String.Format("Avatar/{0}", this.UserID.ToString("D8"));
		this.Avatar = Resources.Load<Sprite> (place); 
	}

	public static void LoadAllAvatar()
	{
		Sprite[] sprites = Resources.LoadAll <Sprite> ("Avatar"); 
		foreach (Sprite sprite in sprites)
		{
            User user = BaseRepository.getById<User>(Convert.ToInt32(sprite.name));
            UserAvatar userAvatar = BaseRepository.add<UserAvatar>();
            userAvatar.Load(user, sprite);
		}
	}

}

