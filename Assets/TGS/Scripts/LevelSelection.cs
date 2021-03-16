using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelSelection : MonoBehaviour {
	public string backBtnInstantiate;
	public string nextBtnInstantiate;
	public Button[] buttons;
    public GameObject missionDetails;
	private void Awake()
	{
	TGS_Constants.CurrentScene = ScenesList.LevelSelection;

	}
	// Use this for initialization
	void Start () {
        missionDetails = GameObject.FindGameObjectWithTag("MissionDes");
//		PlayerPrefs.SetInt ("unlocklevels1", 14);
		for (int i = 0; i < buttons.Length; i++) {
			if (i <= PlayerPrefs.GetInt ("unlocklevels1")) {
				buttons [i].interactable = true;
				buttons [i].transform.GetChild (0).gameObject.SetActive (false);
			}
		}
	}

	public void SelectLevel(int levelno)
	{
		TGS_Constants.currentlevel = levelno;
        for (int i = 0; i < 10; i++)
        {
            missionDetails.transform.GetChild(i).gameObject.SetActive(false);
     
        }

        missionDetails.transform.GetChild(TGS_Constants.currentlevel - 1).gameObject.SetActive(true);


		//UIManager.InstantitatePrefab (nextBtnInstantiate);
		//UIManager.SceneToLoad ();
		//UIManager.Instance.btnsound ();

		//Destroy (this.gameObject);
	}
		
	// Update is called once per frame
	void Update () {
		
	}

	public void Back()
	{
        this.gameObject.SetActive(false);

		UIManager.Instance.btnsound ();
	}

	public void Next()
	{
        TGS_Constants.scenetoload = 1;
        UIManager.SceneToLoad();
        UIManager.InstantitatePrefab (nextBtnInstantiate);
		UIManager.Instance.btnsound ();

		Destroy (this.gameObject);
	}
}
