using UnityEngine;

/// <summary>
/// サンプル用のスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "NewSO", menuName = "Sample/Create SampleSO")]
public class SampleSO : ScriptableObject
{
    public string dataName;
    public int value;
}