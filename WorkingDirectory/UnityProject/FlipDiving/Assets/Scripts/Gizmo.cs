using UnityEngine;
using System.Collections;

public class Gizmo : MonoBehaviour {

    public float gizmoSize = 1f;

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }
}
