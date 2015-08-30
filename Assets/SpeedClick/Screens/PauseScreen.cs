using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScreen : SpeedClickScreen {

    public GameStatusDirector director;

    public override void OnEscape()
    {
        director.OnEscape();
    }

}
