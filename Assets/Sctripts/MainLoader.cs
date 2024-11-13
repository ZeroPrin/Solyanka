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
        Debug.Log("SceneLoader ������� ������������ � MainLoader");
    }

    private void Awake()
    {
        Debug.Log("MainLoader Awake");
    }

    private void Start()
    {
        if (_sceneLoader == null)
        {
            Debug.LogError("SceneLoader �� ��� ������������!");
            return;
        }

        Debug.Log("MainLoader Start - ��������� �����...");
        _sceneLoader.LoadSceneByName("Main");
    }
}
