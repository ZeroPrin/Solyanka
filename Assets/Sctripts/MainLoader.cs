using System.Collections;
using UnityEngine;
using Zenject;

public class MainLoader : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
        Debug.Log("SceneLoader успешно инъектирован в MainLoader");
    }

    private void Awake()
    {
        Debug.Log("MainLoader Awake");
    }

    private void Start()
    {
        if (_sceneLoader == null)
        {
            Debug.LogError("SceneLoader не был инъектирован!");
            return;
        }

        Debug.Log("MainLoader Start - Загружаем сцену...");
        _sceneLoader.LoadSceneByName("Main");
    }
}
