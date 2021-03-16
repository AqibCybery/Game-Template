using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGS_MainMenu : MonoBehaviour
{

	#region OldCode
	public string objectToInstantiate;
	public string objectToDestroy;
	public string menuToDisable;
	AudioSource bgmusic;
	public GameObject Menuscene;
	public GameObject musicbtn, soundbtn;
	public Sprite musicon, musicoff, soundon, soundoff;
	public GameObject bikeselection, levelselection;
	public GameObject mainmenu; public GameObject privacy, menu;
	public GameObject mainmenutitle, bikeselectiontitle;
   
    // Use this for initialization
    void Start()
	{
		Time.timeScale = 1;
		//if (PlayerPrefs.GetInt("privacy") == 0)
		//{
		//	privacy.SetActive(true);
		//	// menu.SetActive(false);
		//}
		////		PlayerPrefs.DeleteAll ();
		////   bgmusic = GameObject.FindGameObjectWithTag ("menumusic").GetComponent<AudioSource> ();
		//if (PlayerPrefs.GetInt("checkvolume") == 0)
		//{
		//	PlayerPrefs.SetInt("music", 0);
		//	PlayerPrefs.SetInt("Sound", 0);
		//	PlayerPrefs.SetInt("checkvolume", 1);
		//}
		////
		////mainmenutitle.SetActive(true);
		//GetComponent<Canvas>().worldCamera = Camera.main;
		//Time.timeScale = 1f;

		//if (PlayerPrefs.GetInt("music") == 0)
		//{
		//	Debug.Log("volume1");
		//	bgmusic.volume = 1f;
		//	musicbtn.GetComponent<Image>().sprite = musicon;

		//}
		//else if (PlayerPrefs.GetInt("music") == 1)
		//{
		//	Debug.Log("volume0");

		//	bgmusic.volume = 0f;
		//	musicbtn.GetComponent<Image>().sprite = musicoff;

		//}

		//if (PlayerPrefs.GetInt("Sound") == 0)
		//{
		//	Debug.Log("audio0");

		//	AudioListener.volume = 1f;
		//	soundbtn.GetComponent<Image>().sprite = soundon;

		//}
		//else if (PlayerPrefs.GetInt("Sound") == 1)
		//{
		//	Debug.Log("audio1");

		//	AudioListener.volume = 0f;
		//	soundbtn.GetComponent<Image>().sprite = soundoff;

		//}
		//		Debug.Log ("ad");
		//		AdsCallerManager.Instance.RequestBanner ();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void missionSelection()
	{
		UIManager.Instance.btnsound();
		levelselection.SetActive(true);


	}

	public void waponSelection()
	{
		UIManager.Instance.btnsound();
		bikeselection.SetActive(true);
		mainmenu.SetActive(false);
		//mainmenutitle.SetActive(false);
		//	bikeselectiontitle.SetActive (true);
		//		UIManager.InstantitatePrefab (objectToInstantiate);

		//		Destroy (this.gameObject);
	}

	public void Quit()
	{

		Application.Quit();
	}

	public void Music()
	{

		if (PlayerPrefs.GetInt("music") == 0)
		{
			bgmusic.volume = 0f;
			musicbtn.GetComponent<Image>().sprite = musicoff;
			PlayerPrefs.SetInt("music", 1);
		}
		else if (PlayerPrefs.GetInt("music") == 1)
		{

			bgmusic.volume = 1f;
			musicbtn.GetComponent<Image>().sprite = musicon;
			PlayerPrefs.SetInt("music", 0);

		}
		UIManager.Instance.btnsound();

	}
	public void Sound()
	{

		if (PlayerPrefs.GetInt("Sound") == 0)
		{
			AudioListener.volume = 0f;
			soundbtn.GetComponent<Image>().sprite = soundoff;
			PlayerPrefs.SetInt("Sound", 1);


		}
		else if (PlayerPrefs.GetInt("Sound") == 1)
		{
			AudioListener.volume = 1f;
			soundbtn.GetComponent<Image>().sprite = soundon;
			PlayerPrefs.SetInt("Sound", 0);

		}
		UIManager.Instance.btnsound();

	}


	public void MoreGAmes()
	{
		Application.OpenURL("https://play.google.com/store/apps/dev?id=7212013146777561056");
	}

	public void Accept()
	{

		privacy.SetActive(false);
		PlayerPrefs.SetInt("privacy", 1);
		menu.SetActive(true);

	}

	public void privacylink()
	{

		Application.OpenURL("https://docs.google.com/document/d/e/2PACX-1vTOfmbKQVmnkL0Q9EMd-K1fAiHYJO63-HCsUN3Uh0jtwrnfBsSvn_MQu_dh4Xzgk1-RkwCEkJZpzV4o/pub");

	}
	#endregion
	[Header("References")]
	public PanelObj[] MenuPanels;
	public Text Scoretxt;
    public int experience;
    public int Level
    {
        get { return experience / 750; }
    }
	private void OnEnable()
	{
		TGS_Constants.CurrentScene = ScenesList.MainMenu;
		UIManager.Instance.UIPanels.Clear();
		foreach (PanelObj panel in MenuPanels)
			UIManager.Instance.UIPanels.Add(panel);
		UIManager.Instance.Score = Scoretxt;
		Scoretxt.text = StoreData.LoadAsObj().Score.ToString();
	}
	public void ShowRewardVideoAd()
    {
		AdsController.instance.ShowRewardedAd();
    }
	public void PurchaseNoAds()
    {
		PlayerPrefs.SetInt("NoAds", 1);
		//UnityInAppsIntegration.THIS.BuyNonConsumable();
	}
	public void ClearPlayerPref()
    {
        PlayerPrefs.DeleteAll();
    }
	
}
