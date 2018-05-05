using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelerometerDebug : MonoBehaviour {
    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = Input.acceleration.x.ToString("0.00") + ", " + 
            Input.acceleration.y.ToString("0.00") + ", " + 
            Input.acceleration.z.ToString("0.00");

    }
}
