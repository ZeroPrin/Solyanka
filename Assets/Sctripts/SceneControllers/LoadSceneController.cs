using System.Collections;
using UnityEngine;
using Zenject;

public class LoadSceneController : IInitializable
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
        _uiManager.OpenScreen(Enums.ScreenType.LoadScreen);
        
        _sceneLoader.LoadSceneByType(Enums.SceneType.Main);
    }
}