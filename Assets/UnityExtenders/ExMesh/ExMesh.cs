using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 扇型の様々なメッシュを生成するクラス
/// </summary>

/// 下を参考にループでのスコープを意識するか計算回数を意識するか等々学びつつ作った
/// やってくうちにどんどん規模が大きくなってコメントアウトや計算の掟、命名規則等々10回以上変更しているので、
/// 計算の無駄や他と違うルールや名前のものがあるかもしれない　時間に余裕が生まれたら一新しつつ拡張して何でもかんでも誰でも生成できる物にしていきたい
/// 参考URL：　https://gazushige.com/blog/6e50f617-b9cf-4f2e-ac7d-59c9466a5d6b
/// 制作終盤に見つけた初めからこれ使えばよかったって感じのコライダークラス
/// 自分でいろいろいじくったから理解が深まったし結果オーライってことで
/// 参考URL：　https://baba-s.hatenablog.com/entry/2019/03/04/090000
public static class ExMesh
{
    #region 数学定数
    /// <summary>
    /// 360度ラジアン　キャッシュ用　360 * Mathf.DegToRad = π * 2 = 6.2831853071795...
    /// </summary>
    public const float TwoPi = Mathf.PI * 2.0f;
    /// <summary>
    /// 180度ラジアン　上下の変数宣言に合わせて宣言している　精度や計算負荷を考えて変更しても良い
    /// </summary>
    public const float OnePi = Mathf.PI;
    /// <summary>
    /// 90度ラジアン　キャッシュ用　90 * Mathf.DegToRad = π / 2 = 1.5707963267948...
    /// </summary>
    public const float TwoDePi = Mathf.PI / 2.0f;
    /// <summary>
    /// 1度ラジアン　こちらも上下に合わせて宣言している　精度や計算負荷を考えて変更しても良い
    /// </summary>
    public const float DegToRad = Mathf.Deg2Rad;
    #endregion 数学定数

    #region Circle

    #region CircleMesh

    /// <summary>
    /// [円型メッシュ]を生成して返す(テスト高速型)：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="range">生成する[円型メッシュ]の距離</param>
    /// <param name="addvertices">生成する[円型メッシュ]の追加する頂点の数</param>
    /// <returns>頂点数指定の[円型メッシュ]</returns>
    public static Mesh FastCreateCircleMesh(float range, int addvertices)
    {
        //値補正
        range = range <= 0 ? 5f : range;
        addvertices = addvertices < 0　? 180 : addvertices;

        //頂点　頂点インデックス定義
        int _roundvert = addvertices + 3;
        var _vertices = new Vector3[_roundvert + 1];
        var _triangles = new List<int>(_roundvert * 3);

        //計算キャッシュ
        float _cutAngle = TwoPi / (float)_roundvert;

        //頂点　頂点インデックス代入
        _vertices[0] = Vector3.zero;
        for(int i = 0; i < _roundvert; i++)
        {
            float i_angle = i * _cutAngle;
            _vertices[i + 1] = new Vector3(Mathf.Sin(i_angle) * range, 0, Mathf.Cos(i_angle) * range);
            if(i > 0)
                _triangles.AddRange(new int[3] { 0, i, i + 1 });
        }
        _triangles.AddRange(new int[] { 0, addvertices, 1 });

        //メッシュ生成
        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();

        return _mesh;
    }
    /// <summary>
    /// [円型メッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="range">生成する[円型メッシュ]の距離</param>
    /// <param name="addvertices">生成する[円型メッシュ]の追加する頂点の数</param>
    /// <returns>頂点数指定の[円型メッシュ]</returns>
    public static Mesh CreateCircleMesh(float range, int addvertices)
    {
        //値補正
        range = range <= 0 ? 5f : range;
        addvertices = addvertices < 0 ? 180 : addvertices;

        //頂点　頂点インデックス定義
        int _roundvert = addvertices + 3;
        var _vertices = new Vector3[_roundvert + 1];
        var _triangles = new List<int>(_roundvert * 3);

        //計算キャッシュ
        float _cutAngle = TwoPi / (float)_roundvert;

        //頂点　頂点インデックス代入
        _vertices[0] = Vector3.zero;
        for (int i = 0; i < _roundvert; i++)
        {
            float i_angle = i * _cutAngle;
            _vertices[i + 1] = new Vector3(Mathf.Sin(i_angle) * range, 0, Mathf.Cos(i_angle) * range);
            _triangles.AddRange(new int[3] { 0, 1 + (1 + i) % _roundvert, 1 + (2 + i) % _roundvert });
        }

        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();

        return _mesh;
    }
    /// <summary>
    /// [円型メッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="range">生成する[円型メッシュ]の距離</param>
    /// <returns>頂点数自動の[円型メッシュ]</returns>
    public static Mesh CreateCircleMesh(float range)
    {
        return CreateCircleMesh(range, 180);
    }
    /// <summary>
    /// [円型メッシュ]を生成して返す：void型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数指定の[円型メッシュ]</param>
    /// <param name="range">生成する[円型メッシュ]の距離</param>
    /// <param name="addvertices">生成する[円型メッシュ]の追加する頂点の数</param>
    public static void CreateCircleMesh(out Mesh mesh, float range, int addvertices)
    {
        mesh = CreateCircleMesh(range, addvertices);
    }
    /// <summary>
    /// [円型メッシュ]を生成して返す：void型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数自動の[円型メッシュ]</param>
    /// <param name="range">生成する[円型メッシュ]の距離</param>
    public static void CreateCircleMesh(out Mesh mesh, float range)
    {
        mesh = CreateCircleMesh(range);
    }

    #endregion CircleMesh

    #region CircleTorusMesh

    /// <summary>
    /// [円型トーラスメッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="s_range">生成する[円型トーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[円型トーラスメッシュ]の終点距離</param>
    /// <param name="addvertices">生成する[円型トーラスメッシュ]の追加する頂点の数</param>
    /// <returns>頂点数指定の[円型トーラスメッシュ]</returns>
    public static Mesh CreateCircleTorusMesh(float s_range, float e_range, int addvertices)
    {
        //値補正
        s_range = s_range <= 0 ? 2.5f : s_range;
        e_range = e_range <= 0 ? 2.5f : e_range;
        addvertices = addvertices < 0 ? 180 : addvertices;

        //頂点　頂点インデックス定義
        int _roundvert = addvertices + 3;
        var _vertices = new Vector3[_roundvert * 2];
        var _triangles = new List<int>(_roundvert * 6);

        //計算キャッシュ
        float _cutAngle = TwoPi / (float)_roundvert;
        float _distance = s_range + e_range;

        //頂点　頂点インデックス代入
        for(int i = 0; i < _roundvert; i++)
        {
            int mul2 = i * 2;
            float
                i_angle = i * _cutAngle,
                x = Mathf.Sin(i_angle),
                y = Mathf.Cos(i_angle);
            _vertices[mul2] = new Vector3(x * s_range, 0, y * s_range);
            _vertices[mul2 + 1] = new Vector3(x * _distance, 0, y * _distance);
        }
        for(int i = 0, roundMinus1 = _roundvert - 1; i < roundMinus1; i++)
        {
            int mul2 = i * 2;
            _triangles.AddRange(new int[] { mul2, mul2 + 1, mul2 + 2 });
            _triangles.AddRange(new int[] { mul2 + 3, mul2 + 2, mul2 + 1 });
        }
        _triangles.AddRange(new int[] { _vertices.Length - 2, _vertices.Length - 1, 0 });
        _triangles.AddRange(new int[] { 1, 0, _vertices.Length - 1 });

        //メッシュ生成
        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();

        return _mesh;
    }
    /// <summary>
    /// [円型トーラスメッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="s_range">生成する[円型トーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[円型トーラスメッシュ]の終点距離</param>
    /// <returns>頂点数自動の[円型トーラスメッシュ]</returns>
    public static Mesh CreateCircleTorusMesh(float s_range, float e_range)
    {
        return CreateCircleTorusMesh(s_range, e_range, 180);
    }
    /// <summary>
    /// [円型トーラスメッシュ]を生成して返す：void型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数指定の[円型トーラスメッシュ]</param>
    /// <param name="s_range">生成する[円型トーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[円型トーラスメッシュ]の終点距離</param>
    /// <param name="addvertices">生成する[円型トーラスメッシュ]の追加する頂点の数</param>
    public static void CreateCircleTorusMesh(out Mesh mesh, float s_range, float e_range, int addvertices)
    {
        mesh = CreateCircleTorusMesh(s_range, e_range, addvertices);
    }
    /// <summary>
    /// [円型トーラスメッシュ]を生成して返す：void型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数自動の[円型トーラスメッシュ]</param>
    /// <param name="s_range">生成する[円型トーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[円型トーラスメッシュ]の終点距離</param>
    public static void CreateCircleTorusMesh(out Mesh mesh, float s_range, float e_range)
    {
        mesh = CreateCircleTorusMesh(s_range, e_range);
    }

    #endregion CircleTorusMesh

    #region CircleMesh3D

    /// <summary>
    /// [円型3Dメッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="range">生成する[円型3Dメッシュ]の距離</param>
    /// <param name="height">生成する[円型3Dメッシュ]の高さ</param>
    /// <param name="addvertices">生成する[円型3Dメッシュ]の追加する頂点の数</param>
    /// <returns>頂点数指定の[円型3Dメッシュ]</returns>
    public static Mesh CreateCircle3DMesh(float range, float height, int addvertices)
    {
        //値補正
        range = range <= 0 ? 5f : range;
        height = height <= 0 ? 5f : height;
        addvertices = addvertices < 0 ? 180 : addvertices;

        //頂点　頂点インデックス定義
        int _roundvert = addvertices + 3;
        var _vertices = new Vector3[_roundvert * 2 + 2];
        var _triangles = new List<int>(_roundvert * 12);

        //計算キャッシュ
        float _cutAngle = TwoPi / (float)_roundvert;
        float _halfHeight = height / 2f;

        //頂点　頂点インデックス代入
        _vertices[0] = Vector3.up * _halfHeight;
        _vertices[1] = Vector3.down * _halfHeight;
        for (int i = 0; i < _roundvert; i++)
        {
            int mul2Plus2 = i * 2 + 2;
            float
                i_angle = i * _cutAngle,
                x = Mathf.Sin(i_angle) * range,
                y = Mathf.Cos(i_angle) * range;
            _vertices[mul2Plus2] = new Vector3(x, _halfHeight, y);
            _vertices[mul2Plus2 + 1] = new Vector3(x, -_halfHeight, y);
        }
        for(int i = 0, roundMinus1 = _roundvert - 1; i < roundMinus1; i++)
        {
            int mul2Plus2 = i * 2 + 2;
            _triangles.AddRange(new int[] { 0, mul2Plus2, mul2Plus2 + 2 });
            _triangles.AddRange(new int[] { 1, mul2Plus2 + 3, mul2Plus2 + 1 });
            _triangles.AddRange(new int[] { mul2Plus2, mul2Plus2 + 1, mul2Plus2 + 2 });
            _triangles.AddRange(new int[] { mul2Plus2 + 3, mul2Plus2 + 2, mul2Plus2 + 1 });
        }
        _triangles.AddRange(new int[] { 0, _vertices.Length - 2, 2 });
        _triangles.AddRange(new int[] { 1, 3, _vertices.Length - 1 });
        _triangles.AddRange(new int[] { _vertices.Length - 2, _vertices.Length - 1, 2 });
        _triangles.AddRange(new int[] { 3, 2, _vertices.Length - 1 });

        //メッシュ作成
        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();

        return _mesh;
    }
    /// <summary>
    /// [円型3Dメッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="range">生成する[円型3Dメッシュ]の距離</param>
    /// <param name="height">生成する[円型3Dメッシュ]の高さ</param
    /// <returns>頂点数自動の[円型3Dメッシュ]</returns>
    public static Mesh CreateCircle3DMesh(float range, float height)
    {
        return CreateCircle3DMesh(range, height, 180);
    }
    /// <summary>
    /// [円型3Dメッシュ]を生成して返す：void型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数指定の[円型3Dメッシュ]</param>
    /// <param name="range">生成する[円型3Dメッシュ]の距離</param>
    /// <param name="height">生成する[円型3Dメッシュ]の高さ</param>
    /// <param name="addvertices">生成する[円型3Dメッシュ]の追加する頂点の数</param>
    public static void CreateCircle3DMesh(out Mesh mesh, float range, float height, int addvertices)
    {
        mesh = CreateCircle3DMesh(range, height, addvertices);
    }
    /// <summary>
    /// [円型3Dメッシュ]を生成して返す：void型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数自動の[円型3Dメッシュ]</param>
    /// <param name="range">生成する[円型3Dメッシュ]の距離</param>
    /// <param name="height">生成する[円型3Dメッシュ]の高さ</param>
    public static void CreateCircle3DMesh(out Mesh mesh, float range, float height)
    {
        mesh = CreateCircle3DMesh(range, height);
    }

    #endregion CircleMesh3D

    #region CircleTorusMesh3D

    /// <summary>
    /// [円型3Dトーラスメッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="s_range">生成する[円型3Dトーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[円型3Dトーラスメッシュ]の終点距離</param>
    /// <param name="height">生成する[円型3Dトーラスメッシュ]の高さ</param>
    /// <param name="addvertices">生成する[円型3Dトーラスメッシュ]の追加する頂点数</param>
    /// <returns>頂点数手動の[円型3Dトーラスメッシュ]</returns>
    public static Mesh CreateCircleTorus3DMesh(float s_range, float e_range, float height, int addvertices)
    {
        //値補正
        s_range = s_range <= 0 ? 2.5f : s_range;
        e_range = e_range <= 0 ? 2.5f : e_range;
        height = height <= 0 ? 5f : height;
        addvertices = addvertices < 0 ? 180 : addvertices;

        //頂点　頂点インデックス定義
        int _roundvert = addvertices + 3;
        var _vertices = new Vector3[_roundvert * 4];
        var _triangles = new List<int>(_roundvert * 8);

        //計算キャッシュ
        float _cutAngle = TwoPi / (float)_roundvert;
        float _distance = s_range + e_range;
        float _halfHeight = height / 2f;

        //頂点　頂点インデックス代入
        for(int i = 0; i < _roundvert; i++)
        {
            int mul4 = i * 4;
            float
                x = Mathf.Sin(i * _cutAngle),
                y = Mathf.Cos(i * _cutAngle),
                x_in = x * s_range,
                y_in = y * s_range,
                x_out = x * _distance,
                y_out = y * _distance;
            _vertices[mul4] = new Vector3(x_in, _halfHeight, y_in);
            _vertices[mul4 + 1] = new Vector3(x_in, -_halfHeight, y_in);
            _vertices[mul4 + 2] = new Vector3(x_out, _halfHeight, y_out);
            _vertices[mul4 + 3] = new Vector3(x_out, -_halfHeight, y_out);
        }
        for(int i = 0, surfaces_minusOne = _roundvert - 1; i < surfaces_minusOne; i++)
        {
            int mul4 = i * 4; int mul4odd = i * 4 + 1;
            _triangles.AddRange(new int[] { mul4, mul4 + 2, mul4 + 6 });
            _triangles.AddRange(new int[] { mul4 + 6, mul4 + 4, mul4 });
            _triangles.AddRange(new int[] { mul4odd + 2, mul4odd, mul4odd + 4 });
            _triangles.AddRange(new int[] { mul4odd + 4, mul4odd + 6, mul4odd + 2 });
            _triangles.AddRange(new int[] { mul4 + 2, mul4odd + 2, mul4odd + 6 });
            _triangles.AddRange(new int[] { mul4odd + 6, mul4 + 6, mul4 + 2 });
            _triangles.AddRange(new int[] { mul4 + 4, mul4odd + 4, mul4odd });
            _triangles.AddRange(new int[] { mul4odd, mul4, mul4 + 4 });
        }
        _triangles.AddRange(new int[] { _vertices.Length - 4, _vertices.Length - 2, 0 });
        _triangles.AddRange(new int[] { 2, 0, _vertices.Length - 2 });
        _triangles.AddRange(new int[] { _vertices.Length - 1, _vertices.Length - 3, 1 });
        _triangles.AddRange(new int[] { 3, _vertices.Length - 1, 1 });
        _triangles.AddRange(new int[] { _vertices.Length - 2, _vertices.Length - 1, 3 });
        _triangles.AddRange(new int[] { 3, 2, _vertices.Length - 2 });
        _triangles.AddRange(new int[] { 0, 1, _vertices.Length - 3 });
        _triangles.AddRange(new int[] { _vertices.Length - 3, _vertices.Length - 4, 0 });

        //メッシュ生成
        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();

        return _mesh;
    }
    /// <summary>
    /// [円型3Dトーラスメッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="s_range">生成する[円型3Dトーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[円型3Dトーラスメッシュ]の終点距離</param>
    /// <param name="height">生成する[円型3Dトーラスメッシュ]の高さ</param>
    /// <returns>頂点数自動の[円型3Dトーラスメッシュ]</returns>
    public static Mesh CreateCircleTorus3DMesh(float s_range, float e_range, float height)
    {
        return CreateCircleTorus3DMesh(s_range, e_range, height, 180);
    }
    /// <summary>
    /// [円型3Dトーラスメッシュ]を生成して返す：void型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数手動の[円型3Dトーラスメッシュ]</param>
    /// <param name="s_range">生成する[円型3Dトーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[円型3Dトーラスメッシュ]の終点距離</param>
    /// <param name="height">生成する[円型3Dトーラスメッシュ]の高さ</param>
    /// <param name="addvertices">生成する[円型3Dトーラスメッシュ]の追加する頂点数</param>
    public static void CreateCircleTorus3DMesh(out Mesh mesh, float s_range, float e_range, float height, int addvertices)
    {
        mesh = CreateCircleTorus3DMesh(s_range, e_range, height, addvertices);
    }
    /// <summary>
    /// [円型3Dトーラスメッシュ]を生成して返す：void型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数自動の[円型3Dトーラスメッシュ]</param>
    /// <param name="s_range">生成する[円型3Dトーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[円型3Dトーラスメッシュ]の終点距離</param>
    /// <param name="height">生成する[円型3Dトーラスメッシュ]の高さ</param>
    public static void CreateCircleTorus3DMesh(out Mesh mesh, float s_range, float e_range, float height)
    {
        mesh = CreateCircleTorus3DMesh(s_range, e_range, height);
    }

    #endregion CircleTorusMesh3D

    #endregion Circle

    #region Fan

    #region FanMesh

    /// <summary>
    /// [扇型メッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="angle">生成する[扇形メッシュ]の角度</param>
    /// <param name="range">生成する[扇形メッシュ]の距離</param>
    /// <param name="addvertices">生成する[扇形メッシュ]の追加する頂点の数</param>
    /// <returns>頂点数指定の[扇形メッシュ]</returns>
    public static Mesh CreateFanMesh(float angle, float range, int addvertices)
    {
        //値補正
        angle = angle <= 0 ? 180.0f : angle;
        range = range <= 0 ? 5.0f : range;
        addvertices = addvertices < 0 ? Mathf.CeilToInt(angle) : addvertices;

        //頂点　頂点インデックス定義
        int _roundvert = addvertices + 3;
        var _vertices = new Vector3[_roundvert + 1];
        var _triangles = new List<int>((addvertices + 2) * 3);

        //計算キャッシュ
        float _i_angle = -angle / 2f * DegToRad;
        float _cutAngle = angle / (float)(_roundvert - 1) * DegToRad;

        //頂点　頂点インデックス代入
        _vertices[0] = Vector3.zero;
        for(int i = 1, surfaces = _roundvert - 1; i <= _roundvert; i++)
        {
            float
                _x = Mathf.Sin(_i_angle) * range,
                _y = Mathf.Cos(_i_angle) * range;
            _vertices[i] =  new Vector3(_x, 0, _y);
            if(i <= surfaces)
                _triangles.AddRange(new int[] { 0, i, i + 1 });
            _i_angle += _cutAngle;
        }

        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();

        return _mesh;
    }
    /// <summary>
    /// [扇型メッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="angle">生成する[扇形メッシュ]の角度</param>
    /// <param name="range">生成する[扇形メッシュ]の距離</param>
    /// <returns>頂点数自動の[扇形メッシュ]</returns>
    public static Mesh CreateFanMesh(float angle, float range)
    {
        return CreateFanMesh(angle, range, Mathf.CeilToInt(angle));
    }
    /// <summary>
    /// [扇型メッシュ]を生成して返す：void型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数指定の[扇形メッシュ]</param>
    /// <param name="angle">生成する[扇形メッシュ]の角度</param>
    /// <param name="range">生成する[扇形メッシュ]の距離</param>
    /// <param name="addvertices">生成する[扇形メッシュ]の追加する頂点の数</param>
    public static void CreateFanMesh(out Mesh mesh, float angle, float range, int addvertices)
    {
        mesh = CreateFanMesh(angle, range, addvertices);
    }
    /// <summary>
    /// [扇型メッシュ]を生成して返す：void型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数自動の[扇形メッシュ]</param>
    /// <param name="angle">生成する[扇形メッシュ]の角度</param>
    /// <param name="range">生成する[扇形メッシュ]の距離</param>
    public static void CreateFanMesh(out Mesh mesh, float angle, float range)
    {
        mesh = CreateFanMesh(angle, range);
    }

    #endregion FanMesh

    #region FanTorusMesh

    /// <summary>
    /// [扇型トーラスメッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="angle">生成する[扇形トーラスメッシュ]の角度</param>
    /// <param name="s_range">生成する[扇形トーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[扇形トーラスメッシュ]の終点距離</param>
    /// <param name="addvertices">生成する[扇形トーラスメッシュ]の追加する頂点の数</param>
    /// <returns>頂点数指定の[扇形トーラスメッシュ]</returns>
    public static Mesh CreateFanTorusMesh(float angle, float s_range, float e_range, int addvertices)
    {
        //値補正
        angle = angle <= 0 ? 180.0f : angle;
        s_range = s_range <= 0 ? 2.5f : s_range;
        e_range = e_range <= 0 ? 2.5f : e_range;
        addvertices = addvertices < 0 ? Mathf.CeilToInt(angle) : addvertices;

        //頂点　頂点インデックス定義
        int _roundvert = addvertices + 3;
        var _vertices = new Vector3[_roundvert * 2];
        var _triangles = new List<int>((addvertices + 2) * 2 * 3);

        //計算キャッシュ
        float _ang = -angle / 2.0f * DegToRad;
        float _cutAngle = angle / (float)(addvertices + 2) * DegToRad;
        float _distance = s_range + e_range;

        //頂点　頂点インデックス代入
        for(int i = 0, roundMinus1 = _roundvert - 1; i < _roundvert; i++)
        {
            float
                x = Mathf.Sin(_ang),
                y = Mathf.Cos(_ang);
            _vertices[i * 2] = new Vector3(x * s_range, 0, y * s_range);
            _vertices[i * 2 + 1] = new Vector3(x * _distance, 0, y * _distance);
            _ang += _cutAngle;
            if(i < roundMinus1)
            {
                int mul2 = i * 2;
                _triangles.AddRange(new int[3] { mul2, mul2 + 1, mul2 + 2 });
                _triangles.AddRange(new int[3] { mul2 + 3, mul2 + 2, mul2 + 1 });
            }
        }

        //メッシュ生成
        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();
        return _mesh;
    }
    /// <summary>
    /// [扇型トーラスメッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="angle">生成する[扇形トーラスメッシュ]の角度</param>
    /// <param name="s_range">生成する[扇形トーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[扇形トーラスメッシュ]の終点距離</param>
    /// <returns>頂点数自動の[扇形トーラスメッシュ]</returns>
    public static Mesh CreateFanTorusMesh(float angle, float s_range, float e_range)
    {
        return CreateFanTorusMesh(angle, s_range, e_range, Mathf.CeilToInt(angle));
    }
    /// <summary>
    /// [扇型トーラスメッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数指定の[扇形トーラスメッシュ]</param>
    /// <param name="angle">生成する[扇形トーラスメッシュ]の角度</param>
    /// <param name="s_range">生成する[扇形トーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[扇形トーラスメッシュ]の終点距離</param>
    /// <param name="addvertices">生成する[扇形トーラスメッシュ]の追加する頂点の数</param>
    public static void CreateFanTorusMesh(out Mesh mesh, float angle, float s_range, float e_range, int addvertices)
    {
        mesh = CreateFanTorusMesh(angle, s_range, e_range, addvertices);
    }
    /// <summary>
    /// [扇型トーラスメッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数自動の[扇形トーラスメッシュ]</param>
    /// <param name="angle">生成する[扇形トーラスメッシュ]の角度</param>
    /// <param name="s_range">生成する[扇形トーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[扇形トーラスメッシュ]の終点距離</param>
    public static void CreateFanTorusMesh(out Mesh mesh, float angle, float s_range, float e_range)
    {
        mesh = CreateFanTorusMesh(angle, s_range, e_range);
    }

    #endregion FanTorusMesh

    #region FanMesh3D

    /// <summary>
    /// [扇型3Dメッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="angle">生成する[扇型3Dメッシュ]の角度</param>
    /// <param name="range">生成する[扇型3Dメッシュ]の距離</param>
    /// <param name="height">生成する[扇型3Dメッシュ]の高さ</param>
    /// <param name="addvertices">生成する[扇型3Dメッシュ]の追加する頂点の数</param>
    /// <returns>頂点数指定の[扇型3Dメッシュ]</returns>
    public static Mesh CreateFan3DMesh(float angle, float range, float height, int addvertices)
    {
        angle = angle <= 0 ? 180.0f : angle;
        range = range <= 0 ? 5.0f : range;
        height = height <= 0 ? 5.0f : height;
        addvertices = addvertices < 0 ? addvertices = Mathf.CeilToInt(angle) : addvertices;

        int _roundvert = addvertices + 3;
        var _vertices = new Vector3[_roundvert * 2 + 2];
        var _triangles = new List<int>((addvertices + 2) * 12 + 12);

        float _ang = -angle / 2.0f * DegToRad;
        float _cutAngle = angle / (float)(addvertices + 2) * DegToRad;
        float _halfHeight = height / 2.0f;

        _vertices[0] = Vector3.up * _halfHeight;
        _vertices[1] = Vector3.down * _halfHeight;
        for(int i = 0, roundMinus1 = _roundvert - 1; i < _roundvert; i++)
        {
            float
                x = Mathf.Sin(_ang) * range,
                y = Mathf.Cos(_ang) * range;
            _vertices[i * 2 + 2] = new Vector3(x, _halfHeight, y);
            _vertices[i * 2 + 3] = new Vector3(x, -_halfHeight, y);
            _ang += _cutAngle;
            if(i < roundMinus1)
            {
                int mul2 = i * 2;
                _triangles.AddRange(new int[] { 0, mul2 + 2, mul2 + 4 });
                _triangles.AddRange(new int[] { 1, mul2 + 5, mul2 + 3 });
                _triangles.AddRange(new int[] { mul2 + 2, mul2 + 3, mul2 + 5 });
                _triangles.AddRange(new int[] { mul2 + 5, mul2 + 4, mul2 + 2 });
            }
        }
        _triangles.AddRange(new int[] { 0, 1, 3 });
        _triangles.AddRange(new int[] { 3, 2, 0 });
        _triangles.AddRange(new int[] { 1, 0, _vertices.Length - 2 });
        _triangles.AddRange(new int[] { _vertices.Length - 2, _vertices.Length - 1, 1 });

        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();
        return _mesh;
    }
    /// <summary>
    /// [扇型3Dメッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="angle">生成する[扇型3Dメッシュ]の角度</param>
    /// <param name="range">生成する[扇型3Dメッシュ]の距離</param>
    /// <param name="height">生成する[扇型3Dメッシュ]の高さ</param>
    /// <returns>頂点数自動の[扇型3Dメッシュ]</returns>
    public static Mesh CreateFan3DMesh(float angle, float range, float height)
    {
        return CreateFan3DMesh(angle, range, height, Mathf.CeilToInt(angle));
    }
    /// <summary>
    /// [扇型3Dメッシュ]を生成して返す：void型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数指定の[扇型3Dメッシュ]</param>
    /// <param name="angle">生成する[扇型3Dメッシュ]の角度</param>
    /// <param name="range">生成する[扇型3Dメッシュ]の距離</param>
    /// <param name="height">生成する[扇型3Dメッシュ]の高さ</param>
    /// <param name="addvertices">生成する[扇型3Dメッシュ]の追加する頂点の数</param>
    public static void CreateFan3DMesh(out Mesh mesh, float angle, float range, float height, int addvertices)
    {
        mesh = CreateFan3DMesh(angle, range, height, addvertices);
    }
    /// <summary>
    /// [扇型3Dメッシュ]を生成して返す：void型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数手動の[扇型3Dメッシュ]</param>
    /// <param name="angle">生成する[扇型3Dメッシュ]の角度</param>
    /// <param name="range">生成する[扇型3Dメッシュ]の距離</param>
    /// <param name="height">生成する[扇型3Dメッシュ]の高さ</param>
    public static void CreateFan3DMesh(out Mesh mesh, float angle, float range, float height)
    {
        mesh = CreateFan3DMesh(angle, range, height);
    }

    #endregion FanMesh3D

    #region FanTorusMesh3D

    /// <summary>
    /// [扇型3Dトーラスメッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="angle">生成する[扇型3Dトーラスメッシュ]の角度</param>
    /// <param name="s_range">生成する[扇型3Dトーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[扇型3Dトーラスメッシュ]の終点距離</param>
    /// <param name="height">生成する[扇型3Dトーラスメッシュ]の高さ</param>
    /// <param name="addvertices">生成する[扇型3Dトーラスメッシュ]の追加する頂点の数</param>
    /// <returns>頂点数指定の[扇型3Dトーラスメッシュ]</returns>
    public static Mesh CreateFanTorus3DMesh(float angle, float s_range, float e_range, float height, int addvertices)
    {
        angle = angle <= 0 ? 5.0f : angle;
        s_range = s_range <= 0 ? 2.5f : s_range;
        e_range = e_range <= 0 ? 2.5f : e_range;
        addvertices = addvertices < 0 ? addvertices = Mathf.CeilToInt(angle) : addvertices;

        int _roundvert = addvertices + 3;
        var _vertices = new Vector3[_roundvert * 4];
        var _triangles = new List<int>((addvertices + 2) * 24 + 12);

        float _ang = -angle / 2.0f * DegToRad;
        float _cutAngle = angle / (float)(addvertices + 2) * DegToRad;
        float _distance = s_range + e_range;
        float _halfHeight = height / 2.0f;

        for(int i = 0, roundMinus1 = _roundvert - 1; i < _roundvert; i++)
        {
            int mul4 = i * 4;
            float
                x = Mathf.Sin(_ang),
                y = Mathf.Cos(_ang);
            _vertices[mul4] = new Vector3(x * s_range, _halfHeight, y * s_range);
            _vertices[mul4 + 1] = new Vector3(x * s_range, -_halfHeight, y * s_range);
            _vertices[mul4 + 2] = new Vector3(x * _distance, _halfHeight, y * _distance);
            _vertices[mul4 + 3] = new Vector3(x * _distance, -_halfHeight, y * _distance);
            _ang += _cutAngle;
            if(i < roundMinus1)
            {
                int mul4odd = i * 4 + 1;
                _triangles.AddRange(new int[] { mul4, mul4 + 2, mul4 + 6 });
                _triangles.AddRange(new int[] { mul4 + 6, mul4 + 4, mul4 });
                _triangles.AddRange(new int[] { mul4odd + 2, mul4odd, mul4odd + 4 });
                _triangles.AddRange(new int[] { mul4odd + 4, mul4odd + 6, mul4odd + 2 });
                _triangles.AddRange(new int[] { mul4 + 2, mul4odd + 2, mul4odd + 6 });
                _triangles.AddRange(new int[] { mul4odd + 6, mul4 + 6, mul4 + 2 });
                _triangles.AddRange(new int[] { mul4 + 4, mul4odd + 4, mul4odd });
                _triangles.AddRange(new int[] { mul4odd, mul4, mul4 + 4 });
            }
        }
        _triangles.AddRange(new int[] { 0, 1, 3 });
        _triangles.AddRange(new int[] { 3, 2, 0 });
        _triangles.AddRange(new int[] { _vertices.Length - 3, _vertices.Length - 4, _vertices.Length - 2 });
        _triangles.AddRange(new int[] { _vertices.Length - 2, _vertices.Length - 1, _vertices.Length - 3 });

        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();
        return _mesh;
    }
    /// <summary>
    /// [扇型3Dトーラスメッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="angle">生成する[扇型3Dトーラスメッシュ]の角度</param>
    /// <param name="s_range">生成する[扇型3Dトーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[扇型3Dトーラスメッシュ]の終点距離</param>
    /// <param name="height">生成する[扇型3Dトーラスメッシュ]の高さ</param>
    /// <returns>頂点数自動の[扇型3Dトーラスメッシュ]</returns>
    public static Mesh CreateFanTorus3DMesh(float angle, float s_range, float e_range, float height)
    {
        return CreateFanTorus3DMesh(angle, s_range, e_range, height, Mathf.CeilToInt(angle));
    }
    /// <summary>
    /// [扇型3Dトーラスメッシュ]を生成して返す：void型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数指定の[扇型3Dトーラスメッシュ]</param>
    /// <param name="angle">生成する[扇型3Dトーラスメッシュ]の角度</param>
    /// <param name="s_range">生成する[扇型3Dトーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[扇型3Dトーラスメッシュ]の終点距離</param>
    /// <param name="height">生成する[扇型3Dトーラスメッシュ]の高さ</param>
    /// <param name="addvertices">生成する[扇型3Dトーラスメッシュ]の追加する頂点の数</param>
    public static void CreateFanTorus3DMesh(out Mesh mesh, float angle, float s_range, float e_range, float height, int addvertices)
    {
        mesh = CreateFanTorus3DMesh(angle, s_range, e_range, height, addvertices);
    }
    /// <summary>
    /// [扇型3Dトーラスメッシュ]を生成して返す：void型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数自動の[扇型3Dトーラスメッシュ]</param>
    /// <param name="angle">生成する[扇型3Dトーラスメッシュ]の角度</param>
    /// <param name="s_range">生成する[扇型3Dトーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[扇型3Dトーラスメッシュ]の終点距離</param>
    /// <param name="height">生成する[扇型3Dトーラスメッシュ]の高さ</param>
    public static void CreateFanTorus3DMesh(out Mesh mesh, float angle, float s_range, float e_range, float height)
    {
        mesh = CreateFanTorus3DMesh(angle, s_range, e_range, height);
    }

    #endregion FanTorusMesh3D

    #endregion Fan

    #region Special

    #region Sphere

    #region HemiShere

    /// <summary>
    /// [球型扇半球メッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="angle">生成する[球型扇半球メッシュ]の角度</param>
    /// <param name="range">生成する[球型扇半球メッシュ]の距離</param>
    /// <param name="width_addvertices">生成する[球型扇半球メッシュ]の追加する水平方向の頂点の数</param>
    /// <param name="height_addvertices">生成する[球型扇半球メッシュ]の追加する垂直方向の頂点の数</param>
    /// <returns>頂点数指定の[球型扇半球メッシュ]</returns>
    public static Mesh CreateFanHemiSphereMesh(float angle, float range, int width_addvertices, int height_addvertices)
    {
        //値補正
        angle = angle <= 0 ? 180.0f : angle;
        range = range <= 0 ? 5.0f : range;
        width_addvertices = width_addvertices < 0 ? 180 : width_addvertices;
        height_addvertices = height_addvertices < 0 ? 90 : height_addvertices;

        //頂点　頂点インデックス定義
        var _vertices = new Vector3[(width_addvertices + 3) * (height_addvertices + 1) + 3];
        var _triangles = new List<int>(
            ((height_addvertices * 2) + 4) * 3//側面
            + (height_addvertices + 1) * 4//縦
            * (width_addvertices + 2));

        //計算キャッシュ
        float
            _w_angle = -angle / 2f * DegToRad,
            _cut_w_ang = angle / (float)(width_addvertices + 2) * DegToRad,
            _cut_h_ang = OnePi / (float)(height_addvertices + 2);

        //頂点　頂点インデックス代入
        _vertices[0] = Vector3.zero;
        _vertices[1] = Vector3.up * range;
        _vertices[2] = Vector3.down * range;
        for(int i = 0; i < width_addvertices + 3; i++)
        {
            float
                fix_2D_x = Mathf.Sin(_w_angle) * range,
                fix_2D_y = Mathf.Cos(_w_angle) * range;

            float h_ang_save = TwoDePi - _cut_h_ang;
            for(int j = 0; j < height_addvertices + 1; j++)
            {
                float
                    x = Mathf.Cos(h_ang_save) * fix_2D_x,
                    y = Mathf.Sin(h_ang_save) * range,
                    z = Mathf.Cos(h_ang_save) * fix_2D_y;
                _vertices[3 + i * (height_addvertices + 1) + j] = new Vector3(x, y, z);
                h_ang_save -= _cut_h_ang;
            }
            _w_angle += _cut_w_ang;
        }
        //側面
        int _r_start_width_vert = _vertices.Length - 1 - height_addvertices;
        _triangles.AddRange(new int[3] { 0, 3, 1 });
        _triangles.AddRange(new int[3] { 0, 2, height_addvertices + 3 });
        _triangles.AddRange(new int[3] { 0, 1, _r_start_width_vert });
        _triangles.AddRange(new int[3] { 0, _vertices.Length - 1, 2 });
        for(int i = 0; i < height_addvertices; i++)
        {
            _triangles.AddRange(new int[3] { 0, i + 4, i + 3 });//左
            _triangles.AddRange(new int[3] { 0, _r_start_width_vert + i, _r_start_width_vert + i + 1 });//右
        }
        //底面 <外面>
        for(int i = 0; i < width_addvertices + 2; i++)
        {
            _triangles.AddRange(new int[3] {
                1,
                3 + i * (height_addvertices + 1),
                3 + (height_addvertices + 1) + i * (height_addvertices + 1)
            });//上
            //外面
            int i_vert_offset = i * (height_addvertices + 1);
            for(int j = 0; j < height_addvertices; j++)
            {
                int j_v_offset = j + i_vert_offset;
                _triangles.AddRange(new int[3] {
                    3 + j_v_offset,
                    4 + j_v_offset,
                    4 + height_addvertices +  j_v_offset });
                _triangles.AddRange(new int[3] {
                    5 + height_addvertices + j_v_offset,
                    4 + height_addvertices + j_v_offset,
                    4 + j_v_offset });
            }
            _triangles.AddRange(new int[3] {
                2,
                3 + height_addvertices + (height_addvertices + 1) + i * (height_addvertices + 1),
                3 + height_addvertices + i * (height_addvertices + 1)
            });//下
        }

        //メッシュ作成
        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();

        return _mesh;
    }
    /// <summary>
    /// [球型扇半球メッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="angle">生成する[球型扇半球メッシュ]の角度</param>
    /// <param name="range">生成する[球型扇半球メッシュ]の距離</param>
    /// <returns>頂点数自動の[球型扇半球メッシュ]</returns>
    public static Mesh CreateFanHemiSphereMesh(float angle, float range)
    {
        return CreateFanHemiSphereMesh(angle, range, Mathf.CeilToInt(angle), 90);
    }
    /// <summary>
    /// [球型扇半球メッシュ]を生成して返す：void型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数指定の[球型扇半球メッシュ]</param>
    /// <param name="angle">生成する[球型扇半球メッシュ]の角度</param>
    /// <param name="range">生成する[球型扇半球メッシュ]の距離</param>
    /// <param name="width_addvertices">生成する[球型扇半球メッシュ]の追加する水平方向の頂点の数</param>
    /// <param name="height_addvertices">生成する[球型扇半球メッシュ]の追加する垂直方向の頂点の数</param>
    public static void CreateFanHemiSphereMesh(out Mesh mesh, float angle, float range, int width_addvertices, int height_addvertices)
    {
        mesh = CreateFanHemiSphereMesh(angle, range, width_addvertices, height_addvertices);
    }
    /// <summary>
    /// [球型扇半球メッシュ]を生成して返す：void型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数自動の[球型扇半球メッシュ]</param>
    /// <param name="angle">生成する[球型扇半球メッシュ]の角度</param>
    /// <param name="range">生成する[球型扇半球メッシュ]の距離</param>
    public static void CreateFanHemiSphereMesh(out Mesh mesh, float angle, float range)
    {
        mesh = CreateFanHemiSphereMesh(angle, range);
    }

    #endregion HemiSphere

    #region SideSphere

    /// <summary>
    /// [球型横扇メッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="angle">生成する[球型横扇メッシュ]の角度</param>
    /// <param name="range">生成する[球型横扇メッシュ]の距離</param>
    /// <param name="width_addvertices">生成する[球型横扇メッシュ]の追加する水平方向の頂点の数</param>
    /// <param name="height_addvertices">生成する[球型横扇メッシュ]の追加する垂直方向の頂点の数</param>
    /// <returns>頂点数指定の[球型横扇メッシュ]</returns>
    public static Mesh CreateFanSideSphereMesh(float angle, float range, int width_addvertices, int height_addvertices)
    {
        int _h_vert = height_addvertices + 2;
        var _vertices = new Vector3[(width_addvertices + 3) * _h_vert + 1];
        var _triangles = new List<int>((width_addvertices + 3) * (height_addvertices + 1) * 6 + (width_addvertices + 3) * 6);

        //計算キャッシュ
        float
            _w_angle = -OnePi,
            _h_angle = angle / 2f * DegToRad,
            _cut_w_ang = TwoPi / (float)(width_addvertices + 3),
            _cut_h_ang = angle / (float)(height_addvertices + 1) * DegToRad;

        //頂点　頂点インデックス代入
        _vertices[0] = Vector3.zero;
        for(int i = 0; i < width_addvertices + 3; i++)
        {
            float
                fix_2D_x = Mathf.Sin(_w_angle),
                fix_2D_y = Mathf.Cos(_w_angle);

            float h_ang_save = _h_angle;
            for(int j = 0; j < height_addvertices + 2; j++)
            {
                float
                    x = Mathf.Cos(h_ang_save) * range * fix_2D_x,
                    y = Mathf.Sin(h_ang_save) * range,
                    z = Mathf.Cos(h_ang_save) * range * fix_2D_y;
                _vertices[1 + i * _h_vert + j] = new Vector3(x, y, z);
                h_ang_save -= _cut_h_ang;
            }
            _w_angle += _cut_w_ang;
        }
        //底面 <外面>
        for(int i = 0; i < width_addvertices + 3; i++)
        {
            //上
            _triangles.AddRange(new int[3] { 0, 1 + i * _h_vert, (1 + _h_vert + i * _h_vert) % (_vertices.Length - 1) });

            //外面
            int i_vert_offset = i * _h_vert;
            for(int j = 0; j < height_addvertices + 1; j++)
            {
                int j_v_offset = j + i_vert_offset;
                _triangles.AddRange(new int[3] {
                    1 + j_v_offset,
                    2 + j_v_offset,
                    (3 + height_addvertices + j_v_offset) % (_vertices.Length - 1) });

                _triangles.AddRange(new int[3] {
                    (3 + height_addvertices + j_v_offset) % (_vertices.Length - 1) + 1,
                    (3 + height_addvertices + j_v_offset) % (_vertices.Length - 1),
                    2 + j_v_offset });
            }

            //下
            _triangles.AddRange(new int[3] { 0, (_h_vert + i * _h_vert) % (_vertices.Length - 1) + _h_vert, _h_vert + i * _h_vert });
        }

        //メッシュ作成
        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();

        return _mesh;
    }
    /// <summary>
    /// [球型横扇メッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="angle">生成する[球型横扇メッシュ]の角度</param>
    /// <param name="range">生成する[球型横扇メッシュ]の距離</param>
    /// <returns>頂点数自動の[球型横扇メッシュ]</returns>
    public static Mesh CreateFanSideSphereMesh(float angle, float range)
    {
        return CreateFanSideSphereMesh(angle, range, 180, Mathf.CeilToInt(angle));
    }
    /// <summary>
    /// [球型横扇メッシュ]を生成して返す：void型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数指定の[球型横扇メッシュ]</param>
    /// <param name="angle">生成する[球型横扇メッシュ]の角度</param>
    /// <param name="range">生成する[球型横扇メッシュ]の距離</param>
    /// <param name="width_addvertices">生成する[球型横扇メッシュ]の追加する水平方向の頂点の数</param>
    /// <param name="height_addvertices">生成する[球型横扇メッシュ]の追加する垂直方向の頂点の数</param>
    public static void CreateFanSideSphereMesh(out Mesh mesh, float angle, float range, int width_addvertices, int height_addvertices)
    {
        mesh = CreateFanSideSphereMesh(angle, range, width_addvertices, height_addvertices);
    }
    /// <summary>
    /// [球型横扇メッシュ]を生成して返す：void型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数自動の[球型横扇メッシュ]</param>
    /// <param name="angle">生成する[球型横扇メッシュ]の角度</param>
    /// <param name="range">生成する[球型横扇メッシュ]の距離</param>
    public static void CreateFanSideSphereMesh(out Mesh mesh, float angle, float range)
    {
        mesh = CreateFanSideSphereMesh(angle, range);
    }

    #endregion SideSphere

    #region PartsOfShere

    /// <summary>
    /// [球型欠片メッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="width_angle">生成する[球型欠片メッシュ]の横角度</param>
    /// <param name="height_angle">生成する[球型欠片メッシュ]の縦角度</param>
    /// <param name="range">生成する[球型欠片メッシュ]の距離</param>
    /// <param name="width_addvertices">生成する[球型欠片メッシュ]の追加する水平方向の頂点の数</param>
    /// <param name="height_addvertices">生成する[球型欠片メッシュ]の追加する垂直方向の頂点の数</param>
    /// <returns>頂点数指定の[球型欠片メッシュ]</returns>
    public static Mesh CreateFanPartsOfSphereMesh(float width_angle, float height_angle, float range, int width_addvertices, int height_addvertices)
    {
        //値補正
        width_angle = width_angle <= 0 ? 180.0f : width_angle;
        height_angle = height_angle <= 0 ? 90.0f : height_angle;
        range = range <= 0 ? 5.0f : range;
        width_addvertices = width_addvertices < 0 ? 180 : width_addvertices;
        height_addvertices = height_addvertices < 0 ? 90 : height_addvertices;

        //頂点　頂点インデックス定義
        int _w_vert = width_addvertices + 2,
            _h_vert = height_addvertices + 2;
        var _vertices = new Vector3[(_w_vert + 1) * _h_vert + 1];
        var _triangles = new List<int>(_w_vert * 6 * (height_addvertices + 1) + (height_addvertices + 1) * 3 + (_w_vert * (height_addvertices + 1) * 6));

        //計算キャッシュ
        float
            _w_angle = -width_angle / 2f * DegToRad,
            _h_angle = height_angle / 2f * DegToRad,
            _cut_w_ang = width_angle / (float)(_w_vert) * DegToRad,
            _cut_h_ang = height_angle / (float)(height_addvertices + 1) * DegToRad;

        //頂点　頂点インデックス代入
        _vertices[0] = Vector3.zero;
        for(int i = 0; i < width_addvertices + 3; i++)
        {
            float
                fix_2D_x = Mathf.Sin(_w_angle),
                fix_2D_y = Mathf.Cos(_w_angle);

            float h_ang_save = _h_angle;
            for(int j = 0; j < _h_vert; j++)
            {
                float
                    x = Mathf.Cos(h_ang_save) * range * fix_2D_x,
                    y = Mathf.Sin(h_ang_save) * range,
                    z = Mathf.Cos(h_ang_save) * range * fix_2D_y;
                _vertices[1 + i * _h_vert + j] = new Vector3(x, y, z);
                h_ang_save -= _cut_h_ang;
            }
            _w_angle += _cut_w_ang;
        }
        //側面
        int _r_start_width_vert = _vertices.Length - 1 - _h_vert;
        for(int i = 0; i < height_addvertices + 1; i++)
        {
            //左
            _triangles.AddRange(new int[3] { 0, i + 2, i + 1 });
            //右
            _triangles.AddRange(new int[3] { 0, _r_start_width_vert + i + 1, _r_start_width_vert + i + 2 });
        }
        //底面 <外面>
        for(int i = 0; i < _w_vert; i++)
        {
            //上
            _triangles.AddRange(new int[3] { 0, 1 + i * _h_vert, 1 + _h_vert + i * _h_vert });
            //下
            _triangles.AddRange(new int[3] { 0, _h_vert * 2 + i * _h_vert, _h_vert + i * _h_vert });
            //外面
            int i_vert_offset = i * _h_vert;
            for(int j = 0; j < height_addvertices + 1; j++)
            {
                int j_v_offset = j + i_vert_offset;
                _triangles.AddRange(new int[3] {
                    1 + j_v_offset,
                    2 + j_v_offset,
                    3 + height_addvertices + j_v_offset });
                _triangles.AddRange(new int[3] {
                    4 + height_addvertices + j_v_offset,
                    3 + height_addvertices + j_v_offset,
                    2 + j_v_offset });
            }
        }

        //メッシュ作成
        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();

        return _mesh;
    }
    /// <summary>
    /// [球型欠片メッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="width_angle">生成する[球型欠片メッシュ]の横角度</param>
    /// <param name="height_angle">生成する[球型欠片メッシュ]の縦角度</param>
    /// <param name="range">生成する[球型欠片メッシュ]の距離<</param>
    /// <returns>頂点数自動の[球型欠片メッシュ]</returns>
    public static Mesh CreateFanPartsOfSphereMesh(float width_angle, float height_angle, float range)
    {
        return CreateFanPartsOfSphereMesh(width_angle, height_angle, range, Mathf.CeilToInt(width_angle), Mathf.CeilToInt(height_angle));
    }
    /// <summary>
    /// [球型欠片メッシュ]を生成して返す：void型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数指定の[球型欠片メッシュ]</param>
    /// <param name="width_angle">生成する[球型欠片メッシュ]の横角度</param>
    /// <param name="height_angle">生成する[球型欠片メッシュ]の縦角度</param>
    /// <param name="range">生成する[球型欠片メッシュ]の距離</param>
    /// <param name="width_addvertices">生成する[球型欠片メッシュ]の追加する水平方向の頂点の数</param>
    /// <param name="height_addvertices">生成する[球型欠片メッシュ]の追加する垂直方向の頂点の数</param>
    public static void CreateFanPartsOfSphereMesh(out Mesh mesh, float width_angle, float height_angle, float range, int width_addvertices, int height_addvertices)
    {
        mesh = CreateFanPartsOfSphereMesh(width_angle, height_angle, range, width_addvertices, height_addvertices);
    }
    /// <summary>
    /// [球型欠片メッシュ]を生成して返す：void型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数自動の[球型欠片メッシュ]</param>
    /// <param name="width_angle">生成する[球型欠片メッシュ]の横角度</param>
    /// <param name="height_angle">生成する[球型欠片メッシュ]の縦角度</param>
    /// <param name="range">生成する[球型欠片メッシュ]の距離<</param>
    public static void CreateFanPartsOfSphereMesh(out Mesh mesh, float width_angle, float height_angle, float range)
    {
        mesh = CreateFanPartsOfSphereMesh(width_angle, height_angle, range);
    }

    #endregion PartsOfShere

    #endregion Sphere

    #region Moon

    /// <summary>
    /// [月形メッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="angle">生成する[月形メッシュ]の角度</param>
    /// <param name="base_range">生成する[月形メッシュ]の土台距離</param>
    /// <param name="hole_range">生成する[月形メッシュ]の切削距離</param>
    /// <param name="addvertices">生成する[月形メッシュ]の頂点の数</param>
    /// <returns>頂点数指定の[月形メッシュ]</returns>
    public static Mesh CreateMoonMesh(float angle, float base_range, float hole_range, int addvertices)
    {
        //値補正
        angle = angle <= 0 ? 180.0f : angle;
        base_range = base_range <= 0 ? 2.5f : base_range;
        hole_range = hole_range <= 0 ? 2.5f : hole_range;
        addvertices = addvertices < 0 ? 180 : addvertices;

        //頂点　頂点インデックス定義
        var _vertices = new Vector3[addvertices * 2 + 4];
        var _triangles = new List<int>(addvertices * 6 + 6);

        //計算キャッシュ
        float
            _ang = -angle / 2.0f,
            _cutangle = angle / (float)(addvertices + 2);
        float
            x = Mathf.Sin(_ang * DegToRad) * base_range,
            y = Mathf.Cos(_ang * DegToRad) * base_range,
            hole_pos = base_range - hole_range;

        //頂点　頂点インデックス代入
        _vertices[0] = new Vector3(x, 0, y);
        _ang += _cutangle;
        for(int i = 0; i < addvertices + 1; i++)
        {
            int mul2 = i * 2;
            x = Mathf.Sin(_ang * DegToRad);
            y = Mathf.Cos(_ang * DegToRad);
            _vertices[mul2 + 1] = new Vector3(x * hole_range, 0, y * hole_range - hole_pos);
            _vertices[mul2 + 2] = new Vector3(x * base_range, 0, y * base_range);
            _ang += _cutangle;
        }
        x = Mathf.Sin(angle / 2.0f * DegToRad) * base_range;
        y = Mathf.Cos(angle / 2.0f * DegToRad) * base_range;
        _vertices[_vertices.Length - 1] = new Vector3(x, 0, y);//最後の頂点
        for (int i = 0; i < addvertices; i++)
        {
            int mul2 = i * 2;
            _triangles.AddRange(new int[3] { mul2 + 1, mul2 + 2, mul2 + 3 });
            _triangles.AddRange(new int[3] { mul2 + 4, mul2 + 3, mul2 + 2 });
        }
        _triangles.AddRange(new int[3] { 0, 2, 1 });
        _triangles.AddRange(new int[3] { _vertices.Length - 3, _vertices.Length - 2, _vertices.Length - 1 });

        //メッシュ生成
        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();

        return _mesh;
    }
    /// <summary>
    /// [月形メッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="angle">生成する[月形メッシュ]の角度</param>
    /// <param name="base_range">生成する[月形メッシュ]の土台距離</param>
    /// <param name="hole_range">生成する[月形メッシュ]の切削距離</param>
    /// <returns>頂点数自動の[月形メッシュ]</returns>
    public static Mesh CreateMoonMesh(float angle, float base_range, float hole_range)
    {
        return CreateMoonMesh(angle, base_range, hole_range, Mathf.CeilToInt(angle));
    }
    /// <summary>
    /// [月形メッシュ]を生成して返す：void型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数指定の[月形メッシュ]</param>
    /// <param name="angle">生成する[月形メッシュ]の角度</param>
    /// <param name="base_range">生成する[月形メッシュ]の土台距離</param>
    /// <param name="hole_range">生成する[月形メッシュ]の切削距離</param>
    /// <param name="addvertices">生成する[月形メッシュ]の頂点の数</param>
    public static void CreateMoonMesh(out Mesh mesh, float angle, float base_range, float hole_range, int addvertices)
    {
        mesh = CreateMoonMesh(angle, base_range, hole_range, addvertices);
    }
    /// <summary>
    /// [月形メッシュ]を生成して返す：void型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数自動の[月形メッシュ]</param>
    /// <param name="angle">生成する[月形メッシュ]の角度</param>
    /// <param name="base_range">生成する[月形メッシュ]の土台距離</param>
    /// <param name="hole_range">生成する[月形メッシュ]の切削距離</param>
    public static void CreateMoonMesh(out Mesh mesh, float angle, float base_range, float hole_range)
    {
        mesh = CreateMoonMesh(angle, base_range, hole_range);
    }

    #endregion Moon

    #region Star

    #region Star2D

    /// <summary>
    /// [星形メッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="odd_range">生成する[星形メッシュ]の奇数頂点の距離</param>
    /// <param name="even_range">生成する[星形メッシュ]の偶数頂点の距離</param>
    /// <param name="addvertices">生成する[星形メッシュ]の追加する頂点の数</param>
    /// <returns>頂点数指定の[星形メッシュ]</returns>
    public static Mesh CreateStarMesh(float odd_range, float even_range, int addvertices)
    {
        //値補正
        odd_range = odd_range <= 0 ? 5.0f : odd_range;
        even_range = even_range <= 0 ? 2.5f : even_range;
        addvertices = addvertices < 0 ? 3 : addvertices;

        //頂点　頂点インデックス定義
        var _vertices = new Vector3[(addvertices + 2) * 2 + 1];
        var _triangles = new List<int>((addvertices + 2) * 6);

        //計算キャッシュ
        float
            _outer_angle = TwoPi / (float)(addvertices + 2),
            _inner_angle = _outer_angle / 2.0f + _outer_angle,
            _cutangle = _outer_angle;

        //頂点　頂点インデックス代入
        _vertices[0] = Vector3.zero;
        for(int i = 0; i < addvertices + 2; i++)
        {
            int mul2 = i * 2;
            float
                x = Mathf.Sin(_outer_angle),
                y = Mathf.Cos(_outer_angle);
            _vertices[mul2 + 1] = new Vector3(x * odd_range, 0, y * odd_range);

            x = Mathf.Sin(_inner_angle);
            y = Mathf.Cos(_inner_angle);
            _vertices[mul2 + 2] = new Vector3(x * even_range, 0, y * even_range);

            _outer_angle += _cutangle;
            _inner_angle += _cutangle;
        }
        for(int i = 1; i <= addvertices + 1; i++)
        {
            int mul2 = i * 2;
            _triangles.AddRange(new int[3] { 0, mul2, mul2 + 1 });
            _triangles.AddRange(new int[3] { 0, mul2 + 1, mul2 + 2 });
        }
        _triangles.AddRange(new int[3] { 0, 1, 2 });
        _triangles.AddRange(new int[3] { 0, _vertices.Length - 1, 1 });

        //メッシュ生成
        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();

        return _mesh;
    }
    /// <summary>
    /// [星形メッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="odd_range">生成する[星形メッシュ]の奇数頂点の距離</param>
    /// <param name="even_range">生成する[星形メッシュ]の偶数頂点の距離</param>
    /// <returns>頂点数自動の[星形メッシュ]</returns>
    public static Mesh CreateStarMesh(float odd_range, float even_range)
    {
        return CreateStarMesh(odd_range, even_range, 3);
    }
    /// <summary>
    /// [星形メッシュ]を生成して返す：void型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数指定の[星形メッシュ]</param>
    /// <param name="odd_range">生成する[星形メッシュ]の奇数頂点の距離</param>
    /// <param name="even_range">生成する[星形メッシュ]の偶数頂点の距離</param>
    /// <param name="addvertices">生成する[星形メッシュ]の追加する頂点の数</param>
    public static void CreateStarMesh(out Mesh mesh, float odd_range, float even_range, int addvertices)
    {
        mesh = CreateStarMesh(odd_range, even_range, addvertices);
    }
    /// <summary>
    /// [星形メッシュ]を生成して返す：void型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数自動の[星形メッシュ]</param>
    /// <param name="odd_range">生成する[星形メッシュ]の奇数頂点の距離</param>
    /// <param name="even_range">生成する[星形メッシュ]の偶数頂点の距離</param>
    public static void CreateStarMesh(out Mesh mesh, float odd_range, float even_range)
    {
        mesh = CreateStarMesh(odd_range, even_range);
    }

    #endregion Star2D

    #region Star3D

    /// <summary>
    /// [星形3Dメッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="odd_range">生成する[星形3Dメッシュ]の奇数頂点の距離</param>
    /// <param name="even_range">生成する[星形3Dメッシュ]の偶数頂点の距離</param>
    /// <param name="height">生成する[星形3Dメッシュ]の高さ</param>
    /// <param name="addvertices">生成する[星形3Dメッシュ]の追加する頂点の数</param>
    /// <returns>頂点数指定の[星形3Dメッシュ]</returns>
    public static Mesh CreateStar3DMesh(float odd_range, float even_range, float height, int addvertices)
    {
        //値補正
        odd_range = odd_range <= 0 ? 5.0f : odd_range;
        even_range = even_range <= 0 ? 2.5f : even_range;
        height = height <= 0 ? 2.5f : height;
        addvertices = addvertices < 0 ? 3 : addvertices;

        //頂点　頂点インデックス定義
        var _vertices = new Vector3[(addvertices + 2) * 2 + 2];
        var _triangles = new List<int>((addvertices + 2) * 12);

        //計算キャッシュ
        float
            _outer_angle = TwoPi / (float)(addvertices + 2),
            _inner_angle = _outer_angle / 2.0f + _outer_angle,
            _cutangle = _outer_angle;

        //頂点　頂点インデックス代入
        _vertices[0] = Vector3.up * (height / 2.0f);
        _vertices[1] = Vector3.down * (height / 2.0f);
        for(int i = 0; i < addvertices + 2; i++)
        {
            int mul2 = i * 2;
            float
                x = Mathf.Sin(_outer_angle),
                y = Mathf.Cos(_outer_angle);
            _vertices[mul2 + 2] = new Vector3(x * odd_range, 0, y * odd_range);

            x = Mathf.Sin(_inner_angle);
            y = Mathf.Cos(_inner_angle);
            _vertices[mul2 + 3] = new Vector3(x * even_range, 0, y * even_range);

            _outer_angle += _cutangle;
            _inner_angle += _cutangle;
        }
        for(int i = 1; i <= addvertices + 1; i++)
        {
            int mul2Plus1 = i * 2 + 1;
            _triangles.AddRange(new int[3] { 0, mul2Plus1, mul2Plus1 + 1 });
            _triangles.AddRange(new int[3] { 0, mul2Plus1 + 1, mul2Plus1 + 2 });
            _triangles.AddRange(new int[3] { 1, mul2Plus1 + 1, mul2Plus1 });
            _triangles.AddRange(new int[3] { 1, mul2Plus1 + 2, mul2Plus1 + 1 });
        }
        _triangles.AddRange(new int[3] { 0, 2, 3 });
        _triangles.AddRange(new int[3] { 0, _vertices.Length - 1, 2 });
        _triangles.AddRange(new int[3] { 1, 3, 2 });
        _triangles.AddRange(new int[3] { 1, 2, _vertices.Length - 1 });

        //メッシュ生成
        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();

        return _mesh;
    }
    /// <summary>
    /// [星形3Dメッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="odd_range">生成する[星形3Dメッシュ]の奇数頂点の距離</param>
    /// <param name="even_range">生成する[星形3Dメッシュ]の偶数頂点の距離</param>
    /// <param name="height">生成する[星形3Dメッシュ]の高さ</param>
    /// <returns>頂点数自動の[星形3Dメッシュ]</returns>
    public static Mesh CreateStar3DMesh(float odd_range, float even_range, float height)
    {
        return CreateStar3DMesh(odd_range, even_range, height, 3);
    }
    /// <summary>
    /// [星形3Dメッシュ]を生成して返す：void型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数指定の[星形3Dメッシュ]</param>
    /// <param name="odd_range">生成する[星形3Dメッシュ]の奇数頂点の距離</param>
    /// <param name="even_range">生成する[星形3Dメッシュ]の偶数頂点の距離</param>
    /// <param name="height">生成する[星形3Dメッシュ]の高さ</param>
    /// <param name="addvertices">生成する[星形3Dメッシュ]の追加する頂点の数</param>
    public static void CreateStar3DMesh(out Mesh mesh, float odd_range, float even_range, float height, int addvertices)
    {
        mesh = CreateStar3DMesh(odd_range, even_range, height, addvertices);
    }
    /// <summary>
    /// [星形3Dメッシュ]を生成して返す：void型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数自動の[星形3Dメッシュ]</param>
    /// <param name="odd_range">生成する[星形3Dメッシュ]の奇数頂点の距離</param>
    /// <param name="even_range">生成する[星形3Dメッシュ]の偶数頂点の距離</param>
    /// <param name="height">生成する[星形3Dメッシュ]の高さ</param>
    public static void CreateStar3DMesh(out Mesh mesh, float odd_range, float even_range, float height)
    {
        mesh = CreateStar3DMesh(odd_range, even_range, height);
    }

    #endregion Star3D

    #endregion Star

    #region Torus

    /// <summary>
    /// [トーラスメッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="s_range">生成する[トーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[トーラスメッシュ]の終点距離</param>
    /// <param name="width_addvertices">生成する[トーラスメッシュ]の追加する水平方向の頂点の数</param>
    /// <param name="height_addvertices">生成する[トーラスメッシュ]の追加する垂直方向の頂点の数</param>
    /// <returns>頂点数指定の[トーラスメッシュ]</returns>
    public static Mesh CreateTorusMesh(float s_range, float e_range, int width_addvertices, int height_addvertices)
    {
        //値補正
        s_range = s_range <= 0 ? 2.5f : s_range;
        e_range = e_range <= 0 ? 2.5f : e_range;
        width_addvertices = width_addvertices < 0 ? 180 : width_addvertices;
        height_addvertices = height_addvertices < 0 ? 180 : height_addvertices;

        //頂点　頂点インデックス定義
        int
            _w_vert = width_addvertices + 3,
            _h_vert = height_addvertices + 3;

        Vector3[] _vertices = new Vector3[_w_vert * _h_vert];
        List<int> _triangles = new List<int>(_vertices.Length * 6);

        //計算キャッシュ
        float
            _cut_w_angle = 360 / (float)_w_vert * Mathf.Deg2Rad,
            _cut_h_angle = 360 / (float)_h_vert * Mathf.Deg2Rad,
            _thickness = e_range / 2.0f,
            _range = s_range + _thickness;

        //頂点　頂点インデックス代入
        for(int i = 0; i < _w_vert; i++)
        {
            int
                s_vert = _h_vert * i,
                e_vert = _h_vert * (i + 1) % _vertices.Length;
            float
                i_angle = i * _cut_w_angle,
                Sin_i = Mathf.Sin(i_angle),
                Cos_i = Mathf.Cos(i_angle);

            for(int j = 0; j < _h_vert; j++)
            {
                float
                    j_angle = j * _cut_h_angle;
                float
                //                          ↓の_x と_z のサインコサインを逆にしたらすごくなる
                x = Mathf.Sin(j_angle) * Sin_i * _thickness + Sin_i * _range,
                y = Mathf.Cos(j_angle) * _thickness,
                z = Mathf.Sin(j_angle) * Cos_i * _thickness + Cos_i * _range;

                _vertices[i * _h_vert + j] = new Vector3(x, y, z);

                int devide_vert = (j + 1) % _h_vert;
                _triangles.AddRange(new int[3] { s_vert + j, s_vert + devide_vert, e_vert + j });
                _triangles.AddRange(new int[3] { e_vert + devide_vert, e_vert + j, s_vert + devide_vert });
            }
        }

        //メッシュ作成
        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();

        return _mesh;
    }
    /// <summary>
    /// [トーラスメッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="s_range">生成する[トーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[トーラスメッシュ]の終点距離</param>
    /// <returns>頂点数自動の[トーラスメッシュ]</returns>
    public static Mesh CreateTorusMesh(float s_range, float e_range)
    {
        return CreateTorusMesh(s_range, e_range, 90, 90);
    }
    /// <summary>
    /// [トーラスメッシュ]を生成して返す：void型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数指定の[トーラスメッシュ]</param>
    /// <param name="s_range">生成する[トーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[トーラスメッシュ]の終点距離</param>
    /// <param name="width_addvertices">生成する[トーラスメッシュ]の追加する水平方向の頂点の数</param>
    /// <param name="height_addvertices">生成する[トーラスメッシュ]の追加する垂直方向の頂点の数</param>
    public static void CreateTorusMesh(out Mesh mesh, float s_range, float e_range, int width_addvertices, int height_addvertices)
    {
        mesh = CreateTorusMesh(s_range, e_range, width_addvertices, height_addvertices);
    }
    /// <summary>
    /// [トーラスメッシュ]を生成して返す：void型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数自動の[トーラスメッシュ]</param>
    /// <param name="s_range">生成する[トーラスメッシュ]の始点距離</param>
    /// <param name="e_range">生成する[トーラスメッシュ]の終点距離</param>
    public static void CreateTorusMesh(out Mesh mesh, float s_range, float e_range)
    {
        mesh = CreateTorusMesh(s_range, e_range);
    }

    #endregion Torus

    #region Apple

    /// <summary>
    /// [リンゴ型3Dメッシュ]を生成して返す：Mesh型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="range">生成する[リンゴ型3Dメッシュ]の距離</param>
    /// <param name="verticalPless">生成する[リンゴ型3Dメッシュ]の縦圧縮係数</param>
    /// <param name="width">生成する[リンゴ型3Dメッシュ]の横方向係数</param>
    /// <param name="height">生成する[リンゴ型3Dメッシュ]の縦方向係数</param>
    /// <param name="width_addvertices">生成する[リンゴ型3Dメッシュ]の追加する水平方向の頂点の数</param>
    /// <param name="height_addvertices">生成する[リンゴ型3Dメッシュ]の追加する垂直方向の頂点の数</param>
    /// <returns>頂点数指定の[リンゴ型3Dメッシュ]</returns>
    public static Mesh CreateApple3DMesh(float range, float verticalPless, float width, float height, int width_addvertices, int height_addvertices)
    {
        /// 参考リンク：https://nyjp07.com/index_apple.html
        //値補正　係数に関しては補正しないのでお好きにどうぞ
        range = range < 0 ? 5.0f : range;
        width_addvertices = width_addvertices < 0 ? 16 : width_addvertices;
        height_addvertices = height_addvertices < 0 ? 16 : height_addvertices;

        //頂点　頂点インデックス定義
        int _vert = 4 + height_addvertices;
        int _round = 3 + width_addvertices;
        var _vertices = new Vector3[_vert * _round + 2];
        var _triangles = new List<int>((_vert - 1) * _round * 6);

        //係数関連定義
        float
            _a = range,              //1.0f  aは元のカージオイドの定数　サイズに直結する ここではレンジを置き換えてサイズにしている
            _b = verticalPless,      //1.2f  縦方向に圧縮する圧縮係数　大きいと縦に伸びる
            _p = width,              //0.15f 下の横幅　大きいと縮む
            _q = height;             //0.08f 上から下までの距離　大きいと縮む
        //計算キャッシュ
        float//90 -> 0 -> -90 の180度回転(縦方向)
            t_start = TwoDePi,//90度
            t_end = -TwoDePi,//-90度
            dot_t = (t_end - t_start) / (float)(_vert + 1);// 変換前のカージオイドの位相角[ラジアン]と位相角の変分[ラジアン]

        //頂点　頂点インデックス代入 (初回)
        _vertices[0] = Vector3.zero;
        _vertices[1] = new Vector3(0, _b * _a * -2.0f * Mathf.Exp(-_q * 9.869605f), 0);
        //アップル半分の頂点座標を計算　計算が重いので以後この頂点座標からx,z座標をY軸に回転させた座標を計算する
        float t = t_start - dot_t;
        for(int i = 0; i < _vert; i++)
        {
            float
                R = _a * (1 - Mathf.Sin(t)),// 変換前のカージオイドの曲座標の動径
                anglePow2 = (t + t_end) * (t + t_end),
                x = R * Mathf.Cos(t) * Mathf.Exp(-_p * anglePow2),
                y = _b * R * Mathf.Sin(t) * Mathf.Exp(-_q * anglePow2);
            _vertices[i + 2] = new Vector3(0, y, -x);
            t -= dot_t;
        }
        //二回目以降の座標処理
        float
            _w_cutangle = TwoPi / (float)_round;
        float _w_angle = TwoDePi - _w_cutangle;
        for(int i = 1; i < _round; i++)
        {
            float
                fix_2D_sin = Mathf.Sin(_w_angle),
                fix_2D_cos = Mathf.Cos(_w_angle);

            for(int j = 0; j < _vert; j++)
            {
                _vertices[2 + i * _vert + j] = new Vector3(_vertices[j + 2].z * fix_2D_cos, _vertices[j + 2].y, _vertices[j + 2].z * fix_2D_sin);
            }
            _w_angle -= _w_cutangle;
        }
        //頂点　頂点インデックス代入
        int _roundvert = _round * _vert,
            _sidevert = 2 + _vert;
        for(int i = 0; i < _round; i++)
        {
            int i_offset = i * _vert;
            _triangles.AddRange(new int[3] { 0, 2 + i_offset, 2 + (_vert + i_offset) % _roundvert });
            for(int j = 0; j < _vert - 1; j++)
            {
                int offset_r = (2 + i_offset) % _roundvert,
                    offset_l = (_sidevert + i_offset) % _roundvert;
                _triangles.AddRange(new int[3] { j + offset_r, j + 1 + offset_r, j + offset_l });
                _triangles.AddRange(new int[3] { j + 1 + offset_l, j + offset_l, j + 1 + offset_r });
            }
            _triangles.AddRange(new int[3] { 1, 1 + _vert + (_vert + i_offset) % _roundvert, 1 + _vert + i_offset });
        }

        //メッシュ生成
        Mesh _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();

        return _mesh;
    }
    /// <summary>
    /// [リンゴ型3Dメッシュ]を生成して返す：Mesh型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="range">生成する[リンゴ型3Dメッシュ]の距離</param>
    /// <param name="verticalPless">生成する[リンゴ型3Dメッシュ]の縦圧縮係数</param>
    /// <param name="width">生成する[リンゴ型3Dメッシュ]の横方向係数</param>
    /// <param name="height">生成する[リンゴ型3Dメッシュ]の縦方向係数</param>
    /// <returns>頂点数自動の[リンゴ型3Dメッシュ]</returns>
    public static Mesh CreateApple3DMesh(float range, float verticalPless, float width, float height)
    {
        return CreateApple3DMesh(range, verticalPless, width, height, 16, 16);
    }
    /// <summary>
    /// [リンゴ型3Dメッシュ]を生成して返す：void型　生成面数：手動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数指定の[リンゴ型3Dメッシュ]</param>
    /// <param name="range">生成する[リンゴ型3Dメッシュ]の距離</param>
    /// <param name="verticalPless">生成する[リンゴ型3Dメッシュ]の縦圧縮係数</param>
    /// <param name="width">生成する[リンゴ型3Dメッシュ]の横方向係数</param>
    /// <param name="height">生成する[リンゴ型3Dメッシュ]の縦方向係数</param>
    /// <param name="width_addvertices">生成する[リンゴ型3Dメッシュ]の追加する水平方向の頂点の数</param>
    /// <param name="height_addvertices">生成する[リンゴ型3Dメッシュ]の追加する垂直方向の頂点の数</param>
    public static void CreateApple3DMesh(out Mesh mesh, float range, float verticalPless, float width, float height, int width_addvertices, int height_addvertices)
    {
        mesh = CreateApple3DMesh(range, verticalPless, width, height, width_addvertices, height_addvertices);
    }
    /// <summary>
    /// [リンゴ型3Dメッシュ]を生成して返す：void型　生成面数：自動　・型一覧{ Mesh, void }
    /// </summary>
    /// <param name="mesh">頂点数自動の[リンゴ型3Dメッシュ]</param>
    /// <param name="range">生成する[リンゴ型3Dメッシュ]の距離</param>
    /// <param name="verticalPless">生成する[リンゴ型3Dメッシュ]の縦圧縮係数</param>
    /// <param name="width">生成する[リンゴ型3Dメッシュ]の横方向係数</param>
    /// <param name="height">生成する[リンゴ型3Dメッシュ]の縦方向係数</param>
    public static void CreateApple3DMesh(out Mesh mesh, float range, float verticalPless, float width, float height)
    {
        mesh = CreateApple3DMesh(range, verticalPless, width, height);
    }

    #endregion Apple

    #endregion Special

    //未完成　ExMeshから関数を呼んでメッシュを生成したい時に、
    //「変数が誤っていないか」「頂点数を余りにも多く指定していないか」等を調べてDebug.Logする
    //命名規則を更新できていなかったり特殊形状メッシュの関数は用意できていないのでまだ気休め程度にしか使えない
#if UNITY_EDITOR
    #region CheckVariavle
    /// <summary>
    /// 扇形メッシュを生成する為の変数の精査を行う関数
    /// </summary>
    /// <param name="angle">角度：0.1f <= angle <= 359.9f を推奨</param>
    /// <param name="range">距離：0.1f <= range <= 200.0f を推奨</param>
    /// <param name="surfaces">枚数：2 <= surfaces <= 359 を推奨</param>
    public static bool CheckVariableFanMesh(float angle, float range, int surfaces)
    {
        bool _perfectVariables = true;
        if(angle <= 0)
        {
            ExDebug.LogError("角度が0未満なのでメッシュに適してないよ！", Color.red, 20);
            _perfectVariables = false;
        }
        if(angle >= 360f)
        {
            ExDebug.LogError("角度が360以上なのでメッシュに適してないよ！", Color.red, 20);
            _perfectVariables = false;
        }
        if(range <= 0)
        {
            ExDebug.LogError("距離が0未満なのでメッシュに適してないよ！", Color.red, 20);
            _perfectVariables = false;
        }
        if(range <= 200)
        {
            ExDebug.LogWarning("距離が200以上だよ！大きすぎると計算誤差やパフォーマンスの低下等の可能性が高まるよ！", Color.blue, 20);
            _perfectVariables = false;
        }
        if(surfaces <= 1)
        {
            ExDebug.LogError("面数が1枚以下だよ！2枚以上生成してね！", Color.red, 20);
            _perfectVariables = false;
        }
        if(surfaces >= 360)
        {
            ExDebug.LogWarning("面数が360枚以上だよ！多すぎるとパフォーマンスが著しく下がる可能性があるよ！", Color.blue, 20);
            _perfectVariables = false;
        }
        if(surfaces > Mathf.CeilToInt(angle))
        {
            ExDebug.Log("角度よりも大きい値が枚数に指定されてるよ！", Color.yellow, 20);
        }

        return _perfectVariables;
    }
    /// <summary>
    /// 扇形メッシュを生成する為の変数の精査を行う関数
    /// </summary>
    /// <param name="angle">角度：0.1f <= angle <= 359.9f を推奨</param>
    /// <param name="range">距離：0.1f <= range <= 200.0f を推奨</param>
    public static bool CheckVariableFanMesh(float angle, float range)
    {
        return CheckVariableFanMesh(angle, range, Mathf.CeilToInt(angle));
    }
    /// <summary>
    /// 扇形トーラスメッシュを生成する為の変数の精査を行う関数
    /// </summary>
    /// <param name="angle">角度：0.1f <= angle <= 359.9f を推奨</param>
    /// <param name="s_range">距離：0.1f <= s_range <= 200.0f を推奨</param>
    /// <param name="e_range">距離：0.1f <= e_range <= 200.0f を推奨</param>
    /// <param name="surfaces">枚数：2 <= surfaces <= 359 を推奨</param>
    public static bool CheckVariableFanTorusMesh(float angle, float s_range, float e_range, int surfaces)
    {
        bool _perfectVariables = true;
        if(angle <= 0)
        {
            ExDebug.LogError("角度が0未満なのでメッシュに適してないよ！", Color.red, 20);
            _perfectVariables = false;
        }
        if(angle >= 360f)
        {
            ExDebug.LogError("角度が360以上なのでメッシュに適してないよ！", Color.red, 20);
            _perfectVariables = false;
        }
        if(s_range <= 0)
        {
            ExDebug.LogError("距離が0未満なのでメッシュに適してないよ！", Color.red, 20);
            _perfectVariables = false;
        }
        if(s_range <= 200)
        {
            ExDebug.LogWarning("距離が200以上だよ！大きすぎると計算誤差やパフォーマンスの低下等の可能性が高まるよ！", Color.blue, 20);
            _perfectVariables = false;
        }
        if(e_range <= 0)
        {
            ExDebug.LogError("距離が0未満なのでメッシュに適してないよ！", Color.red, 20);
            _perfectVariables = false;
        }
        if(e_range <= 200)
        {
            ExDebug.LogWarning("距離が200以上だよ！大きすぎると計算誤差やパフォーマンスの低下等の可能性が高まるよ！", Color.blue, 20);
            _perfectVariables = false;
        }
        if(surfaces <= 1)
        {
            ExDebug.LogError("面数が1枚以下だよ！2枚以上生成してね！", Color.red, 20);
            _perfectVariables = false;
        }
        if(surfaces >= 360)
        {
            ExDebug.LogWarning("面数が360枚以上だよ！多すぎるとパフォーマンスが著しく下がる可能性があるよ！", Color.blue, 20);
            _perfectVariables = false;
        }
        if(surfaces > Mathf.CeilToInt(angle))
        {
            ExDebug.Log("角度よりも大きい値が枚数に指定されてるよ！", Color.yellow, 20);
        }

        return _perfectVariables;
    }
    /// <summary>
    /// 扇形トーラスメッシュを生成する為の変数の精査を行う関数
    /// </summary>
    /// <param name="angle">角度：0.1f <= angle <= 359.9f を推奨</param>
    /// <param name="s_range">距離：0.1f <= s_range <= 200.0f を推奨</param>
    /// <param name="e_range">距離：0.1f <= e_range <= 200.0f を推奨</param>
    public static bool CheckVariableFanTorusMesh(float angle, float s_range, float e_range)
    {
        return CheckVariableFanTorusMesh(angle, s_range, e_range, Mathf.CeilToInt(angle));
    }
    #endregion CheckVariavle
#endif
}