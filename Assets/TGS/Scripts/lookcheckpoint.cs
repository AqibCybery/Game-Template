﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookcheckpoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (GameObject.FindGameObjectWithTag ("checkpoint").transform);

	}
}
