using UnityEngine;
using System.IO;

public class MaskMapGenerator : MonoBehaviour
{
    [Header("Input Textures")]
    public Texture2D _metallicMap;
    public Texture2D _aoMap;
    public Texture2D _roughnessMap;

    [Header("Output Settings")]
    public string _outputFileName = "Generated_MaskMap";

    [Button]
    public void GenerateMaskMap()
    {
        if (_aoMap == null || _roughnessMap == null)
        {
            Debug.LogError("Ошибка: AO Map и Roughness Map должны быть заданы");
            return;
        }

        int width = _aoMap.width;
        int height = _aoMap.height;
        Texture2D maskMap = new Texture2D(width, height, TextureFormat.RGBA32, false);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float r = _metallicMap ? _metallicMap.GetPixel(x, y).r : 0f;
                float g = _aoMap ? _aoMap.GetPixel(x, y).r : 1f;
                float b = 1f; 
                float a = _roughnessMap ? 1f - _roughnessMap.GetPixel(x, y).r : 1f;

                maskMap.SetPixel(x, y, new Color(r, g, b, a));
            }
        }

        maskMap.Apply();

        string folderPath = Application.dataPath + "/Textures/";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = folderPath + _outputFileName + ".png";
        File.WriteAllBytes(filePath, maskMap.EncodeToPNG());

        Debug.Log($"Mask Map сохранена: {filePath}");

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}
