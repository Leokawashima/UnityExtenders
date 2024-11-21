using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GaMe.ExMesh.ExGizmos gizmo;
    
    [SerializeField] GaMe.ExMesh.ExGizmos[] gizmos;

    private void OnDrawGizmos()
    {
        GaMe.ExMesh.ExGizmos.Draw(gizmo);

        foreach (var gizmo in gizmos)
        {
            GaMe.ExMesh.ExGizmos.Draw(gizmo);
        }
    }
}