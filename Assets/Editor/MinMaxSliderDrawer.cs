using UnityEditor;
using UnityEngine;

/// <summary>
/// 引数として渡した値の間で、
/// Vector2型の最小値(x)と最大値(y)をスライダーで調整できるようにします
/// </summary>
[CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
public class MinMaxSliderDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        MinMaxSliderAttribute minMaxSlider = (MinMaxSliderAttribute)attribute;

        if (property.propertyType == SerializedPropertyType.Vector2)
        {
            Vector2 range = property.vector2Value;
            
            float minValue = range.x;
            float maxValue = range.y;
            
            float labelWidth = EditorGUIUtility.labelWidth;
            float fieldWidth = 40f;
            
            EditorGUI.LabelField(new Rect(position.x, position.y, labelWidth, position.height), label);
            
            float sliderX = position.x + labelWidth;
            float sliderWidth = position.width - labelWidth - (fieldWidth * 2) - 10;
            
            minValue = EditorGUI.FloatField(new Rect(sliderX, position.y, fieldWidth, position.height), minValue);
            EditorGUI.MinMaxSlider(new Rect(sliderX + fieldWidth + 5, position.y, sliderWidth, position.height), 
                ref minValue, ref maxValue, minMaxSlider.Min, minMaxSlider.Max);
            maxValue = EditorGUI.FloatField(new Rect(sliderX + fieldWidth + sliderWidth + 10, position.y, fieldWidth, position.height), maxValue);

            property.vector2Value = new Vector2(Mathf.Clamp(minValue, minMaxSlider.Min, maxValue), Mathf.Clamp(maxValue, minValue, minMaxSlider.Max));
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use MinMaxSlider with Vector2");
        }
    }
}
