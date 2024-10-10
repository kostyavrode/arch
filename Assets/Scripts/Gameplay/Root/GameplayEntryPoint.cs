using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{

    public event Action GoToMainMenuSceneRequested;

    [SerializeField] private GameObject sceneRootBinder;
    [SerializeField] private UIGameplayRootBinder uiBinder;

    public void Run(UIRootView uIRoot)
    {
        Debug.Log("Gameplay Scene Loaded");
        var ui = Instantiate(uiBinder);
        uIRoot.AttachSceneUI(ui.gameObject);

        ui.GoToMainMenuButtonClicked += () =>
        {
            GoToMainMenuSceneRequested?.Invoke();
        };
    }
}
