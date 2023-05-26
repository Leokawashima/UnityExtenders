using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mesh生成をお助けするクラス
/// </summary>

public static class MeshSupport
{
    public static Vector3 SetVertex(this Vector3 vec3, float x, float y, float z)
    {
        return new Vector3(x, y, z);
    }
    public static List<int> SetTriangle(this List<int> list, int start, int middle, int end)
    {
        list.AddRange(new List<int>(3) { start, middle, end });
        return list;
    }
    public static int[] SetTriangle(this int[] array, int head_index, int start, int middle, int end)
    {
        array[head_index] = start;
        array[head_index + 1] = middle;
        array[head_index + 2] = end;
        return array;
    }
    /// <summary>
    /// 四角形メッシュの頂点配列をケツから突っ込む拡張メソッド
    /// </summary>
    /// <param name="list">頂点情報を入れるリスト　into vertex for List</param>
    /// <param name="left_d">左下頂点　Left and Bottom Vertex</param>
    /// <param name="left_u">左上頂点　Left and Top Vertex</param>
    /// <param name="right_d">右下頂点　Right and Bottom Vertex</param>
    /// <param name="right_u">右上頂点　Right and Top Vertex</param>
    public static List<int> SetQuad(this List<int> list, int left_d, int left_u, int right_d, int right_u)
    {
        ///　頂点関係図
        /// 
        /// left_u　②                     left_u　③ -→　right_u　①
        ///   ↑　　＼                            ┏　　　　　│
        ///   │　　　＼                            ＼　　　 │
        ///   │　　　　＼                            ＼     │
        ///   │　　　　  ┛                            ＼   ↓
        /// left_d　①　←-right_d　③                      right_d　②
        /// 
        /// left_d  →   left_u  →   right_d の三角
        /// right_u →   right_d →   left_u  の三角

        list.AddRange(new List<int>(6) { left_d, left_u, right_d, right_u, right_d, left_u });
        return list;
    }
    /// <summary>
    /// 四角形メッシュの頂点配列を引数から引数 + 5 まで突っ込む拡張メソッド
    /// </summary>
    /// <param name="array">頂点情報を入れる配列　into vertex for array[head_index ～ head_index + 5]</param>
    /// <param name="left_d">左下頂点　Left and Bottom Vertex</param>
    /// <param name="left_u">左上頂点　Left and Top Vertex</param>
    /// <param name="right_d">右下頂点　Right and Bottom Vertex</param>
    /// <param name="right_u">右上頂点　Right and Top Vertex</param>
    public static int[] SetQuad(this int[] array, int head_index, int left_d, int left_u, int right_d, int right_u)
    {
        array[head_index] = left_d;
        array[head_index + 1] = left_u;
        array[head_index + 2] = right_d;
        array[head_index + 3] = right_u;
        array[head_index + 4] = right_d;
        array[head_index + 5] = left_u;
        return array;
    }

    public static Vector3[] CreateVertices(int vertices)
    {
        return new Vector3[vertices];
    }
    public static Vector3[] CreateVertices(this Vector3[] vec3, int vertices)
    {
        return new Vector3[vertices];
    }
    public static List<int> CreatePolygon()
    {
        return new List<int>();
    }
    public static List<int> CreatePolygon(this List<int> list)
    {
        return new List<int>();
    }
    public static List<int> CreatePolygon(int tri_surfaces)
    {
        return new List<int>(tri_surfaces * 3);
    }
    public static List<int> CreatePolygon(this List<int> list, int tri_surfaces)
    {
        return new List<int>(tri_surfaces * 3);
    }
    public static List<int> CreatePolygon(int tri_surfaces, int quad_surfaces)
    {
        return new List<int>(tri_surfaces * 3 + quad_surfaces * 6);
    }
    public static List<int> CreatePolygon(this List<int> list, int tri_surfaces, int quad_surfaces)
    {
        return new List<int>(tri_surfaces * 3 + quad_surfaces * 6);
    }
    public static Mesh CreateMesh(Vector3[] vertices, List<int> triangles)
    {
        Mesh _mesh = new Mesh();

        _mesh.SetVertices(vertices);
        _mesh.SetTriangles(triangles, 0);
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();

        return _mesh;
    }
    public static Mesh CreateMesh(this Mesh mesh, Vector3[] vertices, List<int> triangles)
    {
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
    public static Mesh SetMeshMaterial(this Mesh mesh, MeshMaterial mesh_material)
    {
        mesh.SetVertices(mesh_material.vertices);
        mesh.SetTriangles(mesh_material.triangles, 0);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
    public static Mesh MatToMesh(this MeshMaterial mesh_material)
    {
        Mesh _mesh = new Mesh();

        _mesh.SetVertices(mesh_material.vertices);
        _mesh.SetTriangles(mesh_material.triangles, 0);
        _mesh.SetUVs(0, mesh_material.uvs);
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();

        return _mesh;
    }
}

/// 個人的にメッシュ作る人に聴いて回りたいのが
/// 
/// 1----3         0----1
/// |    |　←-→　|    |
/// 0----2         2----3
/// 
///右左どちらでもいいけれど、0→1→2　を描いてから　3→2→1　派閥　VS　1→3→2　派閥　これデータ的に見たらどっちが正解なんだろう…てめっちゃ気になる。
///
/// /＼　 個人的には綺麗さ重視であったり、
/// ＼/ ←みたいに四角一枚描いたらすぐ角度が変わる図形は3,2,1で
///  □□□　見た目としては一面同じだが計算次第で波をうつような並列的な
///  □□□　←のように配置されたポリゴンなら左上から右に0,1,2,3...と頂点を置くだろうから開始地点を揃える1→3→2を使いたいなー…って感じがする。
///  実際描画したら結果変わらないし内部的に同じものとして扱われてるならどうでもいい。