using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Utility;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CarSelection : MonoBehaviour {
	public GameObject[] cars,acc;
	public string  nextObjectToInstantiate;
	public string  backObjectToInstantiate;
	public string menutoenable;
	GameObject carsparent;
	public int[]  carsprice;
	GameObject cam;
	public GameObject bikes;
	public GameObject campoint;
	public GameObject buybutton,nextbutton,scorebg;
	public Text score;
	public GameObject lockbutton;
	float refvelocity =0.0f;
	public Slider speedslider;
	public Slider brakeslider;
	public Text acceleration,speed,turn;
	public float[] speedstartingvalue;
	public float[] speedlimit;
	public float[] brakestartingvalue;
	public float[] brakelimit;
	public Material[] bikesmat;
	string buttonanme;
	public Material[] carcolor;
	public GameObject bikeselection;
	public GameObject menupanel;
	public Texture[] skinstextures;
	public GameObject mainmenutitle , bikeselectiontitle ;
//	public Transform[] points;
//	public GameObject[] lookatpoints;
	GameObject pointsparent;

	float pos;
    private void Awake()
    {
		TGS_Constants.CurrentScene = ScenesList.ObjectSelection;

	}

	// Use this for initialization
	void Start () {
      //  PlayerPrefs.DeleteAll();
       // PlayerPrefs.SetInt ("score", 3000);
		TGS_Constants.currentcar = 0;
		carsparent = GameObject.FindGameObjectWithTag ("cars");
		for (int i = 0; i < (carsparent.transform.childCount); i++) {

			cars [i] = carsparent.transform.GetChild (i).transform.gameObject;
			cars [TGS_Constants.currentcar].SetActive (true);
		}
////	
////
		score.text ="$" + PlayerPrefs.GetInt ("score").ToString ();
		CheckCarUnlock ();
		//if (PlayerPrefs.GetFloat ("speedbike0") == 0) {
		//	PlayerPrefs.SetFloat ("speedbike0", speedstartingvalue [0]);
		//}
		//if (PlayerPrefs.GetFloat ("speedbike1") == 0) {
		//	PlayerPrefs.SetFloat ("speedbike1", speedstartingvalue [1]);
		//}
		//if (PlayerPrefs.GetFloat ("speedbike2") == 0) {
		//	PlayerPrefs.SetFloat ("speedbike2", speedstartingvalue [2]);
		//}
		//if (PlayerPrefs.GetFloat ("brakebike0") == 0) {
		//	PlayerPrefs.SetFloat ("brakebike0", brakestartingvalue [0]);
		//}
		//if (PlayerPrefs.GetFloat ("brakebike1") == 0) {
		//	PlayerPrefs.SetFloat ("brakebike1", brakestartingvalue [1]);
		//}
		//if (PlayerPrefs.GetFloat ("brakebike2") == 0) {
		//	PlayerPrefs.SetFloat ("brakebike2", brakestartingvalue [2]);
		//}
		
		buybutton.transform.GetChild (0).GetComponent<Text> ().text = carsprice [TGS_Constants.currentcar].ToString ();
	}

	
	// Update is called once per frame
	void Update () {
//		cam.transform.RotateAround (cars [TGS_Constants.currentcar].transform.position, Vector3.down, 20 * Time.deltaTime);
//		cam.transform.position = new Vector3(	Mathf.SmoothDamp(cam.transform.position.x , points[TGS_Constants.currentcar].transform.position.x,ref refvelocity,1f),cam.transform.position.y,cam.transform.position.z);
		//Debug.Log(PlayerPrefs.GetFloat ("speedbike0"));


	}



	public	void Right()
	{
		TGS_Constants.currentcar++;
		if (TGS_Constants.currentcar == cars.Length )
			TGS_Constants.currentcar = 0;
		for (int i = 0; i < cars.Length; i++) {
            if (TGS_Constants.currentcar == i)
            {
              // acc[i].gameObject.SetActive(true);
               cars[i].gameObject.SetActive(true);
               
            }
            else
            {
            //  acc[i].gameObject.SetActive(false);
              cars[i].gameObject.SetActive(false);
             
            }

        }
 

		CheckCarUnlock ();
	//	UIManager.Instance.btnsound ();
//		buybutton.transform.GetChild (0).GetComponent<Text> ().text = carsprice [TGS_Constants.currentcar].ToString ();

	}

	public void Left()
	{
		TGS_Constants.currentcar--;
		if (TGS_Constants.currentcar < 0)
			TGS_Constants.currentcar = (cars.Length-1);
        for (int i = 0; i < cars.Length; i++)
        {
            if (TGS_Constants.currentcar == i)
            {
               cars[i].gameObject.SetActive(true);
              //  acc[i].gameObject.SetActive(true);
            }

            else { 
                cars[i].gameObject.SetActive(false);
          //   acc[i].gameObject.SetActive(false);
        }

        }
     

		CheckCarUnlock ();
		UIManager.Instance.btnsound ();
//		buybutton.transform.GetChild (0).GetComponent<Text> ().text = carsprice [TGS_Constants.currentcar].ToString ();

	}

	public void Next()
	{

		//UIManager.InstantitatePrefab (nextObjectToInstantiate);
		UIManager.Instance.btnsound ();

		//Destroy (this.transform.root.gameObject);
	}

	public void Back()
	{
		menupanel.SetActive (true);
		bikeselection.SetActive (false);
		bikes.SetActive (false);
	//	bikeselectiontitle.SetActive (false);
	//	mainmenutitle.SetActive (true);
	}

	//public void touchbegin()
	//{
	//	pos	 = ControlFreak2.CF2Input.mousePosition.x;
	//}



	public void RotateVehicles()
	{
//		if (pos==0) 


		//float rotx = ControlFreak2.CF2Input.GetAxis ("Mouse X") * 3;

		//cars [TGS_Constants.currentcar].transform.Rotate (Vector3.down * rotx); 



	}


	void CheckCarUnlock()
	{
		if (PlayerPrefs.GetInt ("unlockcar" + TGS_Constants.currentcar) == TGS_Constants.currentcar)
        {

			if (buybutton.activeSelf) {
				buybutton.SetActive (false);
			}
			if (lockbutton.activeSelf) {
				lockbutton.SetActive (false);
			}
			if (!nextbutton.activeSelf) {
				nextbutton.SetActive (true);
			}
			if (scorebg.activeSelf) {
				scorebg.SetActive (false);
			}
		} else {
			if (!buybutton.activeSelf) {
				buybutton.SetActive (true);
			}
			if (!lockbutton.activeSelf)
				lockbutton.SetActive (true);
			if (nextbutton.activeSelf)
				nextbutton.SetActive (false);
			if (!scorebg.activeSelf) {
				scorebg.SetActive (true);
			}
		}
	
		if (scorebg.activeSelf)
			scorebg.transform.GetChild (0).GetComponent<Text> ().text ="$" + carsprice [TGS_Constants.currentcar].ToString ();

	}

	public void Buy()
	{
		if (PlayerPrefs.GetInt ("score") >= carsprice [TGS_Constants.currentcar]) {
			PlayerPrefs.SetInt ("unlockcar" + TGS_Constants.currentcar, TGS_Constants.currentcar);
			PlayerPrefs.SetInt ("score", PlayerPrefs.GetInt ("score") - carsprice [TGS_Constants.currentcar]);
			score.text = PlayerPrefs.GetInt ("score").ToString();
			UIManager.Instance.btnsound ();

		}
		CheckCarUnlock ();

	}

	//public void SpeedUpgradation()
	//{
	//	if ((PlayerPrefs.GetInt ("score") >= 100) && (PlayerPrefs.GetInt("unlockcar"+TGS_Constants.currentcar)==TGS_Constants.currentcar)) {
	//		if (speedslider.value < speedlimit[TGS_Constants.currentcar]) {
				
	//			speedslider.value += 0.1f;
	//			PlayerPrefs.SetFloat ("speedbike"+TGS_Constants.currentcar, speedslider.value);
	//			PlayerPrefs.SetInt ("score", PlayerPrefs.GetInt ("score") -100);
	//			score.text = PlayerPrefs.GetInt ("score").ToString();
	//		}

	//	}
//	}

	//public void BrakeUpgradation()
	//{
	//	if ((PlayerPrefs.GetInt ("score") >= 100) &&  (PlayerPrefs.GetInt("unlockcar"+TGS_Constants.currentcar)==TGS_Constants.currentcar)) {
	//		if (brakeslider.value < brakelimit[TGS_Constants.currentcar]) {
	//			brakeslider.value += 0.1f;
	//			PlayerPrefs.SetFloat ("brakebike"+TGS_Constants.currentcar, brakeslider.value);
	//			PlayerPrefs.SetInt ("score", PlayerPrefs.GetInt ("score") -100);
	//			score.text = PlayerPrefs.GetInt ("score").ToString();
	//		}

	//	}

	//}

	//public void checkupgradation()
	//{
	//	speedslider.value = PlayerPrefs.GetFloat ("speedbike"+TGS_Constants.currentcar);
	//	brakeslider.value = PlayerPrefs.GetFloat ("brakebike"+TGS_Constants.currentcar);

	//}

	public void LoadScene()
	{
		TGS_Constants.scenetoload = 1;
		UIManager.SceneToLoad ();

	}

	//public void skins(int currentskin)
	//{
	//	TGS_Constants.currentskin = currentskin;
	//	carcolor [TGS_Constants.currentcar].mainTexture = skinstextures [TGS_Constants.currentskin-1];
	//}
}
