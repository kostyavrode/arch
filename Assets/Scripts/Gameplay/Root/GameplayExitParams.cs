using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayExitParams
{
    public MainMenuEntryParams MainMenuEntryParams { get; private set; }
    
    public GameplayExitParams(MainMenuEntryParams mainMenuEntryParams)
    {
        MainMenuEntryParams = mainMenuEntryParams;
    }
}
