#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>
/// Inspector に進捗バーを表示します
/// 使用できる型はfloat型かint型です
/// </summary>
[CustomPropertyDrawer(typeof(ProgressBarAttribute))]
public class ProgressBarDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 2 + 4; // 通常のフィールド + バーの高さ
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ProgressBarAttribute progressBar = (ProgressBarAttribute)attribute;

        if (property.propertyType != SerializedPropertyType.Float && property.propertyType != SerializedPropertyType.Integer)
        {
            EditorGUI.LabelField(position, label.text, "Use [ProgressBar] with float or int.");
            return;
        }

        float value = property.propertyType == SerializedPropertyType.Float ? property.floatValue : property.intValue;
        float min = progressBar.Min;
        float max = progressBar.Max;

        float progress = Mathf.InverseLerp(min, max, value); // 0~1 の範囲に正規化
        string progressLabel = string.IsNullOrEmpty(progressBar.Label) ? $"{value} / {max}" : progressBar.Label;

        // 数値フィールドを描画
        Rect valueRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(valueRect, property, label);

        // 進捗バーの位置とサイズ
        Rect barRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, EditorGUIUtility.singleLineHeight);

        // バーの色を変える（緑 → 黄 → 赤）
        Color barColor = Color.Lerp(Color.red, Color.green, progress);
        EditorGUI.DrawRect(barRect, new Color(0.2f, 0.2f, 0.2f, 1f)); // 背景色
        EditorGUI.DrawRect(new Rect(barRect.x, barRect.y, barRect.width * progress, barRect.height), barColor);

        // 進捗ラベル
        EditorGUI.LabelField(barRect, progressLabel, new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter, normal = { textColor = Color.white } });
    }
}
#endif
