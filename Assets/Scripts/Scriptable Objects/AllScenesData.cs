using System.IO;
using UnityEditor;
using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "AllScenesData", menuName = "Scriptable Objects/All Scenes Data")]
public class AllScenesData : ScriptableObject
{
    [SerializeField] private SceneData[] _scenes;

    public string GetSceneNameByType(SceneType type)
    {
        foreach (var scene in _scenes)
        {
            if (scene.SceneType == type)
            {
                string scenePath = AssetDatabase.GetAssetPath(scene.SceneAsset);

                string sceneName = Path.GetFileNameWithoutExtension(scenePath);

                return sceneName;
            }
        }

        Debug.LogError("Scene with type " + type + " not found!");

        return null;
    }
}
