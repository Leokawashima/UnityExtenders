using System;
using UnityEngine;
using UnityEditor;

namespace GaMe.ExMesh
{
    [Serializable]
    public class ExGizmosDrawContext
    {
        [SerializeField] private Color m_color = Color.green;
        public Color Color { get => m_color; set => m_color = value; }

        [SerializeField] private Transform m_transform = null;
        public Transform Transform { get => m_transform; set => m_transform = value; }

        [SerializeField] private Vector3 m_position = Vector3.zero;
        public Vector3 Position
        {
            get => m_position;
            set
            {
                m_matrix = Matrix4x4.TRS(value, Quaternion.Euler(m_rotation), m_scale);
                m_position = value;
            }
        }

        [SerializeField] private Vector3 m_rotation = Vector3.zero;
        public Vector3 Rotation
        {
            get => m_rotation;
            set
            {
                m_matrix = Matrix4x4.TRS(m_position, Quaternion.Euler(value), m_scale);
                m_rotation = value;
            }
        }

        [SerializeField] private Vector3 m_scale = Vector3.one;
        public Vector3 Scale
        {
            get => m_scale;
            set
            {
                m_matrix = Matrix4x4.TRS(m_position, Quaternion.Euler(m_rotation), value);
                m_scale = value;
            }
        }

        [SerializeField] private Matrix4x4 m_matrix = Matrix4x4.identity;
        public Matrix4x4 Matrix { get => m_matrix; set => m_matrix = value; }

        public ExGizmosDrawContext()
        {
        }

#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(ExGizmosDrawContext))]
        public class Drawer : PropertyDrawer
        {
            private readonly static GUIContent COLOR_CONTENT = new("Color");
            private readonly static GUIContent TRANSFORM_CONTENT = new("Transform");
            private readonly static GUIContent POSITION_CONTENT = new("Position");
            private readonly static GUIContent ROTATION_CONTENT = new("Rotation");
            private readonly static GUIContent SCALE_CONTENT = new("Scale");

            public override void OnGUI(Rect pos_, SerializedProperty prop_, GUIContent label_)
            {
                var _singleLine = ExGizmosEditorUtility.SINGLE_LINE_HEIGHT;
                var _lineHeight = ExGizmosEditorUtility.SINGLE_LINE_HEIGHT_SPACING;
                var _width = pos_.width;

                pos_.height = _singleLine;

                {
                    prop_.isExpanded = EditorGUI.Foldout(pos_, prop_.isExpanded, label_);
                    pos_.y += _lineHeight;
                }

                if (prop_.isExpanded)
                {
                    EditorGUI.indentLevel++;

                    var _colorProp = prop_.FindPropertyRelative("m_color");
                    var _transformProp = prop_.FindPropertyRelative("m_transform");
                    var _posProp = prop_.FindPropertyRelative("m_position");
                    var _rotProp = prop_.FindPropertyRelative("m_rotation");
                    var _scaleProp = prop_.FindPropertyRelative("m_scale");

                    EditorGUI.PropertyField(pos_, _colorProp, COLOR_CONTENT);
                    pos_.y += _lineHeight;

                    EditorGUI.PropertyField(pos_, _transformProp, TRANSFORM_CONTENT);
                    pos_.y += _lineHeight;

                    EditorGUI.BeginChangeCheck();

                    EditorGUI.PropertyField(pos_, _posProp, POSITION_CONTENT);
                    pos_.y += _lineHeight;

                    EditorGUI.PropertyField(pos_, _rotProp, ROTATION_CONTENT);
                    pos_.y += _lineHeight;

                    EditorGUI.PropertyField(pos_, _scaleProp, SCALE_CONTENT);
                    pos_.y += _lineHeight;

                    if (EditorGUI.EndChangeCheck())
                    {
                        var _matProp = prop_.FindPropertyRelative("m_matrix");
                        _matProp.boxedValue = Matrix4x4.TRS(
                            _posProp.vector3Value,
                            Quaternion.Euler(_rotProp.vector3Value),
                            _scaleProp.vector3Value);
                    }

                    EditorGUI.indentLevel--;
                }
            }

            public override float GetPropertyHeight(SerializedProperty prop_, GUIContent label_)
            {
                var _lineHeight = ExGizmosEditorUtility.SINGLE_LINE_HEIGHT_SPACING;
                var _height = _lineHeight;

                if (prop_.isExpanded)
                {
                    _height += _lineHeight * 5;
                }

                return _height;
            }
        }
#endif
    }
}
