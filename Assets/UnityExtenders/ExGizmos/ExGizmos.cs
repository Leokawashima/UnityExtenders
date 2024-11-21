using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.IMGUI.Controls;
#endif
using UnityEngine;

namespace GaMe.ExMesh
{
    public enum DrawState
    {
        Default,
        ForcePlane,
        ForceWire,
    }

    [Serializable]
    public class ExGizmos
    {
        [SerializeField] private bool m_enabled = true;
        public bool Enabled { get => m_enabled; set => m_enabled = value; }

        [SerializeField] private ExGizmosDrawContext m_context = new();
        public ExGizmosDrawContext Context { get => m_context; set => m_context = value; }

        [SerializeField] private DrawState m_drawMode = DrawState.Default;
        public DrawState DrawMode { get => m_drawMode; set => m_drawMode = value; }

        [SerializeReference] private List<IExGizmosDrawElement> m_drawElements = new();
        public List<IExGizmosDrawElement> DrawElements { get => m_drawElements; set => m_drawElements = value; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Draw(ExGizmos gizmos_)
        {
            if (false == gizmos_.Enabled)
            {
                return;
            }

            var _exColor = Gizmos.color;
            var _exMatrix = Gizmos.matrix;

            var _drawElements = gizmos_.m_drawElements;
            foreach (var element in _drawElements)
            {
                if (false == element.Enabled)
                {
                    continue;
                }

                var _mat = gizmos_.Context;
                gizmos_.Context.Matrix = Matrix4x4.TRS(_mat.Position, Quaternion.Euler(_mat.Rotation), _mat.Scale);
                element.Draw(gizmos_.Context, gizmos_.DrawMode);
            }

            Gizmos.color = _exColor;
            Gizmos.matrix = _exMatrix;
        }
    }

    #region drawer

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ExGizmos))]
    public class ExGizmosDrawer : PropertyDrawer
    {
        private static Color s_bgColor = new(0.2f, 0.4f, 0.3f);
        private static Type[] s_elementsClasses = ExGizmosUtility.GetInterfaceDerivedClassTypes<IExGizmosDrawElement>();

        public override void OnGUI(Rect pos_, SerializedProperty prop_, GUIContent label_)
        {
            var _serialObj = prop_.serializedObject;
            _serialObj.Update();

            var _singleLine = ExGizmosEditorUtility.SINGLE_LINE_HEIGHT;
            var _lineHeight = _singleLine + ExGizmosEditorUtility.VERTICAL_SPACING;
            var _width = pos_.width;

            pos_.height = _singleLine;

            {//BG
                var _rect = new Rect(-18, 0, _width + 21, _singleLine);
                if (_serialObj.FindProperty(fieldInfo.Name).isArray)
                {
                    _rect.x -= 13;
                    _rect.y -= 2;
                    _rect.width += 13;
                    _rect.height += 6;
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

                if (_serialObj.FindProperty(fieldInfo.Name).isArray)
                {
                    pos_.y += 6;
                }
                pos_.y += _lineHeight;
            }

            if (prop_.isExpanded)
            {
                EditorGUI.indentLevel++;

                {
                    var _prop = prop_.FindPropertyRelative("m_context");
                    EditorGUI.PropertyField(pos_, _prop);
                    pos_.y += GetPropertyHeight(_prop);
                }

                {
                    var _prop = prop_.FindPropertyRelative("m_drawMode");
                    _prop.enumValueIndex = EditorGUI.Popup(pos_, "DrawMode", _prop.enumValueIndex, Enum.GetNames(typeof(DrawState)));
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
                            new AdvancedDropdownState(),
                            _OnCallback
                        );
                        _dropdown.Show(pos_);

                        void _OnCallback(Type selectedElementType_)
                        {
                            _serialObj.Update();
                            var _elements = prop_.FindPropertyRelative("m_drawElements");
                            var _index = _elements.arraySize;
                            _elements.InsertArrayElementAtIndex(_index);

                            var _prop = _elements.GetArrayElementAtIndex(_index);
                            var _elementInstance = Activator.CreateInstance(selectedElementType_);
                            _prop.boxedValue = _elementInstance;
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
                if (prop_.serializedObject.FindProperty(fieldInfo.Name).isArray)
                {
                    _height += 6;
                }
                _height += GetPropertyHeight(prop_.FindPropertyRelative("m_context"));
                _height += _lineHeight; // enum
                _height += GetPropertyHeight(prop_.FindPropertyRelative("m_drawElements"));
                _height += _lineHeight; // button
            }

            return _height;
        }

        public class ExGizmosDrawElementAdvancedDropdown : AdvancedDropdown
        {
            private Dictionary<int, Type> m_drawElementsDict = new();
            private Action<Type> m_callback;

            public ExGizmosDrawElementAdvancedDropdown(AdvancedDropdownState state_, Action<Type> callback_) : base(state_)
            {
                minimumSize = new(200, 200);

                m_callback = callback_;
            }

            protected override AdvancedDropdownItem BuildRoot()
            {
                var _root = new AdvancedDropdownItem("DrawElements");

                var _elements = s_elementsClasses;

                foreach (var element in _elements)
                {
                    var _item = new AdvancedDropdownItem(element.Name);
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