using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyprefab : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(this.gameObject, 2f);
	}
}
