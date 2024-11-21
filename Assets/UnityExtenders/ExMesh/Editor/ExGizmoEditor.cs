using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using ExUnityEditor;

[CustomEditor(typeof(ExGizmos))]
public class ExGizmosEditor : Editor
{
    private static bool
        s_menuFoldout = true,
        s_assetsFoldout = false;

    private static GUIStyle
        s_headerStyle,
        s_subStyle;

    private ExGizmos m_target;

    private void OnEnable()
    {
        m_target = target as ExGizmos;

        if (s_headerStyle == null)
        {
            s_headerStyle = new()
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
            };
            s_headerStyle.normal.textColor = Color.white;
        }

        if (s_subStyle == null)
        {
            s_subStyle = new()
            {
                fontStyle = FontStyle.Bold,
            };
            s_subStyle.normal.textColor = new Color(0.7f, 1.0f, 1.0f, 1.0f);
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        serializedObject.Update();
        var _target = m_target;

        using (new ExHeaderGroup("ギズモ設定", s_headerStyle, Color.green))
        {
            _target.DrawColor = EditorGUILayout.ColorField("ギズモカラー", _target.DrawColor);
            _target.IsWire = EditorGUILayout.Toggle("ワイヤ化", _target.IsWire);
        }
        using (new ExHeaderGroup(Color.green, 5.0f))
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("生成ギズモを選択", s_headerStyle);
                using (new ExColorScope.GUIBackGround(Color.green))
                {
                    _target.Choice = (ExGizmos.MeshMenu)EditorGUILayout.EnumPopup(_target.Choice);
                }
            }
        }

        using (new ExFoldOutHeaderScope("メニューフォルダ", ref s_menuFoldout, Color.red))
        {
            if (s_menuFoldout)
            {
                switch (_target.Choice)
                {
                    case ExGizmos.MeshMenu.Default:
                        using (new ExHeaderGroup("頂点数", s_headerStyle, Color.red))
                        {
                            _target.IsSpecifyVertices = EditorGUILayout.Toggle("頂点数手動化", _target.IsSpecifyVertices);
                            using (new EditorGUI.DisabledGroupScope(!_target.IsSpecifyVertices))
                            {
                                using (new ExColorScope.GUIBackGround(Color.red))
                                {
                                    _target.Addvertices = EditorGUILayout.IntSlider("頂点数調整スライダー", _target.Addvertices, 0, 360);
                                }
                            }
                        }

                        using (new ExHeaderGroup("円形 / 扇形", s_headerStyle, Color.red))
                        {
                            _target.IsCircle = EditorGUILayout.Toggle("円化 / 扇化", _target.IsCircle);
                            using (new EditorGUI.DisabledGroupScope(_target.IsCircle))
                            {
                                using (new ExColorScope.GUIBackGround(Color.red))
                                {
                                    _target.FanAngle = EditorGUILayout.Slider("角度スライダー", _target.FanAngle, 0.1f, 359.9f);
                                }
                            }
                        }

                        using (new ExHeaderGroup("3D / 2D", s_headerStyle, Color.red))
                        {
                            _target.Is3D = EditorGUILayout.Toggle("3D化 / 2D化", _target.Is3D);
                            using (new EditorGUI.DisabledGroupScope(!_target.Is3D))
                            {
                                using (new ExColorScope.GUIBackGround(Color.red))
                                {
                                    _target.MeshHeight = EditorGUILayout.Slider("高さ", _target.MeshHeight, 0.1f, 200.0f);
                                }
                            }
                        }

                        using (new ExHeaderGroup("生成距離", s_headerStyle, Color.red))
                        {
                            _target.IsTorus = EditorGUILayout.Toggle("トーラス化 / 無効化", _target.IsTorus);
                            using (new ExColorScope.GUIBackGround(Color.red))
                            {
                                if (_target.IsTorus)
                                {
                                    _target.TorusMaxRange = EditorGUILayout.Slider("終点距離スライダー", _target.TorusMaxRange, 0.1f, 200.0f);
                                    _target.TorusMinRange = EditorGUILayout.Slider("始点距離スライダー", _target.TorusMinRange, 0.1f, 200.0f);
                                }
                                else
                                {
                                    _target.MeshRange = EditorGUILayout.Slider("距離スライダー", _target.MeshRange, 0.1f, 200.0f);
                                }
                            }
                        }

                        break;
                    case ExGizmos.MeshMenu.CustumSphere:

                        using (new ExHeaderGroup("CustumSphere", s_headerStyle, Color.red))
                        {
                            using (new ExColorScope.GUIBackGround(Color.red))
                            {
                                _target.SphereMode = (ExGizmos.CustumSphere)EditorGUILayout.EnumPopup("球形状メニュー", _target.SphereMode);
                                _target.SphereWidthAngle = EditorGUILayout.Slider("横角度スライダー", _target.SphereWidthAngle, 0.1f, 359.9f);
                                _target.SphereHeightAngle = EditorGUILayout.Slider("縦角度スライダー", _target.SphereHeightAngle, 0.1f, 179.9f);
                                _target.SphereRange = EditorGUILayout.Slider("距離スライダー", _target.SphereRange, 0.1f, 200.0f);
                                _target.SphereWidthAddvertices = EditorGUILayout.IntSlider("横頂点数調整スライダー", _target.SphereWidthAddvertices, 0, 360);
                                _target.SphereHeightAddvertices = EditorGUILayout.IntSlider("縦頂点数調整スライダー", _target.SphereHeightAddvertices, 0, 180);
                            }
                        }

                        break;
                    case ExGizmos.MeshMenu.Torus:

                        using (new ExHeaderGroup("Torus", s_headerStyle, Color.red))
                        {
                            using (new ExColorScope.GUIBackGround(Color.red))
                            {
                                _target.TorusStartRange = EditorGUILayout.Slider("終点距離スライダー", _target.TorusStartRange, 0.1f, 200.0f);
                                _target.TorusEndRange = EditorGUILayout.Slider("始点距離スライダー", _target.TorusEndRange, 0.1f, 200.0f);
                                _target.TorusWidthAddvertices = EditorGUILayout.IntSlider("横頂点数調整スライダー", _target.TorusWidthAddvertices, 0, 360);
                                _target.TorusHeightAddvertices = EditorGUILayout.IntSlider("縦頂点数調整スライダー", _target.TorusHeightAddvertices, 0, 360);
                            }
                        }

                        break;
                    case ExGizmos.MeshMenu.Moon:

                        using (new ExHeaderGroup("Moon", s_headerStyle, Color.red))
                        {
                            using (new ExColorScope.GUIBackGround(Color.red))
                            {
                                _target.MoonAngle = EditorGUILayout.Slider("角度スライダー", _target.MoonAngle, 0.1f, 359.9f);
                                _target.MoonBaseRange = EditorGUILayout.Slider("土台距離", _target.MoonBaseRange, 0.1f, 200.0f);
                                _target.MoonHoleRange = EditorGUILayout.Slider("切削距離", _target.MoonHoleRange, 0.05f, _target.MoonBaseRange);
                                _target.MoonAddvertices = EditorGUILayout.IntSlider("頂点数調整スライダー", _target.MoonAddvertices, 0, 200);
                            }
                        }

                        break;
                    case ExGizmos.MeshMenu.Star:

                        using (new ExHeaderGroup("Star", s_headerStyle, Color.red))
                        {
                            using (new ExColorScope.GUIBackGround(Color.red))
                            {
                                _target.StarOddRange = EditorGUILayout.Slider("奇数距離スライダー", _target.StarOddRange, 0.1f, 200.0f);
                                _target.StarEvenRange = EditorGUILayout.Slider("偶数距離スライダー", _target.StarEvenRange, 0.1f, 200.0f);
                                _target.StarAddvertices = EditorGUILayout.IntSlider("頂点数調整スライダー", _target.StarAddvertices, 0, 200);
                            }
                            _target.StarIs3D = EditorGUILayout.Toggle("3D化 / 2D化", _target.StarIs3D);
                            using (new ExColorScope.GUIBackGround(Color.red))
                            {
                                if (_target.StarIs3D)
                                {
                                    _target.StarHeight = EditorGUILayout.Slider("高さ", _target.StarHeight, 0.1f, 200.0f);
                                }
                            }
                        }

                        break;
                    case ExGizmos.MeshMenu.Apple:

                        using (new ExHeaderGroup("Apple", s_headerStyle, Color.red))
                        {
                            using (new ExColorScope.GUIBackGround(Color.red))
                            {
                                _target.AppleWidthAddvertices = EditorGUILayout.IntSlider("外周数調整スライダー", _target.AppleWidthAddvertices, 0, 180);
                                _target.AppleHeightAddvertices = EditorGUILayout.IntSlider("頂点数調整スライダー", _target.AppleHeightAddvertices, 0, 200);
                                _target.AppleRange = EditorGUILayout.Slider("距離スライダー", _target.AppleRange, 0, 20.0f);
                                _target.AppleVerticalPless = EditorGUILayout.Slider("縦圧縮　デフォ 1.2f", _target.AppleVerticalPless, 0, 3.0f);
                                _target.AppleWidthCoefficent = EditorGUILayout.Slider("横係数　デフォ 0.15f", _target.AppleWidthCoefficent, -0.2f, 5.0f);
                                _target.AppleHeightCoefficient = EditorGUILayout.Slider("縦係数　デフォ 0.08f", _target.AppleHeightCoefficient, -0.2f, 5.0f);
                            }
                        }

                        break;
                }
            }
        }

        using (new ExFoldOutHeaderScope("アセット作成", ref s_assetsFoldout, Color.cyan))
        {
            if (s_assetsFoldout)
            {
                EditorGUILayout.LabelField("メッシュの名前を入力", s_subStyle);
                _target.MeshName = EditorGUILayout.TextField(_target.MeshName);
                EditorGUILayout.LabelField("保存先のパスを入力", s_subStyle);
                _target.MeshPath = EditorGUILayout.TextField(_target.MeshPath);
                EditorGUILayout.HelpBox("記述ナシで Assets/メッシュの名前.asset を生成\nフォルダのパスをコピーして貼り付けしてね！", MessageType.Info);
                using (new ExColorScope.GUIBackGround(new Color(0, 0.4f, 1.0f, 1.0f)))
                {
                    if (GUILayout.Button("アセット化"))
                    {
                        MeshToAsset(_target.mesh, _target.MeshName, _target.MeshPath);
                    }
                }
            }
        }

        if (EditorGUI.EndChangeCheck())
        {
            var scene = SceneManager.GetActiveScene();
            EditorSceneManager.MarkSceneDirty(scene);
            _target.MeshChoice();
        }
    }

    void MeshToAsset(Mesh mesh, string name, string folderpath)
    {
        if (!mesh)
        {
            ExDebug.LogError("メッシュが入っていません", Color.red);
            return;
        }
        if (name == "")
            name = "MeshToAsset";
        string path;
        if (folderpath == "")
            path = "Assets/";
        else
            path = folderpath + "/";

        bool onlyCheck = false;
        string[] AssetsAsName = AssetDatabase.FindAssets(name);
        if (AssetsAsName.Length == 0)
            onlyCheck = true;

        int index = 1;
        string assetName = name + "_" + index;
        while (!onlyCheck)
        {
            AssetsAsName = AssetDatabase.FindAssets(assetName);
            if (AssetsAsName.Length > 0)
                index++;
            else
            {
                name = assetName;
                break;
            }
            assetName = name + "_" + index;
        }
        AssetDatabase.CreateAsset(mesh, path + name + ".asset");
        AssetDatabase.SaveAssets();

        ExDebug.Log("メッシュをアセットに変換しました。", Color.green);
        ExDebug.Log("メッシュ名：" + name, Color.green);
        ExDebug.Log("生成パス：" + path + name + ".asset", Color.green);

    }
}