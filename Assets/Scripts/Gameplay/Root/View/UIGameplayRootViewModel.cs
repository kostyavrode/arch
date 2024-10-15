using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameplayRootViewModel
{
    private readonly TestService testService;
    public UIGameplayRootViewModel(TestService service)
    {
        testService = service;
    }
}
