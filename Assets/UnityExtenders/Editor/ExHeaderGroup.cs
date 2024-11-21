using System;
using UnityEngine;
using UnityEditor;

namespace ExUnityEditor
{
    public class ExHeaderGroup : IDisposable
    {
        private bool m_drawLine = false;
        private Color m_color;
        private float m_height;
        private float m_offset;

        const float DEFAULT_HEIGHT = 4.0f;

        public ExHeaderGroup(Color lineColor_, float lineOffset_ = 1.0f)
        {
            m_drawLine = true;
            m_color = lineColor_;
            m_height = DEFAULT_HEIGHT;
            m_offset = lineOffset_;
        }

        public ExHeaderGroup(float lineHeight_, Color lineColor_, float lineOffset_ = 1.0f)
        {
            m_drawLine = true;
            m_color = lineColor_;
            m_height = lineHeight_;
            m_offset = lineOffset_;
        }

        public ExHeaderGroup(string title_, GUIStyle style_)
        {
            EditorGUILayout.LabelField(title_, style_);
        }

        public ExHeaderGroup(string title_, GUIStyle style_, Color lineColor_, float lineOffset_ = 1.0f)
        {
            EditorGUILayout.LabelField(title_, style_);
            m_drawLine = true;
            m_color = lineColor_;
            m_height = DEFAULT_HEIGHT;
            m_offset = lineOffset_;
        }

        public ExHeaderGroup(string title_, GUIStyle style_, float lineHeight_, Color lineColor_, float lineOffset = 1.0f)
        {
            EditorGUILayout.LabelField(title_, style_);
            m_drawLine = true;
            m_color = lineColor_;
            m_height = lineHeight_;
            m_offset = lineOffset;
        }

        public void Dispose()
        {
            if (false == m_drawLine) return;

            var _last = GUILayoutUtility.GetLastRect();
            var _draw = new Rect(0, _last.y + _last.height + 3.0f + m_offset, EditorGUIUtility.currentViewWidth, m_height);
            GUILayoutUtility.GetRect(_draw.width, _draw.height + 6.0f);

            var _forward = GUI.backgroundColor;
            GUI.backgroundColor = m_color;
            GUI.Box(_draw, "");
            GUI.backgroundColor = _forward;
        }
    }
}