using BaCon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MainMenuViewModelRegistrations
{
    public static void Register(DIContainer container)
    {
        container.RegisterFactory(x => new UIMainMenuViewModel()).AsSingle();
    }
}
