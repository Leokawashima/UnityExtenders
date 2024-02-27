using UnityEngine;

/// <summary>
/// ギズモを描くクラス
/// </summary>
[AddComponentMenu("Mesh/ExGizmos")]
public class ExGizmos : MonoBehaviour
{
    #region Variable
    /// <summary>
    /// Headerは変数のグループ分けやジャンルを視覚化して分けるため
    /// 実際の描画に関するEditer拡張はExGizmosEditor.csにまとめている
    /// </summary>
    [Header("ギズモ設定")]
    public Color DrawColor = Color.green;
    public bool IsWire = false;

    [Header("頂点")]
    public bool IsSpecifyVertices = false;
    public int Addvertices = 20;
    [Header("円 / 扇")]
    public bool IsCircle = false;
    public float FanAngle = 180.0f;
    [Header("3D化")]
    public bool Is3D = false;
    public float MeshHeight = 5.0f;
    [Header("トーラス化")]
    public bool IsTorus = false;
    public float MeshRange = 15.0f;
    public float TorusMinRange = 5.0f;
    public float TorusMaxRange = 10.0f;

    public enum MeshMenu { Default, CustumSphere, Torus, Moon, Star, Apple }
    [Header("機能切り替え")]
    public MeshMenu Choice = MeshMenu.Default;

    public enum CustumSphere { HemiSphere, SideSphere, PartsOfSphere }
    [Header("カスタム球")]
    public CustumSphere SphereMode = CustumSphere.HemiSphere;
    public float SphereWidthAngle = 135.0f;
    public float SphereHeightAngle =135.0f;
    public float SphereRange = 5.0f;
    public int SphereWidthAddvertices = 5;
    public int SphereHeightAddvertices = 5;

    [Header("トーラス")]
    public float TorusStartRange = 5.0f;
    public float TorusEndRange = 2.0f;
    public int TorusWidthAddvertices = 20;
    public int TorusHeightAddvertices = 5;

    [Header("ムーン")]
    public float MoonAngle = 359.9f;
    public float MoonBaseRange = 5.0f;
    public float MoonHoleRange = 2.5f;
    public int MoonAddvertices = 50;

    [Header("スター")]
    public float StarOddRange = 5.0f;
    public float StarEvenRange = 2.5f;
    public int StarAddvertices = 3;
    public bool StarIs3D = false;
    public float StarHeight = 5.0f;

    [Header("リンゴ")]
    public int AppleWidthAddvertices = 8;
    public int AppleHeightAddvertices = 8;
    public float AppleRange = 5.0f;
    public float AppleVerticalPless = 1.2f;
    public float AppleWidthCoefficent = 0.15f;
    public float AppleHeightCoefficient = 0.08f;

    [Header("メッシュ関連")]
    public string MeshName = null;
    public string MeshPath = null;

    public Mesh mesh;
    private Transform m_transform;
    #endregion

    #region Draw&Playeraction
    public void MeshChoice()
    {
        switch (Choice) 
        {
            case MeshMenu.Default:
                int adress = 0;
                if(IsSpecifyVertices)  adress += 1;
                if(Is3D)        adress += 2;
                if(IsTorus)     adress += 4;
                if(IsCircle)     adress += 8;

                switch(adress)
                {
                    case 0: ExMesh.CreateFanMesh(out mesh, FanAngle, MeshRange); break;
                    case 1: ExMesh.CreateFanMesh(out mesh, FanAngle, MeshRange, Addvertices); break;
                    case 2: ExMesh.CreateFan3DMesh(out mesh, FanAngle, MeshRange, MeshHeight); break;
                    case 3: ExMesh.CreateFan3DMesh(out mesh, FanAngle, MeshRange, MeshHeight, Addvertices); break;
                    case 4: ExMesh.CreateFanTorusMesh(out mesh, FanAngle, TorusMinRange, TorusMaxRange); break;
                    case 5: ExMesh.CreateFanTorusMesh(out mesh, FanAngle, TorusMinRange, TorusMaxRange, Addvertices); break;
                    case 6: ExMesh.CreateFanTorus3DMesh(out mesh, FanAngle, TorusMinRange, TorusMaxRange, MeshHeight); break;
                    case 7: ExMesh.CreateFanTorus3DMesh(out mesh, FanAngle, TorusMinRange, TorusMaxRange, MeshHeight, Addvertices); break;
                    case 8: ExMesh.CreateCircleMesh(out mesh, MeshRange); break;
                    case 9: ExMesh.CreateCircleMesh(out mesh, MeshRange, Addvertices); break;
                    case 10: ExMesh.CreateCircle3DMesh(out mesh, MeshRange, MeshHeight); break;
                    case 11: ExMesh.CreateCircle3DMesh(out mesh, MeshRange, MeshHeight, Addvertices); break;
                    case 12: ExMesh.CreateCircleTorusMesh(out mesh, TorusMinRange, TorusMaxRange); break;
                    case 13: ExMesh.CreateCircleTorusMesh(out mesh, TorusMinRange, TorusMaxRange, Addvertices); break;
                    case 14: ExMesh.CreateCircleTorus3DMesh(out mesh, TorusMinRange, TorusMaxRange, MeshHeight); break;
                    case 15: ExMesh.CreateCircleTorus3DMesh(out mesh, TorusMinRange, TorusMaxRange, MeshHeight, Addvertices); break;
                }
                break;

            case MeshMenu.CustumSphere:
                switch(SphereMode)
                {
                case CustumSphere.HemiSphere:
                        mesh = ExMesh.CreateFanHemiSphereMesh(SphereWidthAngle, SphereRange, SphereWidthAddvertices, SphereHeightAddvertices); break;
                case CustumSphere.SideSphere:
                        mesh = ExMesh.CreateFanSideSphereMesh(SphereHeightAngle, SphereRange, SphereWidthAddvertices, SphereHeightAddvertices); break;
                case CustumSphere.PartsOfSphere:
                        mesh = ExMesh.CreateFanPartsOfSphereMesh(SphereWidthAngle, SphereHeightAngle, SphereRange, SphereWidthAddvertices, SphereHeightAddvertices); break;
                }
                break;
            case MeshMenu.Torus:
                mesh = ExMesh.CreateTorusMesh(TorusStartRange, TorusEndRange, TorusWidthAddvertices, TorusHeightAddvertices);
                break;
            case MeshMenu.Moon:
                mesh = ExMesh.CreateMoonMesh(MoonAngle, MoonBaseRange, MoonHoleRange, MoonAddvertices);
                break;
            case MeshMenu.Star:
                if(!StarIs3D) 
                    mesh = ExMesh.CreateStarMesh(StarOddRange, StarEvenRange, StarAddvertices);
                else
                    mesh = ExMesh.CreateStar3DMesh(StarOddRange, StarEvenRange, StarHeight, StarAddvertices);
                break;
            case MeshMenu.Apple:
                mesh = ExMesh.CreateApple3DMesh(AppleRange, AppleVerticalPless, AppleWidthCoefficent, AppleHeightCoefficient, AppleWidthAddvertices, AppleHeightAddvertices);
                break;
        } 

        
    }
    private void Reset()
    {
        if(m_transform == null) m_transform = transform;
        MeshChoice();
    }
    private void Awake()
    {
        if (m_transform == null) m_transform = transform;
        MeshChoice();
    }
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (!gameObject.activeInHierarchy) return;
#endif
        Gizmos.color = DrawColor;
        if (m_transform == null) m_transform = transform;
        if (IsWire)
        {
            Gizmos.DrawWireMesh(mesh, m_transform.position, m_transform.rotation, m_transform.localScale);
        }
        else
        {
            Gizmos.DrawMesh(mesh, m_transform.position, m_transform.rotation, m_transform.localScale);
        }
    }
    #endregion
}