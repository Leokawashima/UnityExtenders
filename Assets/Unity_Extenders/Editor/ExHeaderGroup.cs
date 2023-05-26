using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

namespace ExUnityEditor
{
    /// <summary>
    /// ヘッダーに関連する処理をまとめるスコープusingステートメント　終わりに疑似ラインを引くコンストラクタ有り
    /// </summary>
    public class ExHeaderGroup : IDisposable
    {
        private bool onDraw = false;
        private Color color;
        private float height = 0;
        private float offset = 0;

        const float Defline = 4.0f;

        public ExHeaderGroup(Color line_color)
        {
            onDraw = true;
            color = line_color;
            height = Defline;
        }

        public ExHeaderGroup(Color line_color, float line_offset)
        {
            onDraw = true;
            color = line_color;
            height = Defline;
            offset = line_offset;
        }

        public ExHeaderGroup(float line_height, Color line_color)
        {
            onDraw = true;
            color = line_color;
            height = line_height;
        }

        public ExHeaderGroup(float line_height, Color line_color, float line_offset)
        {
            onDraw = true;
            color = line_color;
            height = line_height;
            offset = line_offset;
        }

        public ExHeaderGroup(string title, GUIStyle style)
        {
            EditorGUILayout.LabelField(title, style);
        }

        public ExHeaderGroup(string title, GUIStyle style, Color line_color)
        {
            EditorGUILayout.LabelField(title, style);
            onDraw = true;
            color = line_color;
            height = Defline;
        }

        public ExHeaderGroup(string title, GUIStyle style, Color line_color, float line_offset)
        {
            EditorGUILayout.LabelField(title, style);
            onDraw = true;
            color = line_color;
            height = Defline;
            offset = line_offset;
        }

        public ExHeaderGroup(string title, GUIStyle style, float line_height, Color line_color)
        {
            EditorGUILayout.LabelField(title, style);
            onDraw = true;
            color = line_color;
            height = line_height;
        }

        public ExHeaderGroup(string title, GUIStyle style, float line_height, Color line_color, float line_offset)
        {
            EditorGUILayout.LabelField(title, style);
            onDraw = true;
            color = line_color;
            height = line_height;
            offset = line_offset;
        }

        public void Dispose()
        {
            Rect last = GUILayoutUtility.GetLastRect();
            Rect draw;
            if(onDraw)
            {
                draw = new Rect(0, last.y + last.height + 3.0f + offset, EditorGUIUtility.currentViewWidth, height);
                GUILayoutUtility.GetRect(draw.width, draw.height + 6.0f);
                GUI.backgroundColor = color;
                GUI.Box(draw, "");
                GUI.backgroundColor = Color.white;
            }
        }
    }
}