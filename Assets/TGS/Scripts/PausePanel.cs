using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioListener.volume  = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Restart()
	{
		Time.timeScale = 1f;
		TGS_Constants.scenetoload = 1;
		UIManager.InstantitatePrefab ("Loading");

		UIManager.Instance.btnsound ();

		UIManager.SceneToLoad ();
	}

	public void MainhMenu()
	{
		TGS_Constants.scenetoload = 0;
		UIManager.InstantitatePrefab ("Loading");
		UIManager.SceneToLoad ();
		UIManager.Instance.btnsound ();

		Destroy (this.gameObject);

	}

	public void Resume()
	{
		Time.timeScale = 1;
		UIManager.Instance.Enablehudmenu ();
		UIManager.Instance.btnsound ();

		Destroy (this.gameObject);

	}
}
