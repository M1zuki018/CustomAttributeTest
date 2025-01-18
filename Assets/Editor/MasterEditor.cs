#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// [AutoAssign]と[Button]を統合したCustomEditor
/// </summary>
[CustomEditor(typeof(MonoBehaviour), true)]
public class MasterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // デフォルトのインスペクターを描画
        DrawDefaultInspector();

        // 現在のターゲットオブジェクト
        MonoBehaviour targetObject = (MonoBehaviour)target;

        // AutoAssign の処理
        ProcessAutoAssign(targetObject);

        // Button の処理
        ProcessButtons(targetObject);

        // 変更を適用
        serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// [AutoAssign] 属性を処理する
    /// </summary>
    private void ProcessAutoAssign(MonoBehaviour targetObject)
    {
        // フィールド情報を取得
        FieldInfo[] fields = targetObject.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (FieldInfo field in fields)
        {
            // [AutoAssign] 属性が付与されているか確認
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
    /// [Button] 属性を処理する
    /// </summary>
    private void ProcessButtons(MonoBehaviour targetObject)
    {
        // メソッド情報を取得
        MethodInfo[] methods = targetObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        // 取得したメソッドをすべてログ出力
        foreach (MethodInfo method in methods)
        {
            Debug.Log($"Method Found: {method.Name}");
        }
        
        foreach (MethodInfo method in methods)
        {
            // [Button] 属性が付与されているか確認
            ButtonAttribute buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();
            if (buttonAttribute != null)
            {
                Debug.Log($"Found Button Method: {method.Name}");
                // ボタンのテキストを取得
                string buttonText = string.IsNullOrEmpty(buttonAttribute.Label) ? method.Name : buttonAttribute.Label;

                // ボタンを描画
                if (GUILayout.Button(buttonText))
                {
                    // メソッドを実行
                    method.Invoke(targetObject, null);
                }
            }
        }
    }
}
#endif