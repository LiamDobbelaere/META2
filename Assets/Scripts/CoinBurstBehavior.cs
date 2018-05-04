using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBurstBehavior : MonoBehaviour {
    public GameObject coin;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 15; i++)
            Instantiate(coin, transform.position + Random.insideUnitSphere * 0.1f, Quaternion.identity);

        Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
