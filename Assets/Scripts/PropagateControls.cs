using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PropagateControls : MonoBehaviour {
    public enum Control { Accelerate, Decelerate, TurnLeft, TurnRight, PitchUp, PitchDown, TwistLeft, TwistRight};
    public Control control;

    private ShipController shipController;

    private RectTransform rectTransform;

    // Use this for initialization
    void Start () {
        shipController = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipController>();
        rectTransform = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            Debug.Log("!");
            rectTransform.anchoredPosition += touchDeltaPosition;
        }
    }
}
