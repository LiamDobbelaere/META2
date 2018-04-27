using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour {
    public Rigidbody rb;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update () {
        rb.AddRelativeForce(new Vector3(0, 0, Input.GetAxis("Vertical") * 50f));

        rb.AddRelativeTorque(new Vector3(Input.GetAxis("Tilt") * 0.3f, Input.GetAxis("Horizontal") * 0.6f, Input.GetAxis("Spin") * 0.2f));

        rb.velocity *= 0.95f;
        rb.angularVelocity *= 0.95f;
    }
}
