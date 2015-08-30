using Alisson.Core;
using Assets.SpeedClick.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UserAvatarLoader: MonoBehaviour
{

    public static UserAvatarLoader instance;

    public ServerManager server;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Load(User user)
    {
        StartCoroutine(server.LoadImageIntoSprite(user.AvatarUrl, user.LoadSprite));
    }

    public void LoadAll()
    {
        IEnumerable<User> users = BaseRepository.getAll<User>().Where(u => u.AvatarUrl != "");
        foreach (User user in users)
            Load(user);
    }

}