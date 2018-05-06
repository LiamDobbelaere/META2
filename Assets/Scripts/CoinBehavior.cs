using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour {
    private Material mat;
    private GameObject player;
    private Rigidbody rb;

    private float passedTime = 0f;
    private SphereCollider col;
    private bool collected = false;

	// Use this for initialization
	void Start () {
        mat = GetComponent<Renderer>().material;
        mat.mainTextureOffset += new Vector2(0f, Random.Range(0f, 1f));

        player = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f)), ForceMode.Impulse);
        rb.AddTorque(new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f)), ForceMode.Impulse);

        col = GetComponent<SphereCollider>();
        col.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        mat.mainTextureOffset = Vector2.Lerp(mat.mainTextureOffset, mat.mainTextureOffset + new Vector2(0, 1f), 0.003f);

        passedTime += Time.deltaTime;

        if (passedTime > 1f)
        {
            col.enabled = true;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 2f * Time.deltaTime);
        }

        if (collected)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.25f);
            if (transform.localScale.x < 0.01f && !GetComponent<AudioSource>().isPlaying) Destroy(gameObject);
        }
    }

    public void Collect(int consecutiveCoins)
    {
        GetComponent<AudioSource>().pitch = Mathf.Clamp(.8f + (float)consecutiveCoins / 100f, .8f, 1.3f);
        
        GetComponent<AudioSource>().Play();
        collected = true;
    }
}
