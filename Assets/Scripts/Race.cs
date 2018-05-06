using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject child;

        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i).gameObject;
            child.GetComponent<RaceCheckpoint>().ResetState();
            child.SetActive(false);
        }

        child = transform.GetChild(0).gameObject;
        child.GetComponent<RaceCheckpoint>().ResetState();
        child.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void AdvanceCheckpoint(int siblingIndex)
    {
        GameObject child = transform.GetChild((siblingIndex + 1) % transform.childCount).gameObject;
        child.GetComponent<RaceCheckpoint>().ResetState();
        child.SetActive(true);
    }
}
