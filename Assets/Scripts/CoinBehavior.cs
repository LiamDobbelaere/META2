using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour {
    private Material mat;
    private GameObject player;
    private Rigidbody rb;

    private float passedTime = 0f;
    private SphereCollider collider;
    private bool collected = false;

	// Use this for initialization
	void Start () {
        mat = GetComponent<Renderer>().material;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f)), ForceMode.Impulse);
        rb.AddTorque(new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f)), ForceMode.Impulse);
        collider = GetComponent<SphereCollider>();
        collider.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        mat.mainTextureOffset = Vector2.Lerp(mat.mainTextureOffset, mat.mainTextureOffset + new Vector2(0, 1f), 0.003f);

        passedTime += Time.deltaTime;

        if (passedTime > 2f)
        {
            collider.enabled = true;
            if (Vector3.Distance(player.transform.position, transform.position) < 0.01f)
            {
                rb.AddForce(player.transform.position - transform.position);
            }
            else
            {
                rb.velocity = (player.transform.position - transform.position) * 5f;
            }
        }

        if (collected)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.25f);
            if (transform.localScale.x < 0.01f) Destroy(gameObject);
        }
    }

    public void Collect()
    {
        collected = true;
    }
}
