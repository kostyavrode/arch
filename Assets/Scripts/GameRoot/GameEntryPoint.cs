using BaCon;
using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    private static GameEntryPoint instance;
    private Coroutines coroutines;
    private UIRootView rootView;

    private readonly DIContainer rootContainer= new();

    private DIContainer cachedSceneContainer;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutoStartGame()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout =SleepTimeout.NeverSleep;
        instance = new GameEntryPoint();
        instance.StartGame();
    }
    private GameEntryPoint()
    {
        coroutines=new GameObject(name:"[COROUTINES]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
        rootView = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad (rootView.gameObject);
        rootContainer.RegisterInstance(rootView);

        rootContainer.RegisterFactory(x=> new CommonService()).AsSingle();
    }
    private void StartGame()
    {
#if UNITY_EDITOR
        var sceneName=SceneManager.GetActiveScene().name;
        if (sceneName==ScenesName.GAMEPLAY)
        {

            var entryParams = new GameplayEntryParams("ddd.save", 1);
            coroutines.StartCoroutine(LoadAndStartGameplay(entryParams));
            return;
        }
        if (sceneName==ScenesName.MAINMENU)
        {
            coroutines.StartCoroutine(LoadAndStartMainMenu());
        }
        if (sceneName!=ScenesName.BOOT)
        {
            return;
        }

#endif

        coroutines.StartCoroutine(LoadAndStartMainMenu());
    }
    private IEnumerator LoadAndStartGameplay(GameplayEntryParams gameplayEntryParams)
    {
        rootView.ShowLoadingScript();
        cachedSceneContainer?.Dispose();

        yield return LoadScene(ScenesName.BOOT);
        yield return LoadScene(ScenesName.GAMEPLAY);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint=Object.FindObjectOfType<GameplayEntryPoint>();

        var gameplayContainer = cachedSceneContainer = new DIContainer(rootContainer);

        sceneEntryPoint.Run(gameplayContainer, gameplayEntryParams).Subscribe(gameplayExitParams =>
        {
            coroutines.StartCoroutine(LoadAndStartMainMenu(gameplayExitParams.MainMenuEntryParams));
        });

        rootView.HideLoadingScript();
    }
    private IEnumerator LoadAndStartMainMenu(MainMenuEntryParams mainMenuEntryParams = null)
    {
        rootView.ShowLoadingScript();
        cachedSceneContainer?.Dispose();

        yield return LoadScene(ScenesName.BOOT);
        yield return LoadScene(ScenesName.MAINMENU);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();
        var mainMenuContainer= cachedSceneContainer = new DIContainer(rootContainer);

        sceneEntryPoint.Run(mainMenuContainer,mainMenuEntryParams).Subscribe(mainMenuExitParams =>
        {
            var targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;

            if (targetSceneName==ScenesName.GAMEPLAY)
            {
                coroutines.StartCoroutine(LoadAndStartGameplay(mainMenuExitParams.TargetSceneEnterParams.As<GameplayEntryParams>()));
            }
        });


        rootView.HideLoadingScript();
    }
    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
