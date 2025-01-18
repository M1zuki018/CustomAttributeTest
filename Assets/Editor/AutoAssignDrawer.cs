#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Reflection;

/// <summary>
/// アタッチされたオブジェクトから自動的にアサインします
/// </summary>
[CustomEditor(typeof(MonoBehaviour), true)]
public class AutoAssignDrawer : Editor
{
    //動作を制御するフラグ
    private static bool _isEnabled = false;
    
    public override void OnInspectorGUI()
    {
        if (!_isEnabled)
        {
            DrawDefaultInspector();
            return;
        }
        // デフォルトのプロパティを描画
        DrawDefaultInspector();

        // AutoAssign の処理を実行
        MonoBehaviour targetObject = (MonoBehaviour)target;
        FieldInfo[] fields = targetObject.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (FieldInfo field in fields)
        {
            AutoAssignAttribute autoAssign = field.GetCustomAttribute<AutoAssignAttribute>();
            if (autoAssign != null)
            {
                // Unityオブジェクト型の null 判定を改善
                if (field.GetValue(targetObject) != null && !(field.GetValue(targetObject) is UnityEngine.Object obj && obj == null))
                {
                    continue;
                }

                // フィールドの型に一致するコンポーネントを取得
                Component component = targetObject.GetComponent(field.FieldType);

                // コンポーネントが見つかった場合はアサイン
                if (component != null)
                {
                    field.SetValue(targetObject, component);
                    Debug.Log($"[AutoAssign] {field.Name} に {component.GetType().Name} を自動アサインしました", targetObject);
                }
                else
                {
                    Debug.LogWarning($"[AutoAssign] {field.Name} に対応するコンポーネントが見つかりませんでした", targetObject);
                }
            }
        }
    }
    
    /// <summary>
    /// AutoAssign の有効/無効を切り替える
    /// </summary>
    [MenuItem("Tools/Toggle AutoAssign")]
    public static void ToggleAutoAssign()
    {
        _isEnabled = !_isEnabled;
        Debug.Log($"自動アサイン機能は現在 {(_isEnabled ? "有効" : "無効")} です");
    }
}
#endif