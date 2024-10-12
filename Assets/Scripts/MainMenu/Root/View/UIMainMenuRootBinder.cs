using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenuRootBinder : MonoBehaviour
{
    private Subject<Unit> exitSceneSignalSubject;

    public void HandleGoToGameplayButtonClick()
    {
        exitSceneSignalSubject?.OnNext(Unit.Default);
    }

    public void Bind(Subject<Unit> exitSceneSignalSubject)
    {
        this.exitSceneSignalSubject = exitSceneSignalSubject;
    }
}
