using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEditor;

namespace GaMe.ExMesh
{
    [Serializable]
    public struct ExGizmosDrawContext : IEquatable<ExGizmosDrawContext>, IFormattable
    {
        public Color Color;
        public Transform Transform;
        public Matrix4x4 Matrix;

        public readonly static ExGizmosDrawContext Identity = new()
        {
            Color = Color.green,
            Transform = null,
            Matrix = Matrix4x4.identity,
        };

        public ExGizmosDrawContext(Color color_, Transform follow_, Matrix4x4 matrix_)
        {
            Color = color_;
            Transform = follow_;
            Matrix = matrix_;
        }

        public bool Equals(ExGizmosDrawContext other_)
        {
            return
                Color == other_.Color &&
                Matrix == other_.Matrix;
        }

        public override string ToString()
        {
            return ToString(null, null);
        }
        public string ToString(string format_)
        {
            return ToString(format_, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format_, IFormatProvider formatProvider_)
        {
            if (string.IsNullOrEmpty(format_))
            {

            }

            if (formatProvider_ == null)
            {
                formatProvider_ = CultureInfo.InvariantCulture.NumberFormat;
            }

            return string.Format(CultureInfo.InvariantCulture.NumberFormat,
                Color.ToString(format_, formatProvider_),
                Matrix.ToString(format_, formatProvider_));
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ExGizmosDrawContext))]
    public class ExGizmosDrawContextDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect pos_, SerializedProperty prop_, GUIContent label_)
        {
            //var _serialObj = prop_.serializedObject;
            //_serialObj.Update();

            var _singleLine = ExGizmosEditorUtility.SINGLE_LINE_HEIGHT;
            var _lineHeight = ExGizmosEditorUtility.SINGLE_LINE_HEIGHT_SPACING;
            var _width = EditorGUIUtility.currentViewWidth;

            pos_.height = _singleLine;

            {
                prop_.isExpanded = EditorGUI.Foldout(pos_, prop_.isExpanded, label_);
                pos_.y += _lineHeight;
            }

            if (prop_.isExpanded)
            {
                EditorGUI.indentLevel++;

                {
                    var _prop = prop_.FindPropertyRelative("Color");
                    _prop.colorValue = EditorGUI.ColorField(pos_, new GUIContent("Color"), _prop.colorValue);
                    pos_.y += _lineHeight;
                }

                {
                    var _prop = prop_.FindPropertyRelative("Transform");
                    EditorGUI.PropertyField(pos_, _prop);
                    pos_.y += _lineHeight;
                }

                {
                    var _prop = prop_.FindPropertyRelative("Matrix");
                    _prop.isExpanded = EditorGUI.Foldout(pos_, _prop.isExpanded, "Matrix");
                    pos_.y += _lineHeight;

                    if (_prop.isExpanded)
                    {
                        var _rect = new Rect(pos_);
                        var _labelOffset = 26;
                        var _startOffset = pos_.x + 7;
                        var _floatWidth = _width / 4 - (_startOffset);

                        for (int x = 0; x < 4; ++x)
                        {
                            _rect.y = pos_.y;
                            for (int y = 0; y < 4; ++y)
                            {
                                _rect.x = pos_.x + _floatWidth * x;
                                _rect.width = 0;
                                var _dataStr = $"e{x}{y}";
                                var _dataProp = _prop.FindPropertyRelative(_dataStr);
                                EditorGUI.PrefixLabel(_rect, new GUIContent(_dataStr.ToUpper()));
                                _rect.x += _labelOffset;
                                _rect.width = _floatWidth;
                                _dataProp.floatValue = EditorGUI.FloatField(_rect, _dataProp.floatValue);
                                _rect.y += _lineHeight;
                            }
                        }
                        pos_.y += _lineHeight * 4;
                    }
                }

                EditorGUI.indentLevel--;
            }

            //_serialObj.ApplyModifiedProperties();
        }

        private static float GetPropertyHeight(SerializedProperty prop_)
        {
            return EditorGUI.GetPropertyHeight(prop_, true) + ExGizmosEditorUtility.VERTICAL_SPACING;
        }

        public override float GetPropertyHeight(SerializedProperty prop_, GUIContent label_)
        {
            var _lineHeight = ExGizmosEditorUtility.SINGLE_LINE_HEIGHT_SPACING;
            var _height = _lineHeight;

            if (prop_.isExpanded)
            {
                _height += _lineHeight; // color
                _height += _lineHeight; // transform
                _height += _lineHeight; // matrix
                if (prop_.FindPropertyRelative("Matrix").isExpanded)
                {
                    _height += _lineHeight * 4;
                }
            }

            return _height;
        }
    }
#endif
}
