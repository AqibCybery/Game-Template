using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenehandler : MonoBehaviour {
	public static scenehandler Instance;
	public GameObject menuscene,mainmenu,privacypolicy;
	public GameObject garage;
	public GameObject menurotatepoint,menurotstartpoint;

	GameObject cam;
	public Transform[] points;
	public GameObject[] lookatpoints;
	void Awake()
	{
		Time.timeScale = 1;
		Instance = this;
	}


	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		if (PlayerPrefs.GetInt ("privacypolicy") == 0) {
			if (!privacypolicy.activeSelf)
				privacypolicy.SetActive (true);
			if (mainmenu.activeSelf)
				mainmenu.SetActive (false);

		}
		setcampos ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameObject.FindGameObjectWithTag("MainMenu")) {
			cam.transform.position =	Vector3.Lerp (cam.transform.position, points [TGS_Constants.currentObject].transform.position, 1f * Time.deltaTime);
			cam.transform.LookAt (lookatpoints [TGS_Constants.currentObject].transform);
		} else {

			cam.transform.RotateAround (menurotatepoint.transform.position, Vector3.down, 10f * Time.deltaTime);
		}
	}

	public void setcampos()
	{
		if (cam.transform.position != menurotstartpoint.transform.position)
			cam.transform.position = menurotstartpoint.transform.position;
	}

	public void enablemenuscene()
	{
		menuscene.SetActive (true);
	}
	public void disablemenuscene()
	{
		menuscene.SetActive (false);
	}
	public void enablegarage()
	{
		garage.SetActive (true);
	}
	public void disablegarage()
	{
		garage.SetActive (false);

	}
	public void Aceept()
	{
		PlayerPrefs.SetInt ("privacypolicy", 1);
		privacypolicy.SetActive (false);
		mainmenu.SetActive (true);

	}

}
