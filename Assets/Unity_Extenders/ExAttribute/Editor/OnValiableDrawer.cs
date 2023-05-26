using UnityEditor;
using UnityEngine;

/// <summary>
/// 変数名を上書きして文字をインスペクタに表示できるAttribute：描画クラス
/// </summary>
/// labelからサイズを抽出するフィールド関数どれ…？
/// 参考元：　https://baba-s.hatenablog.com/entry/2017/12/28/145600
[CustomPropertyDrawer(typeof(OnVariableAttribute))]
public class OnValiableAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var onValiable = attribute as OnVariableAttribute;
        if(onValiable != null)
            label.text = onValiable.onStr;
        switch(onValiable.onColor)
        {
            case OnColorState.white: GUI.contentColor = Color.white; break;
            case OnColorState.red: GUI.contentColor = Color.red; break;
            case OnColorState.pink: GUI.contentColor = new Color(0.82f, 0.08f, 0.48f, 1.0f); break;
            case OnColorState.magenta: GUI.contentColor = Color.magenta; break;
            case OnColorState.orange: GUI.contentColor = new Color(1.0f, 0.65f, 0, 1.0f); break;
            case OnColorState.yellow: GUI.contentColor = Color.yellow; break;
            case OnColorState.green: GUI.contentColor = Color.green; break;
            case OnColorState.darkgreen: GUI.contentColor = new Color(0, 0.5f, 0, 1.0f); break;
            case OnColorState.cyan: GUI.contentColor = Color.cyan; break;
            case OnColorState.blue: GUI.contentColor = Color.blue; break;
            case OnColorState.purple: GUI.contentColor = new Color(0.5f, 0, 0.7f, 1.0f); break;
        }
        
        EditorGUI.PropertyField(position, property, label);
        GUI.contentColor = Color.white;
    }
}