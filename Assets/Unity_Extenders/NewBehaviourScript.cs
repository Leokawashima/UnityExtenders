using GaMe.ExMesh;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GaMe.ExMesh.ExGizmos gizmo;
    
    [SerializeField] GaMe.ExMesh.ExGizmos[] gizmos;

    private void OnDrawGizmos()
    {
        GaMe.ExMesh.ExGizmos.Draw(gizmo);
    }
}