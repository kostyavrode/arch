using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEntryParams : SceneEntryParams
{
    public string SaveFileName { get; }
    public int LevelNumber { get; }
    public GameplayEntryParams(string saveFileName, int levelNumber) : base(ScenesName.GAMEPLAY)
    {
        SaveFileName = saveFileName;
        LevelNumber = levelNumber;
    }
}
