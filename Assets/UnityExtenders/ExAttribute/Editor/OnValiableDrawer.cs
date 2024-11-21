using UnityEditor;
using UnityEngine;

/// <summary>
/// OnValiableAttributeDrawerクラス
/// </summary>
/// 参考元：　https://baba-s.hatenablog.com/entry/2017/12/28/145600
[CustomPropertyDrawer(typeof(OnVariableAttribute))]
public class OnValiableAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var _onValiable = attribute as OnVariableAttribute;
        var _forward = GUI.contentColor;

        GUI.contentColor = _onValiable.Color;
        EditorGUI.PropertyField(position, property, label);
        GUI.contentColor = _forward;
    }
}