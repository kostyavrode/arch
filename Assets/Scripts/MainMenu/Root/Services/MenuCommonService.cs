using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCommonService
{
    private readonly CommonService commonService;

    public MenuCommonService(CommonService commonService)
    {
        this.commonService = commonService;
        Debug.Log(GetType().Name + " has been created");
    }
}
