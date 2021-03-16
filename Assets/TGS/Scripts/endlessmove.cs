using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endlessmove : MonoBehaviour {
	public int speed;
	public GameObject patch1;
	public GameObject patch2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * speed * Time.deltaTime);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("trigger1"))
			patch1.transform.position = new Vector3 (patch1.transform.position.x,patch1.transform.position.y,patch1.transform.position.z+135f);
		if (other.gameObject.CompareTag ("trigger2"))
			patch2.transform.position = new Vector3 (patch2.transform.position.x,patch2.transform.position.y,patch2.transform.position.z+135f);

	}



}
