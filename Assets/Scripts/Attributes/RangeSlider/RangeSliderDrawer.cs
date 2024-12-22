using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RangeSliderAttribute))]
public class RangeSliderDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        RangeSliderAttribute rangeSlider = (RangeSliderAttribute)attribute;

        if (property.propertyType != SerializedPropertyType.Vector2)
        {
            EditorGUI.LabelField(position, label.text, "Use RangeSlider with Vector2.");
            return;
        }

        Vector2 range = property.vector2Value;

        float labelWidth = EditorGUIUtility.labelWidth;
        float fieldWidth = 50f;
        float sliderWidth = position.width - labelWidth - fieldWidth * 2 - 10f;

        var labelRect = new Rect(position.x, position.y, labelWidth, position.height);
        var minFieldRect = new Rect(position.x + labelWidth, position.y, fieldWidth, position.height);
        var sliderRect = new Rect(position.x + labelWidth + fieldWidth + 5f, position.y, sliderWidth, position.height);
        var maxFieldRect = new Rect(position.x + labelWidth + fieldWidth + sliderWidth + 10f, position.y, fieldWidth, position.height);

        EditorGUI.LabelField(labelRect, label);

        range.x = EditorGUI.FloatField(minFieldRect, range.x);
        range.y = EditorGUI.FloatField(maxFieldRect, range.y);

        range.x = Mathf.Clamp(range.x, rangeSlider.MinLimit, rangeSlider.MaxLimit);
        range.y = Mathf.Clamp(range.y, rangeSlider.MinLimit, rangeSlider.MaxLimit);

        if (range.y < range.x)
        {
            range.y = range.x;
        }

        EditorGUI.MinMaxSlider(sliderRect, ref range.x, ref range.y, rangeSlider.MinLimit, rangeSlider.MaxLimit);

        property.vector2Value = range;
    }
}
