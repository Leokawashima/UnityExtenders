using UnityEngine;

/// <summary>
/// インスペクタ表示をカスタムするAttribute
/// </summary
/// 参考元：　https://baba-s.hatenablog.com/entry/2017/12/28/145600
public class OnVariableAttribute : PropertyAttribute
{
    public string Text;
    public Color Color;

    public OnVariableAttribute(string text_)
    {
        Text = text_;
    }
    public OnVariableAttribute(string text_, Color color_)
    {
        Text = text_;
        Color = color_;
    }
    public OnVariableAttribute(Color color_)
    {
        Color = color_;
    }
}
public static class ColorPalette
{
    public static Color White => Color.white;
    public static Color Red => Color.red;
    public static Color Pink => new(0.82f, 0.08f, 0.48f, 1.0f);
    public static Color Magenta => Color.magenta;
    public static Color Orange => new(1.0f, 0.65f, 0, 1.0f);
    public static Color Yellow => Color.yellow;
    public static Color Green => Color.green;
    public static Color DarkGreen => new(0, 0.5f, 0, 1.0f);
    public static Color Cyan => Color.cyan;
    public static Color Blue => Color.blue;
    public static Color Purple => new(0.5f, 0, 0.7f, 1.0f);
}