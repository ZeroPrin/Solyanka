using UnityEngine;
using UnityEngine.SceneManagement;
using static Enums;

public class SceneLoader
{
    private readonly AllScenesData _allScenesData;

    public SceneLoader(AllScenesData allScenesData)
    {
        _allScenesData = allScenesData;
    }

    public void LoadSceneByType(SceneType type)
    {
        string sceneName = _allScenesData.GetSceneNameByType(type);

        if (!string.IsNullOrEmpty(sceneName))
        {
            LoadSceneByName(sceneName);
        }
        else
        {
            Debug.LogError("Scene with type " + type + " could not be found!");
        }
    }

    public void LoadSceneByName(string sceneName)
    {
        if (CanLoadScene(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene with name " + sceneName + " not found!");
        }
    }

    public void LoadSceneById(int sceneId)
    {
        if (CanLoadScene(sceneId))
        {
            SceneManager.LoadScene(sceneId);
        }
        else
        {
            Debug.LogError("Scene with index " + sceneId + " not found!");
        }
    }

    private bool CanLoadScene(string sceneName)
    {
        return Application.CanStreamedLevelBeLoaded(sceneName);
    }

    private bool CanLoadScene(int sceneId)
    {
        return sceneId >= 0 && sceneId < SceneManager.sceneCountInBuildSettings;
    }
}
