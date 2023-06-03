#if UNITY_EDITOR
using UnityEngine;

/// <summary>
/// デバッグログのリッチテキストをテキトーに使いやすそうにした静的クラス
/// </summary>
public static class ExDebug
{
    #region Log
    public static void Log(string text, int size)
    { Debug.Log($"<size={size}>{text}</size>"); }
    public static void Log(string text, Color color)
    { Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>"); }
    public static void Log(string text, int size, Color color)
    { Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}><size={size}>{text}</size></color>"); }
    public static void Log(string text, Color color, int size)
    { Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}><size={size}>{text}</size></color>"); }
    #endregion Log

    #region LogWarning
    public static void LogWarning(string text, int size)
    { Debug.LogWarning($"<size={size}>{text}</size>"); }
    public static void LogWarning(string text, Color color)
    { Debug.LogWarning($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>"); }
    public static void LogWarning(string text, int size, Color color)
    { Debug.LogWarning($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}><size={size}>{text}</size></color>"); }
    public static void LogWarning(string text, Color color, int size)
    { Debug.LogWarning($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}><size={size}>{text}</size></color>"); }
    #endregion LogWarning

    #region LogError
    public static void LogError(string text, int size)
    { Debug.LogError($"<size={size}>{text}</size>"); }
    public static void LogError(string text, Color color)
    { Debug.LogError($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>"); }
    public static void LogError(string text, int size, Color color)
    { Debug.LogError($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}><size={size}>{text}</size></color>"); }
    public static void LogError(string text, Color color, int size)
    { Debug.LogError($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}><size={size}>{text}</size></color>"); }
    #endregion LogError
}
#endif
/// (Debugクラス拡張メソッドを定義しようとしたがデバッグクラスはインスタンスを使わないから拡張メソッドを定義しても使えないので別クラスでてきとーに包んだ)
/// 以下　自分への備忘録
/// <size=hoge> </size>     サイズ変更
/// <color=hoge> </color>   色変更　hogeは#00000000のコードで指定できる
/// <b><i> </i></b>         大文字化　斜体化　複数変更したい時は(())のように最後に変更したものから閉じていく
/// <b><i> </b></i>         ←はNG