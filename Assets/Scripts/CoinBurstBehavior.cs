using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBurstBehavior : MonoBehaviour {
    public GameObject coin;

    private float timePassed = 0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;

        if (timePassed > 1f)
        {
            for (int i = 0; i < 16; i++)
                Instantiate(coin, transform.position + Random.insideUnitSphere * 0.05f, Quaternion.identity);

            Destroy(gameObject);
        }
	}
}
