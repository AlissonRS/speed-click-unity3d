using UnityEngine;
using System.Collections;
using Alisson.Core;
using Alisson.Core.Repository;
using System.Linq;
using System.Collections.Generic;
using Boomlagoon.JSON;
using System;
using Assets.SpeedClick.Core;

public class User : BaseObject
{

	public string Login;

	public Sprite GetAvatar()
	{
        IEnumerable<UserAvatar> list = BaseRepository.getAll<UserAvatar>().Where(a => a.UserID == this.ID);
		if (list.Count() == 0)
        {
            UserAvatar avat = BaseRepository.add<UserAvatar>();
            avat.Load(this, null);
            return avat.Avatar;
        }
		else
			return list.First().Avatar;
	}

}

