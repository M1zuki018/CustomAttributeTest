using UnityEngine;

/// <summary>
/// カスタム属性の使用例をまとめています
/// 注：[Button]と[AutoAssignDrawer]はどちらも[CustomEditor(typeof(MonoBehaviour), true)]を使用しており、競合します。
/// Menu/Toolsからそれぞれ機能を無効にするようにしてください
/// </summary>
public class Sample : MonoBehaviour
{
    #region 変更不可にする
    
    [Disable] public int readOnly; //inspectorの変数を変更不可にします
    [ReadOnlyOnRuntime] public float readOnlyOnRuntime; //inspectorに表示しますが、実行中のみ書き換えを行えないようにします
    
    public bool active;
    [ShowIf("active")] public bool hello; //条件が満たされた場合のみフィールドをinspectorに表示します
    
    #endregion

    #region 目立たせる

    [Highlight(1,0,0)] public float important; //背景色を変えて変数を強調します。引数で色を指定できます
    [HighlightIfNull] public Rigidbody2D rb; //オブジェクトがアサインされていない場合ハイライトします
    [RequestField] public Rigidbody2D rb2;

    #endregion

    #region 値の指定方法

    [Angle] public float angle; //角度をノブで調整できます
    
    [RangeArea(1,100)] public int range; //最大値と最小値の間のみ動かせるスライダーを表示します
    
    //指定した値の間でVector2型の最小値(x)と最大値(y)をスライダーで調整できるようにします
    //攻撃力の調整や左右移動の制限の調整など
    [MinMaxSlider(1,10)] public Vector2 minMax;
    
    [ProgressBar(0, 100, "HP")] public float progress; //進捗バーを表示します
    
    //入力される文字列が特定のフォーマットに従っているかをチェックする
    //ファイル名や特定の識別子を扱う場合などに使用できます
    [RegexValidation(@"^\w+$", "英数字のみ使用できます")] public string regexValidation;

    //inspectorからフォルダーを直接指定してパスを取得できるようにします
    //保存先のフォルダーのパスを簡単に取得したい時などに
    //[Button]属性と競合しており、フォルダーを選択した時に1つエラーメッセージが出ます（描画順の問題）
    //無視するかどちらかの属性を使用しないようにしてください
    [FolderPath] public string folderPath;
    
    #endregion

    #region ポップアップ

    [SceneName] public string sceneName; //表示中のシーン名をポップアップから選択できるようにします
    [SceneName(true)] public string sceneName2; //全てのシーン名をポップアップから選択できるようにします
    
    [TagSelector] public string tag1; //タグをポップアップから表示できるようにします

    #endregion

    #region その他機能

    [Comment("変数名の表示を変更")] public string comment; //変数名の表示を任意の文字列に変換できます
    
    [SyncValues] public int number; //複数のオブジェクト間で特定の値を一括編集できるようにします
    
    [RequireComponentOfType(typeof(Camera))] public GameObject cameraObj; //特定のコンポーネントを指定して参照します
    
    [Expandable] public SampleSO sampleSO; //ScriptableObject を Inspector 上で展開して編集できるようにします
    
    [AutoAssign] public Rigidbody2D rb3; //アサインされていなかった場合、自動的にアタッチしたオブジェクトからアサインします

    #endregion
    
    //メソッドをinspectorに表示したボタンから呼び出せるようにします
    [Button("ButtonOnInspecter")]
    public void ButtonOnInspecter()
    {
        Debug.Log("見てくれてありがとうございます！");
    } 
    
}
