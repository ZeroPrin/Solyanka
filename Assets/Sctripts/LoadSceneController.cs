using System.Collections;
using UnityEngine;
using Zenject;

public class LoadSceneController : IInitializable
{
    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void Initialize()
    {
        _sceneLoader.LoadSceneByType(Enums.SceneType.Main);
    }
}