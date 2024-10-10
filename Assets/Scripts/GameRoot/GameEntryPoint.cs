using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    private static GameEntryPoint instance;
    private Coroutines coroutines;
    private UIRootView rootView;

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
    }
    private void StartGame()
    {
#if UNITY_EDITOR
        var sceneName=SceneManager.GetActiveScene().name;
        if (sceneName==ScenesName.GAMEPLAY)
        {
            coroutines.StartCoroutine(LoadAndStartGameplay());
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

        coroutines.StartCoroutine(LoadAndStartGameplay());
    }
    private IEnumerator LoadAndStartGameplay()
    {
        rootView.ShowLoadingScript();

        yield return LoadScene(ScenesName.BOOT);
        yield return LoadScene(ScenesName.GAMEPLAY);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint=Object.FindObjectOfType<GameplayEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToMainMenuSceneRequested += () =>
        {
            coroutines.StartCoroutine(LoadAndStartMainMenu());
        };

        rootView.HideLoadingScript();
    }
    private IEnumerator LoadAndStartMainMenu()
    {
        rootView.ShowLoadingScript();

        yield return LoadScene(ScenesName.BOOT);
        yield return LoadScene(ScenesName.MAINMENU);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToGameplaySceneRequested += () =>
        {
            coroutines.StartCoroutine(LoadAndStartGameplay());
        };

        rootView.HideLoadingScript();
    }
    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
