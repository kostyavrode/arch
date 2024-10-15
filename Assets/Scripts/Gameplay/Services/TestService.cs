using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaCon;
using System;

public class TestService: IDisposable
{
    private readonly CommonService commonService;

    public TestService(CommonService commonService)
    {
        this.commonService = commonService;
        Debug.Log(GetType().Name + " has been created");
    }

    public void Dispose()
    {
        Debug.Log("Подписки очищены");
    }
}
