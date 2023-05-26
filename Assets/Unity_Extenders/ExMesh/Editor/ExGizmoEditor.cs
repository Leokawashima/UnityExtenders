using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using ExUnityEditor;

[CustomEditor(typeof(ExGizmos))]
public class ExGizmosEditor : Editor
{
    bool
        foldout_menus = true,
        foldout_onAssets = false;

    GUIStyle
        headerstyle,
        substyle;

    ExGizmos gizmo;

    public override void OnInspectorGUI()
    {
        if(gizmo == null)
        {
            gizmo = target as ExGizmos;
        }
        if(headerstyle == null)
        {
            headerstyle = new GUIStyle();
            headerstyle.fontSize = 16;
            headerstyle.fontStyle = FontStyle.Bold;
            headerstyle.normal.textColor = Color.white;
        }
        if(substyle == null)
        {
            substyle = new GUIStyle();
            substyle.fontStyle = FontStyle.Bold;
            substyle.normal.textColor = new Color(0.7f, 1.0f, 1.0f, 1.0f);
        }
        EditorGUI.BeginChangeCheck();

        using(new ExHeaderGroup("ギズモ設定", headerstyle, Color.green))
        {
            gizmo.color = EditorGUILayout.ColorField("ギズモカラー", gizmo.color);
            gizmo.onWireMode = EditorGUILayout.Toggle("ワイヤ化", gizmo.onWireMode);
        }
        using(new ExHeaderGroup(Color.green, 5.0f))
        {
            using(new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("生成ギズモを選択", headerstyle);
                using(new ExColorScope.GUIBackGround(Color.green))
                {
                    gizmo.choicemesh = (ExGizmos.MeshMenu)EditorGUILayout.EnumPopup(gizmo.choicemesh);
                }
            }
        }
        
        using(new ExFoldOutHeaderScope("メニューフォルダ", ref foldout_menus, Color.red))
        {
            if(foldout_menus)
                switch(gizmo.choicemesh)
                {
                    case ExGizmos.MeshMenu.Default:
                        using(new ExHeaderGroup("頂点数", headerstyle, Color.red))
                        {
                            gizmo.onHandMadeMode = EditorGUILayout.Toggle("頂点数手動化", gizmo.onHandMadeMode);
                            using(new EditorGUI.DisabledGroupScope(!gizmo.onHandMadeMode))
                            {
                                using(new ExColorScope.GUIBackGround(Color.red))
                                {
                                    gizmo.addvertices = EditorGUILayout.IntSlider("頂点数調整スライダー", gizmo.addvertices, 0, 360);
                                }
                            }
                        }

                        using(new ExHeaderGroup("円形 / 扇形", headerstyle, Color.red))
                        {
                            gizmo.onCycleMode = EditorGUILayout.Toggle("円化 / 扇化", gizmo.onCycleMode);
                            using(new EditorGUI.DisabledGroupScope(gizmo.onCycleMode))
                            {
                                using(new ExColorScope.GUIBackGround(Color.red))
                                {
                                    gizmo.fanAngle = EditorGUILayout.Slider("角度スライダー", gizmo.fanAngle, 0.1f, 359.9f);
                                }
                            }
                        }

                        using(new ExHeaderGroup("3D / 2D", headerstyle, Color.red))
                        {
                            gizmo.on3DMode = EditorGUILayout.Toggle("3D化 / 2D化", gizmo.on3DMode);
                            using(new EditorGUI.DisabledGroupScope(!gizmo.on3DMode))
                            {
                                using(new ExColorScope.GUIBackGround(Color.red))
                                {
                                    gizmo.meshHeight = EditorGUILayout.Slider("高さ", gizmo.meshHeight, 0.1f, 200.0f);
                                }
                            }
                        }

                        using(new ExHeaderGroup("生成距離", headerstyle, Color.red))
                        {
                            gizmo.onTorusMode = EditorGUILayout.Toggle("トーラス化 / 無効化", gizmo.onTorusMode);
                            using(new ExColorScope.GUIBackGround(Color.red))
                            {
                                if(gizmo.onTorusMode)
                                {
                                    gizmo.torusMaxRange = EditorGUILayout.Slider("終点距離スライダー", gizmo.torusMaxRange, 0.1f, 200.0f);
                                    gizmo.torusMinRange = EditorGUILayout.Slider("始点距離スライダー", gizmo.torusMinRange, 0.1f, 200.0f);
                                }
                                else
                                    gizmo.meshrange = EditorGUILayout.Slider("距離スライダー", gizmo.meshrange, 0.1f, 200.0f);
                            }
                        }

                        break;
                    case ExGizmos.MeshMenu.CustumSphere:

                        using(new ExHeaderGroup("CustumSphere", headerstyle, Color.red))
                        {
                            using(new ExColorScope.GUIBackGround(Color.red))
                            {
                                gizmo.sphereMode = (ExGizmos.CustumSphere)EditorGUILayout.EnumPopup("球形状メニュー", gizmo.sphereMode);
                                gizmo.sphere_w_angle = EditorGUILayout.Slider("横角度スライダー", gizmo.sphere_w_angle, 0.1f, 359.9f);
                                gizmo.sphere_h_angle = EditorGUILayout.Slider("縦角度スライダー", gizmo.sphere_h_angle, 0.1f, 179.9f);
                                gizmo.sphere_range = EditorGUILayout.Slider("距離スライダー", gizmo.sphere_range, 0.1f, 200.0f);
                                gizmo.sphere_w_addvertices = EditorGUILayout.IntSlider("横頂点数調整スライダー", gizmo.sphere_w_addvertices, 0, 360);
                                gizmo.sphere_h_addvertices = EditorGUILayout.IntSlider("縦頂点数調整スライダー", gizmo.sphere_h_addvertices, 0, 180);
                            }
                        }

                        break;
                    case ExGizmos.MeshMenu.Torus:

                        using(new ExHeaderGroup("Torus", headerstyle, Color.red))
                        {
                            using(new ExColorScope.GUIBackGround(Color.red))
                            {
                                gizmo.torus_s_range = EditorGUILayout.Slider("終点距離スライダー", gizmo.torus_s_range, 0.1f, 200.0f);
                                gizmo.torus_e_range = EditorGUILayout.Slider("始点距離スライダー", gizmo.torus_e_range, 0.1f, 200.0f);
                                gizmo.torus_w_addvertices = EditorGUILayout.IntSlider("横頂点数調整スライダー", gizmo.torus_w_addvertices, 0, 360);
                                gizmo.torus_h_addvertices = EditorGUILayout.IntSlider("縦頂点数調整スライダー", gizmo.torus_h_addvertices, 0, 360);
                            }
                        }

                        break;
                    case ExGizmos.MeshMenu.Moon:

                        using(new ExHeaderGroup("Moon", headerstyle, Color.red))
                        {
                            using(new ExColorScope.GUIBackGround(Color.red))
                            {
                                gizmo.Moon_angle = EditorGUILayout.Slider("角度スライダー", gizmo.Moon_angle, 0.1f, 359.9f);
                                gizmo.Moon_base_range = EditorGUILayout.Slider("土台距離", gizmo.Moon_base_range, 0.1f, 200.0f);
                                gizmo.Moon_hole_range = EditorGUILayout.Slider("切削距離", gizmo.Moon_hole_range, 0.05f, gizmo.Moon_base_range);
                                gizmo.Moon_addvertices = EditorGUILayout.IntSlider("頂点数調整スライダー", gizmo.Moon_addvertices, 0, 200);
                            }
                        }

                        break;
                    case ExGizmos.MeshMenu.Star:

                        using(new ExHeaderGroup("Star", headerstyle, Color.red))
                        {
                            using(new ExColorScope.GUIBackGround(Color.red))
                            {
                                gizmo.star_odd_range = EditorGUILayout.Slider("奇数距離スライダー", gizmo.star_odd_range, 0.1f, 200.0f);
                                gizmo.star_even_range = EditorGUILayout.Slider("偶数距離スライダー", gizmo.star_even_range, 0.1f, 200.0f);
                                gizmo.star_addvertices = EditorGUILayout.IntSlider("頂点数調整スライダー", gizmo.star_addvertices, 0, 200);
                            }
                            gizmo.star_3D = EditorGUILayout.Toggle("3D化 / 2D化", gizmo.star_3D);
                            using(new ExColorScope.GUIBackGround(Color.red))
                            {
                                if(gizmo.star_3D)
                                    gizmo.star_height = EditorGUILayout.Slider("高さ", gizmo.star_height, 0.1f, 200.0f);
                            }
                        }

                        break;
                    case ExGizmos.MeshMenu.Apple:

                        using(new ExHeaderGroup("Apple", headerstyle, Color.red))
                        {
                            using(new ExColorScope.GUIBackGround(Color.red))
                            {
                                gizmo.apple_width_addvertices = EditorGUILayout.IntSlider("外周数調整スライダー", gizmo.apple_width_addvertices, 0, 180);
                                gizmo.apple_height_addvertices = EditorGUILayout.IntSlider("頂点数調整スライダー", gizmo.apple_height_addvertices, 0, 200);
                                gizmo.apple_range = EditorGUILayout.Slider("距離スライダー", gizmo.apple_range, 0, 20.0f);
                                gizmo.apple_verticalpless = EditorGUILayout.Slider("縦圧縮　デフォ 1.2f", gizmo.apple_verticalpless, 0, 3.0f);
                                gizmo.apple_width_coefficent = EditorGUILayout.Slider("横係数　デフォ 0.15f", gizmo.apple_width_coefficent, -0.2f, 5.0f);
                                gizmo.apple_height_coefficient = EditorGUILayout.Slider("縦係数　デフォ 0.08f", gizmo.apple_height_coefficient, -0.2f, 5.0f);
                            }
                        }

                        break;
                }
        }

        using(new ExFoldOutHeaderScope("アセット作成", ref foldout_onAssets, Color.cyan))
        {
            if(foldout_onAssets)
            {
                EditorGUILayout.LabelField("メッシュの名前を入力", substyle);
                gizmo.meshName = EditorGUILayout.TextField(gizmo.meshName);
                EditorGUILayout.LabelField("保存先のパスを入力", substyle);
                gizmo.meshPath = EditorGUILayout.TextField(gizmo.meshPath);
                EditorGUILayout.HelpBox("記述ナシで Assets/メッシュの名前.asset を生成\nフォルダのパスをコピーして貼り付けしてね！", MessageType.Info);
                using(new ExColorScope.GUIBackGround(new Color(0, 0.4f, 1.0f, 1.0f)))
                {
                    if(GUILayout.Button("アセット化"))
                        MeshToAsset(gizmo.mesh, gizmo.meshName, gizmo.meshPath);
                }
            }
        }

        if(EditorGUI.EndChangeCheck())
        {
            var scene = SceneManager.GetActiveScene();
            EditorSceneManager.MarkSceneDirty(scene);
            gizmo.MeshChoice();
        } 
    }

    void MeshToAsset(Mesh mesh, string name, string folderpath)
    {
        if(!mesh)
        {
            ExDebug.LogError("メッシュが入っていません", Color.red);
            return;
        }
        if(name == "")
            name = "MeshToAsset";
        string path;
        if(folderpath == "")
            path = "Assets/";
        else
            path = folderpath + "/";

        bool onlyCheck = false;
        string[] AssetsAsName = AssetDatabase.FindAssets(name);
        if(AssetsAsName.Length == 0)
            onlyCheck = true;

        int index = 1;
        string assetName = name + "_" + index;
        while(!onlyCheck)
        {
            AssetsAsName = AssetDatabase.FindAssets(assetName);
            if(AssetsAsName.Length > 0)
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