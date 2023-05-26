using UnityEngine;

/// <summary>
/// 変数名を上書きして文字をインスペクタに表示できるAttribute：定義クラス
/// </summary>
/// ヘッダーや変数名のサイズを大きくしたり色を変える方法とかないのかな？
/// と思ってちらっと調べてエディタ拡張とレイアウト関係で属性で簡単に実装するのは時間がかかりそうなので
/// ラベルに変数名が入って描画してるだけっぽいのに気づいてサクっと作った上書きクラス。
/// 時間が余ってたらEditor周りをフル改造できるようになりたい。未来の俺頑張れ。
/// 参考元：　https://baba-s.hatenablog.com/entry/2017/12/28/145600
public class OnVariableAttribute : PropertyAttribute
{
    public string onStr;
    public OnColorState onColor;

    public OnVariableAttribute(string str)
    {
        onStr = str;
        onColor = OnColorState.white;
    }
    public OnVariableAttribute(string str, OnColorState color)
    {
        onStr = str;
        onColor = color;
    }
}
public enum OnColorState
{
    white,
    red,
    pink,
    magenta,
    orange,
    yellow,
    green,
    darkgreen,
    cyan,
    blue,
    purple
}