using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneenabler : MonoBehaviour {
	public GameObject scene;
	public GameObject scene1;
	int rand;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("Mode") == 1) {

			scene.SetActive (true);
		} else {

			scene1.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
