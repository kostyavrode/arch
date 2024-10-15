using BaCon;
using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{

    //public event Action GoToMainMenuSceneRequested;

    [SerializeField] private GameObject sceneRootBinder;
    [SerializeField] private UIGameplayRootBinder uiBinder;

    public Observable<GameplayExitParams> Run(DIContainer container, GameplayEntryParams gameplayEntryParams)
    {
        GameplayRegistrations.Register(container, gameplayEntryParams);

        var gameplayViewModelsContainer = new DIContainer(container);
        GameplayViewModelRegistrations.Register(gameplayViewModelsContainer);

        gameplayViewModelsContainer.Resolve<UIGameplayRootViewModel>(); 
        gameplayViewModelsContainer.Resolve<WorldGameplayRootViewModel>();

        var uiRoot=container.Resolve<UIRootView>();
        Debug.Log("Gameplay Scene Loaded");
        var ui = Instantiate(uiBinder);
        uiRoot.AttachSceneUI(ui.gameObject);

        var exitSceneSignalSubject = new Subject<Unit>();

        ui.Bind(exitSceneSignalSubject);

        Debug.Log($"GamePlay save file name = {gameplayEntryParams.SaveFileName}, level to load = {gameplayEntryParams.LevelNumber}  ");

        var mainMenuEntryParams= new MainMenuEntryParams("Finish");
        var exitParams = new GameplayExitParams(mainMenuEntryParams);
        var exitToMainMenuSceneSignal = exitSceneSignalSubject.Select(x => exitParams);

        return exitToMainMenuSceneSignal;
    }
}
