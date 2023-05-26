using System;
using UnityEngine;

namespace ExUnityEditor
{
    /// <summary>
    /// 戻し忘れを防ぐスコープusingステートメント
    /// </summary>
    public class ExColorScope
    {
        public class GUIBackGround : IDisposable
        {
            public GUIBackGround(Color color)
            {
                GUI.backgroundColor = color;
            }

            public void Dispose()
            {
                GUI.backgroundColor = Color.white;
            }
        }

        public class GUIContent : IDisposable
        {
            public GUIContent(Color color)
            {
                GUI.contentColor = color;
            }

            public void Dispose()
            {
                GUI.contentColor = Color.white;
            }
        }

        public class GUIColor : IDisposable
        {
            public GUIColor(Color color)
            {
                GUI.color = color;
            }

            public void Dispose()
            {
                GUI.color = Color.white;
            }
        }
    }
}

