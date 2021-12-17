using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public GameObject startScene;
	public GameObject wapon;
    public GameObject waponlist;
	//public GameObject scene2;


	public	AudioSource bgmusic;
	//public GameObject[] cars;
	public GameObject[] levelpos;
	//public GameObject[] levelgroundpos;

	public Text timer;
	int minutes;
	bool once;
	GameObject player;
	void Awake()
	{
		Time.timeScale = 1f;
		instance = this;      
    
      //  InstantiateBike();

    }


	// Use this for initialization
	void Start () {
        InstantiateLevel();
        once = false;
		TGS_Constants.accel = false;
		TGS_Constants.brake = false;
		TGS_Constants.steerleft = false;
		TGS_Constants.steerright = false;
		TGS_Constants.respawntriggered = false;
		TGS_Constants.hurdle = false;
		TGS_Constants.treefall = false;
		TGS_Constants.wheeling = false;
		TGS_Constants.checkpointcounter = 0;
        TGS_Constants.play = false;
		//
		TGS_Constants.leveltime = new float[]{40f ,65f,65f,68f,50f,50,60,80, 87f, 70f};
		//Music

		if (PlayerPrefs.GetInt ("music") == 0) {
			Debug.Log ("volume1");
			bgmusic.volume = 1f;


		} else if (PlayerPrefs.GetInt ("music") == 1) {
			Debug.Log ("volume0");

			bgmusic.volume = 0f;


		}
//
		if (PlayerPrefs.GetInt ("Sound") == 0) {
			Debug.Log ("audio0");

			AudioListener.volume = 1f;


		} else if (PlayerPrefs.GetInt ("Sound") == 1) {
			Debug.Log ("audio1");

			AudioListener.volume = 0f;
		

		}
		if (PlayerPrefs.GetInt ("Mode") == 1) {
			GameObject.FindGameObjectWithTag ("map").SetActive (false);

		}
		//
//		AdsCallerManager.Instance.HideBanner();
	}
	
	// Update is called once per frame
	void Update () {
       if( TGS_Constants.EnemyCounter[TGS_Constants.currentlevel - 1] == 0)
        {
            Instantiate(Resources.Load("Menus/LevelClearMenu"));
        }
        Debug.Log(TGS_Constants.currentlevel);
	}

	void Timer()
	{
		if (TGS_Constants.leveltime [TGS_Constants.currentlevel - 1] > 1) {
			TGS_Constants.leveltime [TGS_Constants.currentlevel - 1] -= Time.deltaTime;
			minutes = Mathf.FloorToInt ((TGS_Constants.leveltime [TGS_Constants.currentlevel - 1] / 60));
			int seconds = Mathf.FloorToInt ((TGS_Constants.leveltime [TGS_Constants.currentlevel - 1] % 60));
			timer.text = string.Format ("{0:00}:{1:00}", minutes, seconds);
		} else {
			if (!once) {
				UIManager.InstantitatePrefab ("LevelFailMenu");

				once = true;
			}
		}


	}

	void InstantiateLevel()
	{
		
		Instantiate(Resources.Load("Levels/Level0"+TGS_Constants.currentlevel));
        Debug.Log("Called");

	}

	void InstantiateBike()
	{
		Instantiate(Resources.Load("Bikes/bike"+(TGS_Constants.currentObject+1)));
        
	}


}
