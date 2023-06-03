using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 解説フォーマットクラス　継承先を一つ噛ませてコンストラクタで名前を代入すること。
/// </summary>
[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public abstract class MeshLearningFormatClass : MonoBehaviour
{
    protected MeshFilter meshFilter = null;
    protected MeshRenderer meshRenderer = null;
    protected Material meshMaterial = null;

    protected string LearningLevelName = "FormatClass";
    protected string MeshName = "Mesh：Name";
    protected string MaterialName = "Material：Name";

    protected static Color pink { get { return new Color(0.82f, 0.08f, 0.48f, 1.0f); } }
    protected static Color orange { get { return new Color(1.0f, 0.65f, 0, 1.0f); } }
    protected static Color purple { get { return new Color(0.5f, 0, 0.5f, 1.0f); } }

#if UNITY_EDITOR
    [ContextMenu("CleanUp")]
    protected void CleanUp()
    {
        CleanUp(gameObject, this);
    }
    protected static void CleanUp(GameObject go, MeshLearningFormatClass target)
    {
        DestroyImmediate(target);
        DestroyImmediate(go.GetComponent<MeshRenderer>());
        DestroyImmediate(go.GetComponent<MeshFilter>());
    }
#endif
    protected void Reset()
    {
        if(!meshFilter)
        {
            if(!(meshFilter = GetComponent<MeshFilter>()))
                meshFilter = gameObject.AddComponent<MeshFilter>();
        }
        if(!meshRenderer)
        {
            if(!(meshRenderer = GetComponent<MeshRenderer>()))
                meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }
    }

    protected void SetMeshOnMeshFilter(Mesh mesh)
    {
        mesh.name = LearningLevelName + "：" + MeshName;
        meshFilter.mesh = mesh;
    }

    protected void SetColorOnMeshRenderer(Color color)
    {
        if(!meshMaterial)
        {
            meshMaterial = new Material(Shader.Find("Standard"));
            meshMaterial.name = LearningLevelName + "：" + MaterialName;
            meshRenderer.material = meshMaterial;
        }
        meshMaterial.color = color;
    }
}

/// <summary>
/// 徹底解説クラス
/// </summary>
public abstract class BeginnerMeshLearningFormatClass : MeshLearningFormatClass
{
    protected BeginnerMeshLearningFormatClass()
    {
        LearningLevelName = "徹底解説";
        MeshName = "星型メッシュ";
        MaterialName = "星型メッシュマテリアル";
    }
}
#if UNITY_EDITOR
/// <summary>
/// 徹底解説クラスエディター
/// </summary>
[CustomEditor(typeof(BeginnerMeshLearning))]
public class BeginnerMeshEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        BeginnerMeshLearning b_mesh = target as BeginnerMeshLearning;
        GUI.backgroundColor = Color.blue;
        if(GUILayout.Button("関数呼び出しボタン", GUILayout.Height(30)))
            b_mesh.BeginnerCreateMesh();
    }
}
#endif