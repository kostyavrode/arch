using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuExitParams : SceneExitParams
{
    public SceneEntryParams TargetSceneEnterParams { get; }

    public MainMenuExitParams(SceneEntryParams targetSceneEnterParams)
    {
        TargetSceneEnterParams = targetSceneEnterParams;
    }
}
