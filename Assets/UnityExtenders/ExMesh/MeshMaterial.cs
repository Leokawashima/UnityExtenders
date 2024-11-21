using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メッシュ生成を便利にするための構造体：法線、ＵＶに関しては未完、頂点カラーに関しては対応するか未定。
/// </summary>
public struct MeshMaterial
{
    public Vector3[] vertices;
    public List<int> triangles;
    public Vector3[] normals;
    public Vector2[] uvs;

    private const int tri_verts = 3;
    private const int quad_verts = 6;
    private const int quad_Normals = 4;

    public MeshMaterial(Vector3[] vert, List<int> tri)
    {
        this.vertices = vert;
        this.triangles = tri;
        this.normals = new Vector3[0];
        this.uvs = new Vector2[0];
    }
    public MeshMaterial(int vert, int tri)
    {
        this.vertices = new Vector3[vert];
        this.triangles = new List<int>(tri * tri_verts);
        this.normals = new Vector3[0];
        this.uvs = new Vector2[0];
    }
    public MeshMaterial(int vert, int tri, int quad)
    {
        this.vertices = new Vector3[vert];
        this.triangles = new List<int>(tri * tri_verts + quad * quad_verts);
        this.normals = new Vector3[0];
        this.uvs = new Vector2[0];
    }

    public void SetVertex(int index, float x, float y, float z)
    {
        this.vertices[index] = new Vector3(x, y, z);
    }
    public void SetTriangle(int start_index, int middle_index, int end_index)
    {
        this.triangles.AddRange(new int[3] { start_index, middle_index, end_index });
    }
    public void SetQuad(int left_d, int left_u, int right_d, int right_u)
    {
        this.triangles.AddRange(new int[6] { left_d, left_u, right_d, right_u, right_d, left_u });
    }
    public void SetUvs(int index, float uv_x, float uv_y)
    {
        this.uvs[index] = new Vector2(uv_x, uv_y);
    }
}