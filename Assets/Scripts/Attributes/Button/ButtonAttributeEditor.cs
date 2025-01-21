#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonoBehaviour), true)]
public class ButtonAttributeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var monoBehaviour = (MonoBehaviour)target;

        var type = monoBehaviour.GetType();

        var methods = type.GetMethods(
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Public);

        foreach (var method in methods)
        {
            var buttonAttribute = (ButtonAttribute)System.Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));
            if (buttonAttribute != null)
            {
                if (GUILayout.Button(method.Name))
                {
                    method.Invoke(monoBehaviour, null);
                }
            }
        }
    }
}
#endif