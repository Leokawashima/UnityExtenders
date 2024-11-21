using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public static class ExGizmosEditorUtility
{
    public static readonly float SINGLE_LINE_HEIGHT = EditorGUIUtility.singleLineHeight;
    public static readonly float VERTICAL_SPACING = EditorGUIUtility.standardVerticalSpacing;
    public static readonly float SINGLE_LINE_HEIGHT_SPACING = SINGLE_LINE_HEIGHT + VERTICAL_SPACING;
}
#endif