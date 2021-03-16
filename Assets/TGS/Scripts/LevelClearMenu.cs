using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelClearMenu : MonoBehaviour {
	public Text score;
	int add;
	public Save DataToSave;
	// Use this for initialization
	void Start () {

        Time.timeScale = 0;
		StartCoroutine ("Reward");
		PlayerPrefs.SetInt ("score", (PlayerPrefs.GetInt ("score") + TGS_Constants.reward));
		Debug.Log ("playerpre" + PlayerPrefs.GetInt ("score"));
		DataToSave = StoreData.LoadAsObj();
		if (DataToSave.TotalLevels > TGS_Constants.currentlevel && DataToSave.UnlockLevels < TGS_Constants.currentlevel)
			DataToSave.UnlockLevels += 1;
		StoreData.CreateSaveGameObject(DataToSave);
	    
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	IEnumerator Reward()
	{
		for (int i = 0; i < TGS_Constants.reward; i++) {
			if (add < TGS_Constants.reward) {
				add += 5;
				score.text = "Score  "  + add.ToString ()  ; 
			}
			yield return new WaitForSecondsRealtime (0.01f);

		}
	}
	public void Next()
	{
		if (TGS_Constants.currentlevel != 10) {
			TGS_Constants.currentlevel++;
			TGS_Constants.scenetoload = 1;
		} else {
			TGS_Constants.scenetoload = 0;
		}

	
        UIManager.InstantitatePrefab ("Loading");

		UIManager.SceneToLoad ();
		UIManager.Instance.btnsound ();
		
	}
	public void MainMenu()
	{

		TGS_Constants.scenetoload = 0;
		UIManager.InstantitatePrefab ("Loading");
		UIManager.SceneToLoad ();
	

		Destroy (this.gameObject);
        UIManager.Instance.btnsound();
    }


	public void Restart()
	{
		TGS_Constants.scenetoload = 1;

		UIManager.InstantitatePrefab ("Loading");

		UIManager.SceneToLoad ();
        UIManager.Instance.btnsound();
    }

}
