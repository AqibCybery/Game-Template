using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playercontrol : MonoBehaviour {
	int totalcheckpoint;
    public GameObject firePrefacb;
    public WheelCollider fl;
	public GameObject[] checkpoints;
	GameObject level;
	public AudioClip[] birdssounds;
	public AudioSource cycletyre;
	public AudioSource cycleneutral;
	GameObject flipbutton;
    WheelHit hit;
	Rigidbody rb;
	public	AudioSource audio;
	bool once;
	Text checkpointtext;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("Mode") == 2) {
			totalcheckpoint = GameObject.FindGameObjectsWithTag ("checkpoint").Length;
			checkpoints = new GameObject[totalcheckpoint];

			level = GameObject.Find ("Level0" + TGS_Constants.currentlevel + "(Clone)");

			for (int i = 0; i < totalcheckpoint; i++) {
				checkpoints [i] = level.transform.GetChild (i).gameObject;
				if (i != 0)
					checkpoints [i].SetActive (false);
			}
		}

		cycletyre = GameObject.FindGameObjectWithTag ("cycletyre").GetComponent<AudioSource>();
		cycleneutral = GameObject.FindGameObjectWithTag ("cycleneutral").GetComponent<AudioSource>();
		//flipbutton =	GameObject.FindGameObjectWithTag ("flip");
		//checkpointtext = GameObject.FindGameObjectWithTag ("checkpointcounter").GetComponent<Text> ();
		//checkpointtext.text = (totalcheckpoint).ToString ();
		//flipbutton.SetActive (false);
		once = true;
//		GameObject.FindGameObjectWithTag ("arrow").transform.parent = gameObject.transform;
//		GameObject.FindGameObjectWithTag ("MainCamera").transform.parent = gameObject.transform;

//		checkpoints [0].SetActive (true);
	}
    //void prefabdestroy() {
    //firePrefacb
    //}
    // Update is called once per frame
    void Update() {
       
        if (fl.GetGroundHit(out hit)) {
            if (hit.collider.tag == "tile") {
                //				rb =	hit.collider.GetComponent<Rigidbody> ();
                Invoke("falltile", 1f);
            }
            if (hit.collider.tag == "bomb")
            {
                Debug.Log("bomb");
                Instantiate(firePrefacb, transform.position, transform.rotation);
                TGS_Constants.hurdle = true;
                Time.timeScale = 0.3f;
                Invoke("LevelFailMenu", 1f);
            }
            if (hit.collider.tag == "hurdle") {
                if (!TGS_Constants.hurdle)
                    TGS_Constants.hurdle = true;
            }
        }
    
     

//		Debug.Log (TGS_Constants.bikespeed);
		if ((TGS_Constants.bikespeed >= 2) && TGS_Constants.accel) {
			if (!cycletyre.isPlaying)
			cycletyre.Play ();
			if (cycleneutral.isPlaying)
				cycleneutral.Stop ();
		} 

		if ((TGS_Constants.bikespeed >= 2) && (!TGS_Constants.accel)) {
			if (!cycleneutral.isPlaying)
				cycleneutral.Play ();
			if (cycletyre.isPlaying)
				cycletyre.Stop ();
		}

		if (TGS_Constants.bikespeed < 2) {
			cycleneutral.Stop ();
			cycletyre.Stop ();
		}
	}

	void falltile()
	{
		if (hit.collider.tag == "tile") {

			hit.collider.GetComponent<Rigidbody> ().isKinematic = false;
			hit.collider.enabled = false;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "hurdle") {
			TGS_Constants.hurdle = true;
			other.collider.enabled = false;
//			other.gameObject.AddComponent<Rigidbody> ();
			Time.timeScale = 0.3f;
			Invoke ("LevelFailMenu", 0.5f);


		}

		if (other.gameObject.tag == "tile") {
			other.gameObject.GetComponent<Rigidbody> ().isKinematic = false;

		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "parking") {
//			Invoke ("LevelClearMenu", 1f);
			if ( TGS_Constants.checkpointcounter >= totalcheckpoint)
			Invoke ("LevelClearMenu", 1f);

		}
		if (other.gameObject.tag == "hurdle") {
			TGS_Constants.hurdle = true;
			Time.timeScale = 0.3f;
			Invoke ("LevelFailMenu", 0.5f);
		}

		if (other.gameObject.tag == "checkpoint") {
			if (once) {
				Destroy (other.gameObject);
				Destroy (GameObject.FindGameObjectWithTag ("item").gameObject as GameObject);
				TGS_Constants.checkpointcounter++;
				totalcheckpoint--;
				checkpointtext.text = totalcheckpoint.ToString ();
				checkpoints [TGS_Constants.checkpointcounter].SetActive (true);
		

				once = false;
				Invoke ("OnceTrue", 1f);
			}
		}
		if (other.gameObject.tag == "treefall") {
			TGS_Constants.treefall = true;

		}

		if (other.gameObject.tag == "bird") {
			int rand = Random.Range (0, 3);
			audio.PlayOneShot (birdssounds [rand]);
			audio.volume = 0.2f;
		}

		if (other.gameObject.tag == "fliptrigger") {
			flipbutton.SetActive (true);
			Invoke ("flipover", 2f);
		}
	}

	void OnceTrue()
		{
			once = true;

		}

	void LevelClearMenu()
	{
		UIManager.InstantitatePrefab ("LevelClearMenu");
		Time.timeScale = 0f;
	}

	void LevelFailMenu()
	{
		UIManager.InstantitatePrefab ("LevelFailMenu");
		Time.timeScale = 0f;
	}

	void flipover()
	{
		flipbutton.SetActive (false);
		TGS_Constants.wheeling = false;

	}
}
