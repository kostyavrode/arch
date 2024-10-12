using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEntryParams
{
    public string Result {  get; private set; }

    public MainMenuEntryParams(string result)
    {
        Result=result;
    }
}
