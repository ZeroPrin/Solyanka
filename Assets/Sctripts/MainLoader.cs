using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MainLoader : MonoBehaviour
{
    private IEnumerator Start()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
