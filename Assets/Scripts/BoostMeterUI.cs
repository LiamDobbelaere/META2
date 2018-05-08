using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostMeterUI : MonoBehaviour {
    private ShipController shipController;
    private Image image;

    // Use this for initialization
	void Start () {
        shipController = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipController>();
        image = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        image.fillAmount = shipController.boostAmount / 200f;
	}
}
