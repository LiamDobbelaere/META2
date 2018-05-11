using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {
    public static Global global;
    public GameObject coinBurst;
    public GameObject eventInfo;
    public GameObject objectiveMarker;
    public Sprite raceIcon;
    public Sprite sprintIcon;
    public Sprite nullIcon;
    public Material raceMarkerMaterial;
    public Material sprintMarkerMaterial;
    public GameObject countdown;
    public GameObject musicPlayer;
    
	// Use this for initialization
	void Start () {
        global = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CoinBurst(Vector3 position, int coins)
    {
        Instantiate(coinBurst, position, Quaternion.identity).GetComponent<CoinBurstBehavior>().Burst(coins);
    }
}
