#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;

[CustomEditor(typeof(BaseMonoBehaviour), true)]
[CanEditMultipleObjects]
public class MainEditor : Editor
{
    private string lastHeader = null;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var mono = (MonoBehaviour)target;
        var type = mono.GetType();

        DrawSerializedFields(type);

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space(10);

        DrawMethods(type, mono);
    }

    private void DrawSerializedFields(Type type)
    {
        lastHeader = null;

        SerializedProperty property = serializedObject.GetIterator();
        bool expanded = property.NextVisible(true);

        if (!expanded) return;

        do
        {
            if (property.name == "m_Script")
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.PropertyField(property, true);
                }
                continue;
            }

            FieldInfo fieldInfo = type.GetField(
                property.name,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
            );

            var headerAttribute = fieldInfo != null
                ? (SectionHeaderAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(SectionHeaderAttribute))
                : null;

            if (headerAttribute != null && headerAttribute.Title != lastHeader)
            {
                lastHeader = headerAttribute.Title;
                DrawCenteredLabel(lastHeader);
            }

            EditorGUILayout.PropertyField(property, true);

        } while (property.NextVisible(false));
    }

    private void DrawMethods(Type type, MonoBehaviour mono)
    {
        lastHeader = null;

        var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var method in methods)
        {
            var headerAttribute = (SectionHeaderAttribute)Attribute.GetCustomAttribute(method, typeof(SectionHeaderAttribute));
            if (headerAttribute != null && headerAttribute.Title != lastHeader)
            {
                lastHeader = headerAttribute.Title;
                DrawCenteredLabel(lastHeader);
            }

            var buttonAttribute = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));
            if (buttonAttribute != null)
            {
                if (GUILayout.Button(method.Name))
                {
                    method.Invoke(mono, null);
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
            fontSize = 12
        };
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField(text, style);
        EditorGUILayout.Space(5);
    }
}
#endif