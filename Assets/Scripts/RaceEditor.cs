using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RaceEditor : MonoBehaviour {
    private Transform checkpoints; 

	// Use this for initialization
	void Start () {
        checkpoints = transform.Find("Checkpoints");
	}

    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < checkpoints.childCount; i++)
        {
            var child = checkpoints.GetChild(i);
            var target = checkpoints.GetChild((child.GetSiblingIndex() + 1) % checkpoints.childCount);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(child.position, target.position);
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
