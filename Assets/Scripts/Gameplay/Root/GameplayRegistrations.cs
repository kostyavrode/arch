using BaCon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameplayRegistrations 
{
    public static void Register(DIContainer container, GameplayEntryParams gameplayEntryParams)
    {
        container.RegisterFactory(x => new TestService(container.Resolve<CommonService>())).AsSingle();
    }
}
