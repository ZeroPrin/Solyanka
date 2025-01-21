using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "AllScenesData", menuName = "Scriptable Objects/All Scenes Data")]
public class AllScenesData : ScriptableObject
{
    [SerializeField] private SceneData[] _scenes;

    public string GetSceneNameByType(SceneType type)
    {
        foreach (SceneData scene in _scenes)
        {
            if (scene.SceneType == type)
            {
                return scene.SceneName;
            }
        }

        Debug.LogError("Scene with type " + type + " not found!");
        return null;
    }
}