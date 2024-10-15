using BaCon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MainMenuRegistrations 
{
    public static void Register(DIContainer container, MainMenuEntryParams mainMenuEntryParams)
    {
        container.RegisterFactory(x => new MenuCommonService(container.Resolve<CommonService>())).AsSingle();
    }
}
