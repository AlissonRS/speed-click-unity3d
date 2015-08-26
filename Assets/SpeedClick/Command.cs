using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Command : MonoBehaviour
{

    private Dictionary<string, object> data = new Dictionary<string, object>();

    public T GetData<T>(string key)
    {
        return (T)this.data[key];
    }

    public void SetData<T>(string key, T data)
    {
        this.data[key] = data;
    }

    public void Execute()
    {
        StartCoroutine(ExecuteAsCoroutine());
    }

    public virtual IEnumerator ExecuteAsCoroutine() { yield break; }

}

