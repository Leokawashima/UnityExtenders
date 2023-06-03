using UnityEngine;

/// <summary>
/// ギズモを描くクラス
/// </summary>
[AddComponentMenu("Mesh/ExGizmos")]
public class ExGizmos : MonoBehaviour
{
    #region Variable
    /// <summary>
    /// あくまでこのHeaderは変数のグループ分けやジャンルを視覚化して分けるため　Editer拡張はExGizmosEditor.csにまとめている
    /// </summary>
    [Header("ギズモ設定")]
    public Color color = Color.green;
    public bool onWireMode = false;

    [Header("頂点")]
    public bool onHandMadeMode = false;
    public int addvertices = 20;
    [Header("円 / 扇")]
    public bool onCycleMode = false;
    public float fanAngle = 180.0f;
    [Header("3D化")]
    public bool on3DMode = false;
    public float meshHeight = 5.0f;
    [Header("トーラス化")]
    public bool onTorusMode = false;
    public float meshrange = 15.0f;
    public float torusMinRange = 5.0f;
    public float torusMaxRange = 10.0f;

    public enum MeshMenu { Default, CustumSphere, Torus, Moon, Star, Apple }
    [Header("機能切り替え")]
    public MeshMenu choicemesh = MeshMenu.Default;

    public enum CustumSphere { HemiSphere, SideSphere, PartsOfSphere }
    [Header("カスタム球")]
    public CustumSphere sphereMode = CustumSphere.HemiSphere;
    public float sphere_w_angle = 135.0f;
    public float sphere_h_angle =135.0f;
    public float sphere_range = 5.0f;
    public int sphere_w_addvertices = 5;
    public int sphere_h_addvertices = 5;

    [Header("トーラス")]
    public float torus_s_range = 5.0f;
    public float torus_e_range = 2.0f;
    public int torus_w_addvertices = 20;
    public int torus_h_addvertices = 5;

    [Header("ムーン")]
    public float Moon_angle = 359.9f;
    public float Moon_base_range = 5.0f;
    public float Moon_hole_range = 2.5f;
    public int Moon_addvertices = 50;

    [Header("スター")]
    public float star_odd_range = 5.0f;
    public float star_even_range = 2.5f;
    public int star_addvertices = 3;
    public bool star_3D = false;
    public float star_height = 5.0f;

    [Header("リンゴ")]
    public int apple_width_addvertices = 8;
    public int apple_height_addvertices = 8;
    public float apple_range = 5.0f;
    public float apple_verticalpless = 1.2f;
    public float apple_width_coefficent = 0.15f;
    public float apple_height_coefficient = 0.08f;

    [Header("メッシュ関連")]
    public string meshName = null;
    public string meshPath = null;

    public Mesh mesh;
    Transform tf;
    #endregion

    #region Draw&Playeraction
    public void MeshChoice()
    {
        switch (choicemesh) 
        {
            case MeshMenu.Default:
                int adress = 0;
                if(onHandMadeMode)  adress += 1;
                if(on3DMode)        adress += 2;
                if(onTorusMode)     adress += 4;
                if(onCycleMode)     adress += 8;

                switch(adress)
                {
                    case 0: ExMesh.CreateFanMesh(out mesh, fanAngle, meshrange); break;
                    case 1: ExMesh.CreateFanMesh(out mesh, fanAngle, meshrange, addvertices); break;
                    case 2: ExMesh.CreateFan3DMesh(out mesh, fanAngle, meshrange, meshHeight); break;
                    case 3: ExMesh.CreateFan3DMesh(out mesh, fanAngle, meshrange, meshHeight, addvertices); break;
                    case 4: ExMesh.CreateFanTorusMesh(out mesh, fanAngle, torusMinRange, torusMaxRange); break;
                    case 5: ExMesh.CreateFanTorusMesh(out mesh, fanAngle, torusMinRange, torusMaxRange, addvertices); break;
                    case 6: ExMesh.CreateFanTorus3DMesh(out mesh, fanAngle, torusMinRange, torusMaxRange, meshHeight); break;
                    case 7: ExMesh.CreateFanTorus3DMesh(out mesh, fanAngle, torusMinRange, torusMaxRange, meshHeight, addvertices); break;
                    case 8: ExMesh.CreateCircleMesh(out mesh, meshrange); break;
                    case 9: ExMesh.CreateCircleMesh(out mesh, meshrange, addvertices); break;
                    case 10: ExMesh.CreateCircle3DMesh(out mesh, meshrange, meshHeight); break;
                    case 11: ExMesh.CreateCircle3DMesh(out mesh, meshrange, meshHeight, addvertices); break;
                    case 12: ExMesh.CreateCircleTorusMesh(out mesh, torusMinRange, torusMaxRange); break;
                    case 13: ExMesh.CreateCircleTorusMesh(out mesh, torusMinRange, torusMaxRange, addvertices); break;
                    case 14: ExMesh.CreateCircleTorus3DMesh(out mesh, torusMinRange, torusMaxRange, meshHeight); break;
                    case 15: ExMesh.CreateCircleTorus3DMesh(out mesh, torusMinRange, torusMaxRange, meshHeight, addvertices); break;
                }
                break;

            case MeshMenu.CustumSphere:
                switch(sphereMode)
                {
                case CustumSphere.HemiSphere:
                        mesh = ExMesh.CreateFanHemiSphereMesh(sphere_w_angle, sphere_range, sphere_w_addvertices, sphere_h_addvertices); break;
                case CustumSphere.SideSphere:
                        mesh = ExMesh.CreateFanSideSphereMesh(sphere_h_angle, sphere_range, sphere_w_addvertices, sphere_h_addvertices); break;
                case CustumSphere.PartsOfSphere:
                        mesh = ExMesh.CreateFanPartsOfSphereMesh(sphere_w_angle, sphere_h_angle, sphere_range, sphere_w_addvertices, sphere_h_addvertices); break;
                }
                break;
            case MeshMenu.Torus:
                mesh = ExMesh.CreateTorusMesh(torus_s_range, torus_e_range, torus_w_addvertices, torus_h_addvertices);
                break;
            case MeshMenu.Moon:
                mesh = ExMesh.CreateMoonMesh(Moon_angle, Moon_base_range, Moon_hole_range, Moon_addvertices);
                break;
            case MeshMenu.Star:
                if(!star_3D) 
                    mesh = ExMesh.CreateStarMesh(star_odd_range, star_even_range, star_addvertices);
                else
                    mesh = ExMesh.CreateStar3DMesh(star_odd_range, star_even_range, star_height, star_addvertices);
                break;
            case MeshMenu.Apple:
                mesh = ExMesh.CreateApple3DMesh(apple_range, apple_verticalpless, apple_width_coefficent, apple_height_coefficient, apple_width_addvertices, apple_height_addvertices);
                break;
        } 

        
    }
    void Reset()
    {
        if(tf == null) tf = transform;
        MeshChoice();
    }
    void OnEnable()
    {
        if (tf == null) tf = transform;
        MeshChoice();
    }
    void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if(!gameObject.activeInHierarchy) return;
#endif
        Gizmos.color = color;
        if(tf == null) tf = transform;
        if(!onWireMode)     Gizmos.DrawMesh(mesh, tf.position, tf.rotation, tf.localScale);
        else            Gizmos.DrawWireMesh(mesh, tf.position, tf.rotation, tf.localScale);
    }
    #endregion
}