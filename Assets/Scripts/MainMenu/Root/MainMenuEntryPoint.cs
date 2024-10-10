using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{

    public event Action GoToGameplaySceneRequested;

    [SerializeField] private GameObject sceneRootBinder;
    [SerializeField] private UIMainMenuRootBinder uiBinder;

    public void Run(UIRootView uIRoot)
    {
        Debug.Log("Gameplay Scene Loaded");
        var ui = Instantiate(uiBinder);
        uIRoot.AttachSceneUI(ui.gameObject);

        ui.GoToGameplayButtonClicked += () =>
        {
            GoToGameplaySceneRequested?.Invoke();
        };
    }
}
