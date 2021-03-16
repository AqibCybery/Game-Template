using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilefall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnCollisionEnter(Collision other)
	{
		Debug.Log ("coll");

		if (other.gameObject.tag != "parking") {
			Debug.Log ("coll11");

			GetComponent<Rigidbody> ().isKinematic = false;

		}
	}

}
