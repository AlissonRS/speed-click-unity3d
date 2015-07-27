using UnityEngine;
using System.Collections;
using Alisson.Core;
using Alisson.Core.Repository;
using System.Linq;
using System.Collections.Generic;
using Boomlagoon.JSON;

public class User : BaseObject
{

	public string Login;
	
	public User(JSONValue value) : base(value) { }

	public User(int id, string login)
	{
		this.ID = id;
		this.Login = login;
	}

	public Sprite GetAvatar()
	{
		IEnumerable<UserAvatar> list = BaseRepository<UserAvatar>.getAll().Where(a => a.UserID == this.ID);
		if (list.Count() == 0)
			return BaseRepository<UserAvatar>.add(new UserAvatar(this, null)).Avatar;
		else
			return list.First().Avatar;
	}

}

