using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Advertisements;
public class LevelFailMenu : MonoBehaviour {
	public GameObject respawnbutton;
	GameObject player;
	AudioSource bgmusic;


	// Use this for initialization
	void Start () {


		UIManager.Instance.DisableHudmenu ();

		player = GameObject.FindGameObjectWithTag ("Player");
//		bgmusic = GameObject.FindGameObjectWithTag ("hudmenu").GetComponent<AudioSource> ();

		Time.timeScale = 0f;
		AudioListener.volume  = 0f;

	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Restart()
	{
		TGS_Constants.scenetoload = 1;
		UIManager.SceneToLoad ();
        UIManager.Instance.btnsound();
    }

	public void MainMenu()
	{

		TGS_Constants.scenetoload = 0;
		UIManager.InstantitatePrefab ("Loading");
        UIManager.SceneToLoad();
		Destroy (this.gameObject);
        UIManager.Instance.btnsound();
    }


	public void Respawn()
	{
		Time.timeScale = 1;
		player.transform.position = TGS_Constants.respawnpos;
		player.transform.eulerAngles = TGS_Constants.respawnrot;
		UIManager.Instance.Enablehudmenu ();
		player.GetComponent<Rigidbody> ().isKinematic = true;

		TGS_Constants.accel = false;
		TGS_Constants.brake = false;
		TGS_Constants.steerleft = false;
		TGS_Constants.steerright = false;
//		player.GetComponent<BikeControl> ().speed = 0f;
		player.GetComponent<Rigidbody> ().isKinematic = false;
//		#if !UNITY_EDITOR
//		AdsCallerManager.Instance.ShowAd ();
//		#endif
		checkvolume ();
		Destroy (this.gameObject);



	}

	void checkvolume()
	{


		if (PlayerPrefs.GetInt ("Sound") == 0) {
			Debug.Log ("audio0");

			AudioListener.volume = 1f;


		} else if (PlayerPrefs.GetInt ("Sound") == 1) {
			Debug.Log ("audio1");

			AudioListener.volume = 0f;


		}

}
}
