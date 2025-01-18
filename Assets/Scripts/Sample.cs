using UnityEngine;

/// <summary>
/// カスタム属性の使用例をまとめています
/// </summary>
public class Sample : MonoBehaviour
{
    //角度をノブで調整できます
    [Angle] public float angle;
    
    //変数名の表示を任意の文字列に変換できます
    [Comment("変数名の表示を変更")] public string comment;

    //inspectorの変数を変更不可にします
    [Disable] public int readOnly;
    
    //inspectorに表示しますが、実行中のみ書き換えを行えないようにします
    [ReadOnlyOnRuntime] public float readOnlyOnRuntime;

    //背景色を変えて変数を強調します。引数で色を指定できます
    [Highlight(1,0,0)] public float important;
    
    //オブジェクトがアサインされていない場合ハイライトします
    [HighlightIfNull] public Rigidbody2D rb;
    
    //最大値と最小値の間のみ動かせるスライダーを表示します
    [RangeArea(1,100)] public int range;
    
    //入力される文字列が特定のフォーマットに従っているかをチェックする
    //ファイル名や特定の識別子を扱う場合などに使用できます
    [RegexValidation(@"^\w+$", "英数字のみ使用できます")] public string regexValidation;
    
    //シーン名をポップアップから選択できるようにします
    [SceneName] public string sceneName;
    [SceneName(true)] public string sceneName2;
    
    //条件が満たされた場合のみフィールドをinspectorに表示します
    public bool active;
    [ShowIf("active")] public bool hello;
    
    //複数のオブジェクト間で特定の値を一括編集できるようにします
    [SyncValues] public int number;

    //タグをポップアップから表示できるようにします
    [TagSelector] public string tag1;

    //特定のコンポーネントを指定して参照します
    [RequireComponentOfType(typeof(Camera))] public GameObject cameraObj;

    //メソッドをinspectorに表示したボタンから呼び出せるようにします
    [Button]
    public void ButtonOnInspecter()
    {
        Debug.Log("見てくれてありがとうございます！");
    }
    
    //指定した値の間でVector2型の最小値(x)と最大値(y)をスライダーで調整できるようにします
    [MinMaxSlider(1,10)] public Vector2 minMax;
}
