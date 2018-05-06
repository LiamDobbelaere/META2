using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RaceCheckpointEditor : MonoBehaviour {
    // Use this for initialization
    void Start () {

    }

    void OnDrawGizmosSelected()
    {
        if (!transform.parent) return;

        var target = transform.parent.GetChild((transform.GetSiblingIndex() + 1) % transform.parent.childCount);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, target.position);
    }
}
