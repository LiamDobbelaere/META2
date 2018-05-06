using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBurstBehavior : MonoBehaviour {
    public GameObject coin;
    //private bool bursted = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        /*if (bursted && !GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }*/
	}

    public void Burst(int coins)
    {
        for (int i = 0; i < coins; i++)
            Instantiate(coin, transform.position + Random.insideUnitSphere * 0.05f, Quaternion.identity);

        //bursted = true;
        Destroy(gameObject);

    }
}
