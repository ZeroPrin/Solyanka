using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void LoadSceneByName(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
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
        if (sceneId >= 0 && sceneId < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneId);
        }
        else
        {
            Debug.LogError("Scene with index " + sceneId + " not found!");
        }
    }
}
