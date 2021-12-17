using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelSelection : MonoBehaviour {
	public GameObject LevelbuttonsParent;
	Button[] buttons;
	private void Awake()
	{
	TGS_Constants.CurrentScene = ScenesList.LevelSelection;
	}
	void Start () {
		buttons = new Button[LevelbuttonsParent.transform.childCount];
		for (int i = 0; i < (LevelbuttonsParent.transform.childCount); i++)
		{
			buttons[i] =LevelbuttonsParent.transform.GetChild(i).transform.gameObject.GetComponent<Button>();
			if (i <= PlayerPrefs.GetInt("unlocklevels1"))
			{
				buttons[i].interactable = true;
				buttons[i].transform.GetChild(0).gameObject.SetActive(false);
			}
		}		
	}

	public void SelectLevel(int levelno)
	{
		TGS_Constants.currentlevel = levelno;
		SceneManager.LoadScene("GamePlay");
	}
}
