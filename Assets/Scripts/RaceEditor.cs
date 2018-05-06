using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RaceEditor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var target = transform.GetChild((child.GetSiblingIndex() + 1) % transform.childCount);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(child.position, target.position);
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
