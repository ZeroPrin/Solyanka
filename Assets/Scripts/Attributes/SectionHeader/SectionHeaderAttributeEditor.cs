#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(MonoBehaviour), true)]
public class SectionHeaderAttributeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var monoBehaviour = (MonoBehaviour)target;
        var type = monoBehaviour.GetType();

        // Переменные
        SerializedProperty property = serializedObject.GetIterator();
        bool isFirst = true;

        string lastHeader = null;

        while (property.NextVisible(isFirst))
        {
            isFirst = false;

            // Получаем атрибут SectionHeader, если он есть
            var fieldInfo = type.GetField(property.name, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            var headerAttribute = fieldInfo != null ? (SectionHeaderAttribute)System.Attribute.GetCustomAttribute(fieldInfo, typeof(SectionHeaderAttribute)) : null;

            if (headerAttribute != null && headerAttribute.Title != lastHeader)
            {
                lastHeader = headerAttribute.Title;
                DrawCenteredLabel(headerAttribute.Title);
            }

            EditorGUILayout.PropertyField(property, true);
        }

        serializedObject.ApplyModifiedProperties();

        // Разделение блоков
        EditorGUILayout.Space(10);

        // Методы
        lastHeader = null;
        var methods = type.GetMethods(
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Public);

        foreach (var method in methods)
        {
            // Проверяем наличие SectionHeader для метода
            var headerAttribute = (SectionHeaderAttribute)System.Attribute.GetCustomAttribute(method, typeof(SectionHeaderAttribute));
            if (headerAttribute != null && headerAttribute.Title != lastHeader)
            {
                lastHeader = headerAttribute.Title;
                DrawCenteredLabel(headerAttribute.Title);
            }

            // Проверяем наличие атрибута Button
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

    private void DrawCenteredLabel(string text)
    {
        var style = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            fontSize = 14
        };
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField(text, style);
        EditorGUILayout.Space(5);
    }
}
#endif