using UnityEngine;

/// <summary>
/// デバッグログの文字サイズと色を取り回し良くした静的クラス
/// </summary>
public static class ExDebug
{
    #region Log

    public static void Log(string text, int size) => Debug.Log($"<size={size}>{text}</size>");
    public static void Log(string text, Color color) => Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>");
    public static void Log(string text, int size, Color color) => Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}><size={size}>{text}</size></color>");
    public static void Log(string text, Color color, int size) => Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}><size={size}>{text}</size></color>");

    #endregion Log

    #region LogWarning

    public static void LogWarning(string text, int size) => Debug.LogWarning($"<size={size}>{text}</size>");
    public static void LogWarning(string text, Color color) => Debug.LogWarning($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>");
    public static void LogWarning(string text, int size, Color color) => Debug.LogWarning($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}><size={size}>{text}</size></color>");
    public static void LogWarning(string text, Color color, int size) => Debug.LogWarning($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}><size={size}>{text}</size></color>");

    #endregion LogWarning

    #region LogError

    public static void LogError(string text, int size) => Debug.LogError($"<size={size}>{text}</size>");
    public static void LogError(string text, Color color) => Debug.LogError($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>");
    public static void LogError(string text, int size, Color color) => Debug.LogError($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}><size={size}>{text}</size></color>");
    public static void LogError(string text, Color color, int size) => Debug.LogError($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}><size={size}>{text}</size></color>");

    #endregion LogError
}