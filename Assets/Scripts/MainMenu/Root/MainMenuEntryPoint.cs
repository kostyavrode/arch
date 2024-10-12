using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{

    [SerializeField] private GameObject sceneRootBinder;
    [SerializeField] private UIMainMenuRootBinder uiBinder;

    public Observable<MainMenuExitParams> Run(UIRootView uIRoot, MainMenuEntryParams mainMenuEntryParams)
    {



        Debug.Log("Gameplay Scene Loaded");
        var ui = Instantiate(uiBinder);
        uIRoot.AttachSceneUI(ui.gameObject);
        Debug.Log("Main Menu+ " + mainMenuEntryParams?.Result);

        var exitSignalSubject = new Subject<Unit>();
        ui.Bind(exitSignalSubject);


        var safeFileName = "olo.save";
        var levelNumber = 3;
        var gameplayEnterParams = new GameplayEntryParams(safeFileName, levelNumber);
        var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);

        var exitToGameplaySceneSignal = exitSignalSubject.Select(x => mainMenuExitParams);
        return exitToGameplaySceneSignal;
    }
}
