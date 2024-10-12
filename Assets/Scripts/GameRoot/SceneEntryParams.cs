using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneEntryParams
{
    public string SceneName {get;}

    public SceneEntryParams(string sceneName)
    {
        SceneName = sceneName;
    }

    public T As<T>() where T : SceneEntryParams
    {
        return (T)this;
    }
}
