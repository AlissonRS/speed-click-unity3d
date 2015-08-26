using UnityEngine;
using System.Collections;

public class PauseScreen : SpeedClickScreen {

    public GameStatusDirector director;

    public override void OnEscape()
    {
        director.Resume();
    }

}
