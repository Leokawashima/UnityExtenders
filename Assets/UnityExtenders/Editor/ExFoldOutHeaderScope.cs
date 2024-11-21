using System;
using UnityEngine;
using UnityEditor;

namespace ExUnityEditor
{
    /// <summary>
    /// 閉じ忘れを防ぐHeaderスコープusingステートメント
    /// </summary>
    public class ExFoldOutHeaderScope : IDisposable
    {
        public ExFoldOutHeaderScope(string title, ref bool foldout)
        {
            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, title);
        }

        public ExFoldOutHeaderScope(string title, ref bool foldout, Color color)
        {
            GUI.backgroundColor = color;
            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, title);
            GUI.backgroundColor = Color.white;
        }

        public void Dispose()
        {
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
