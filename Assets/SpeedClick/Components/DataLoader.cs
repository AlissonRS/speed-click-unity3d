using UnityEngine;
using System.Collections;
using Alisson.Core;
using Alisson.Core.Database;
using Assets.SpeedClick.Core;

public class DataLoader : MonoBehaviour {

    public ServerManager server;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(LoadScenes());
	}

    IEnumerator LoadScenes()
    {
        yield return StartCoroutine(BaseRepository.getAllFresh<Scene>());
        foreach (Scene scene in BaseRepository.getAll<Scene>())
            ScenesContainer.instance.AddScene(scene);
    }

}
