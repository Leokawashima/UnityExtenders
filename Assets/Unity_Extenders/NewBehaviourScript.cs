using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GaMe.ExMesh.ExGizmos gizmo;
    
    [SerializeField] GaMe.ExMesh.ExGizmos[] gizmos;


    [SerializeField] Transform TS;

    [SerializeField] Vector3 pos;
    [SerializeField] Vector3 qua;
    [SerializeField] Vector3 sca;

    private void OnDrawGizmos()
    {
        GaMe.ExMesh.ExGizmos.Draw(gizmo);

        foreach (var gizmo in gizmos)
        {
            GaMe.ExMesh.ExGizmos.Draw(gizmo);
        }

        var _ex = Gizmos.matrix;
        if (TS != null)
        {
            var mat = Matrix4x4.TRS(TS.position, TS.rotation, TS.lossyScale);
            mat *= Matrix4x4.TRS(pos, Quaternion.Euler(qua) , sca);
            Gizmos.matrix = mat;
        }

        Gizmos.DrawCube(Vector3.zero, Vector3.one * 10);

        Gizmos.matrix = _ex;
    }
}