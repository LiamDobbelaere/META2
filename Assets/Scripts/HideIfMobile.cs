﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideIfMobile : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(Application.isMobilePlatform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
