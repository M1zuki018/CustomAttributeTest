using UnityEditor;
using UnityEngine;
using System.Reflection;

/// <summary>
/// インスペクターにボタンを表示し、メソッドを実行できるようにします
/// </summary>
[CustomEditor(typeof(MonoBehaviour), true)]
public class ButtonDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MonoBehaviour targetObject = (MonoBehaviour)target;
        MethodInfo[] methods = targetObject.GetType()
            .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var method in methods)
        {
            ButtonAttribute buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();
            if (buttonAttribute != null)
            {
                string buttonText = string.IsNullOrEmpty(buttonAttribute.Label) ? method.Name : buttonAttribute.Label;
                if (GUILayout.Button(buttonText))
                {
                    method.Invoke(targetObject, null);
                }
            }
        }
    }
}
