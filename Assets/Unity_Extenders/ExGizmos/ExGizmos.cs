using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.IMGUI.Controls;
#endif
using UnityEngine;

namespace GaMe.ExMesh
{
    [Serializable]
    public class ExGizmos
    {
        [SerializeField] private bool m_enabled = true;
        public bool Enabled { get => m_enabled; set => m_enabled = value; }

        [SerializeField] private Transform m_transform = null;
        public Transform Transform { get =>  m_transform; set => m_transform = value; }

        [SerializeField] private Matrix4x4 m_matrix = Matrix4x4.identity;
        public Matrix4x4 Matrix { get => m_matrix; set => m_matrix = value;}

        [SerializeField] private Vector3 m_position = Vector3.zero;
        public Vector3 Position { get => m_position; set => m_position = value; }

        [SerializeField] private Quaternion m_rotation = Quaternion.identity;
        public Quaternion Rotation { get => m_rotation; set => m_rotation = value; }

        [SerializeField] private Vector3 m_scale = Vector3.one;
        public Vector3 Scale { get => m_scale; set => m_scale = value; }

        [SerializeField] private Color m_color = Color.green;
        public Color Color { get => m_color; set => m_color = value; }

        public enum DrawState
        {
            Default,
            ForcePlane,
            ForceWire,
        }

        [SerializeField] private DrawState m_drawMode = DrawState.Default;
        public DrawState DrawMode { get => m_drawMode; set => m_drawMode = value; }

        [SerializeField] private Mesh m_mesh = null;
        public Mesh Mesh { get => m_mesh;  set => m_mesh = value; }

        [SerializeField] private int m_subMeshIndex = -1;
        public int SubMeshIndex { get => m_subMeshIndex; set => m_subMeshIndex = value; }

        [SerializeReference] private List<ExGizmosDrawElement> m_drawElements = new();
        public List<ExGizmosDrawElement> DrawElements { get => m_drawElements; set => m_drawElements = value; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Draw(ExGizmos gizmos_)
        {
            if (gizmos_.Enabled)
            {
                var _forward = Gizmos.color;

                var _drawElements = gizmos_.m_drawElements;
                foreach (var element in _drawElements)
                {
                    element.Draw(element.Matrix);
                }

                Gizmos.color = _forward;
            }
        }
    }

    #region drawer

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ExGizmos))]
    public class ExGizmosDrawer : PropertyDrawer
    {
        private static Color s_bgColor = new(0.2f, 0.4f, 0.3f);
        private static readonly float s_singleLine = EditorGUIUtility.singleLineHeight;
        private static ExGizmosDrawElement[] s_subClass = 
            Assembly.GetAssembly(typeof(ExGizmosDrawElement))
                .GetTypes()
                .Where(type_ => type_.IsSubclassOf(typeof(ExGizmosDrawElement)) && false == type_.IsAbstract)
                .Select(type_ => Activator.CreateInstance(type_) as ExGizmosDrawElement)
                .ToArray();

        private static float GetPropertyHeight(SerializedProperty prop_)
        {
            return EditorGUI.GetPropertyHeight(prop_, true) + EditorGUIUtility.standardVerticalSpacing;
        }

        public override void OnGUI(Rect pos_, SerializedProperty prop_, GUIContent label_)
        {
            var _serialObj = prop_.serializedObject;
            _serialObj.Update();

            var _singleLine = s_singleLine;
            var _lineHeight = s_singleLine + EditorGUIUtility.standardVerticalSpacing;
            var _width = EditorGUILayout.GetControlRect().width;
            pos_.height = _singleLine;

            {//BG
                var _rect = new Rect(-18, 0, _width + 21, _singleLine);
                if (_serialObj.FindProperty(fieldInfo.Name).isArray)
                {
                    _rect.x -= 13;
                    _rect.y -= 2;
                    _rect.width += 13;
                    _rect.height += 4;

                }
                EditorGUI.DrawRect(_rect, s_bgColor);
            }

            {//Foldout
                prop_.isExpanded = EditorGUI.Foldout(pos_, prop_.isExpanded, label_);
                var _rect = new Rect(pos_)
                {
                    x = _width +18 - 88,
                };

                EditorGUI.PrefixLabel(_rect, new GUIContent("Enabled"));
                _rect.x += 52;
                var _prop = prop_.FindPropertyRelative("m_enabled");
                _prop.boolValue = EditorGUI.Toggle(_rect, _prop.boolValue);

                pos_.y += _lineHeight;
            }

            if (prop_.isExpanded)
            {
                EditorGUI.indentLevel++;

                {
                    var _prop = prop_.FindPropertyRelative("m_transform");
                    EditorGUI.ObjectField(pos_, _prop, new GUIContent("Transform"));
                    pos_.y += _lineHeight;
                }

                {
                    var _prop = prop_.FindPropertyRelative("m_position");
                    _prop.vector3Value = EditorGUI.Vector3Field(pos_, new GUIContent("Position"), _prop.vector3Value);
                    pos_.y += _lineHeight;
                }

                {
                    var _prop = prop_.FindPropertyRelative("m_rotation");
                    var quat4 = new Vector4(_prop.quaternionValue.x, _prop.quaternionValue.y, _prop.quaternionValue.z, _prop.quaternionValue.w);
                    var value = EditorGUI.Vector4Field(pos_, new GUIContent("Rotation"), quat4);
                    _prop.quaternionValue = new Quaternion(value.x, value.y, value.z, value.w);
                    pos_.y += _lineHeight;
                }

                {
                    var _prop = prop_.FindPropertyRelative("m_scale");
                    _prop.vector3Value = EditorGUI.Vector3Field(pos_, new GUIContent("Scale"), _prop.vector3Value);
                    pos_.y += _lineHeight;
                }

                {
                    var _prop = prop_.FindPropertyRelative("m_color");
                    _prop.colorValue = EditorGUI.ColorField(pos_, new GUIContent("Color"), _prop.colorValue);
                    pos_.y += _lineHeight;
                }

                {
                    var _prop = prop_.FindPropertyRelative("m_drawMode");
                    _prop.enumValueIndex = EditorGUI.Popup(pos_, "DrawMode", _prop.enumValueIndex, Enum.GetNames(typeof(ExGizmos.DrawState)));
                    pos_.y += _lineHeight;
                }

                {
                    var _prop = prop_.FindPropertyRelative("m_drawElements");
                    EditorGUI.PropertyField(pos_, _prop);
                    pos_.y += GetPropertyHeight(_prop);
                }

                {
                    if (GUI.Button(pos_, "Add DrawElement"))
                    {
                        var _dropdown = new ExGizmosDrawElementAdvancedDropdown(
                            new AdvancedDropdownState(), _serialObj, _OnCallback
                            );
                        _dropdown.Show(pos_);

                        void _OnCallback(ExGizmosDrawElement element_)
                        {
                            _serialObj.Update();
                            var _elements = prop_.FindPropertyRelative("m_drawElements");
                            var _index = _elements.arraySize;
                            _elements.InsertArrayElementAtIndex(_index);

                            var _prop = _elements.GetArrayElementAtIndex(_index);
                            _prop.boxedValue = element_;
                            {
                                var _ena = _prop.FindPropertyRelative("m_enabled");
                                _ena.boolValue = true;
                            }
                            _serialObj.ApplyModifiedProperties();
                        }
                    }
                    pos_.y += _lineHeight;
                }

                EditorGUI.indentLevel--;
            }

            _serialObj.ApplyModifiedProperties();
        }
        public override float GetPropertyHeight(SerializedProperty prop_, GUIContent label_)
        {
            var _lineHeight = s_singleLine + EditorGUIUtility.standardVerticalSpacing;
            var _height = 0.0f;

            if (prop_.isExpanded)
            {
                _height += _lineHeight; // GetPropertyHeight(prop_.FindPropertyRelative("m_transform"));
                _height += _lineHeight; // GetPropertyHeight(prop_.FindPropertyRelative("m_position"));
                _height += _lineHeight; // GetPropertyHeight(prop_.FindPropertyRelative("m_rotation"));
                _height += _lineHeight; // GetPropertyHeight(prop_.FindPropertyRelative("m_scale"));
                _height += _lineHeight; // color
                _height += _lineHeight; // enum
                _height += GetPropertyHeight(prop_.FindPropertyRelative("m_drawElements"));
                _height += _lineHeight; // button
            }

            return _height;
        }

        //[DidReloadScripts]
        //private static void OnReloadScripts()
        //{
        //    var _type = typeof(ExGizmosDrawElement);
        //    s_subClass = Assembly.GetAssembly(_type)
        //        .GetTypes()
        //        .Where(type_ => type_.IsSubclassOf(_type) && false == type_.IsAbstract)
        //        .Select(type_ => Activator.CreateInstance(type_) as ExGizmosDrawElement)
        //        .ToArray();
        //}

        public class ExGizmosDrawElementAdvancedDropdown : AdvancedDropdown
        {
            private SerializedObject m_serialObj;
            private Dictionary<int, ExGizmosDrawElement> m_drawElementsDict = new();
            private Action<ExGizmosDrawElement> m_callback;

            public ExGizmosDrawElementAdvancedDropdown(AdvancedDropdownState state_, SerializedObject serialObj_, Action<ExGizmosDrawElement> callback_) : base(state_)
            {
                minimumSize = new(200, 200);

                m_serialObj = serialObj_;
                m_callback = callback_;
            }

            protected override AdvancedDropdownItem BuildRoot()
            {
                var _root = new AdvancedDropdownItem("DrawElements");

                var _elements = s_subClass;

                foreach (var element in _elements)
                {
                    var _item = new AdvancedDropdownItem(element.GetType().Name);
                    m_drawElementsDict.Add(_item.id, element);
                    _root.AddChild(_item);
                }

                return _root;
            }

            protected override void ItemSelected(AdvancedDropdownItem item_)
            {
                m_callback(m_drawElementsDict[item_.id]);
            }
        }
    }
#endif

    #endregion drawer
}