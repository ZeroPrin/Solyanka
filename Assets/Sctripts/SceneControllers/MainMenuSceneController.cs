using UnityEngine;
using Zenject;

public class MainMenuSceneController : IInitializable
{
    private SceneLoader _sceneLoader;
    private UIManager _uiManager;

    [Inject]
    public void Construct(SceneLoader sceneLoader, UIManager uiManager)
    {
        _sceneLoader = sceneLoader;
        _uiManager = uiManager;
    }

    public void Initialize()
    {
        _uiManager.OpenScreen(Enums.ScreenType.MainMenuScreen);
        ((MainMenuScreen)_uiManager.CurrentScreen).GameplayButton.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        _sceneLoader.LoadSceneByType(Enums.SceneType.Gameplay);
    }
}
