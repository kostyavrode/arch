using BaCon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameplayViewModelRegistrations
{
    public static void Register(DIContainer container)
    {
        container.RegisterFactory(x => new UIGameplayRootViewModel(container.Resolve<TestService>())).AsSingle();
        container.RegisterFactory(x => new WorldGameplayRootViewModel()).AsSingle();
    }
}
