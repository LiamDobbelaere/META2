using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class ShipController : MonoBehaviour {
    private Rigidbody rb;
    public float paramDivider = 30f;

    public float accelerationAxis = 0f;
    public float turningAxis = 0f;
    public float tiltAxis = 0f;
    public float spinAxis = 0f;

    private float accelerationAxisSmooth = 0f;
    private float turningAxisSmooth = 0f;
    private float tiltAxisSmooth = 0f;
    private float spinAxisSmooth = 0f;

    private float t = 0.1f;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update () {
        accelerationAxis = Input.GetAxis("Vertical");
        turningAxis = Input.GetAxis("Horizontal");
        tiltAxis = Input.GetAxis("Tilt");
        spinAxis = Input.GetAxis("Spin");

        accelerationAxisSmooth = Mathf.Lerp(accelerationAxisSmooth, accelerationAxis, t);
        turningAxisSmooth = Mathf.Lerp(turningAxisSmooth, turningAxis, t);
        tiltAxisSmooth = Mathf.Lerp(tiltAxisSmooth, tiltAxis, t);
        spinAxisSmooth = Mathf.Lerp(spinAxisSmooth, spinAxis, t);

        rb.AddRelativeForce(new Vector3(0, 0, accelerationAxisSmooth * 50f));
        rb.AddRelativeTorque(new Vector3(tiltAxisSmooth * (0.3f / paramDivider), 
            turningAxisSmooth * (0.6f / paramDivider), spinAxisSmooth * (0.2f / paramDivider)));

        rb.velocity *= 0.95f;
        rb.angularVelocity *= 0.95f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            other.gameObject.GetComponent<CoinBehavior>().Collect();
        }
    }
}
