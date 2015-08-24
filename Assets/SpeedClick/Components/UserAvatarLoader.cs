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

    public ServerManager server;

    public IEnumerator Load(User user)
    {
        string url = String.Format("users/{0}.png", user.ID.ToString("D8"));
        yield return StartCoroutine(server.LoadImageIntoSprite(user, url));
    }

    public IEnumerator LoadAll()
    {
        IEnumerable<User> users = BaseRepository.getAll<User>().Where(u => !u.HasOwnAvatar);
        foreach (User user in users)
            yield return StartCoroutine(Load(user));
    }

}