using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Mode1()
	{
		PlayerPrefs.SetInt ("Mode", 1);
		UIManager.Instance.btnsound ();
		UIManager.InstantitatePrefab ("LevelSelection");
		Destroy (this.gameObject);
	}

	public void Mode2()
	{
		PlayerPrefs.SetInt ("Mode", 2);
		UIManager.Instance.btnsound ();
		UIManager.InstantitatePrefab ("LevelSelection1");
		Destroy (this.gameObject);
	}

	public void Back()
	{
		UIManager.InstantitatePrefab ("MainMenu");

		Destroy (this.gameObject);

	}
}
