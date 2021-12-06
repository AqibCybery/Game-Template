using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGS_MainMenu : MonoBehaviour
{
	[Header("References")]
	public PanelObj[] MenuPanels;
	public Text Scoretxt;   
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
	public void MoreGAmes()
	{
		Application.OpenURL("https://play.google.com/store/apps/dev?id=7212013146777561056");
	}



	public void privacylink()
	{

		Application.OpenURL("https://docs.google.com/document/d/e/2PACX-1vTOfmbKQVmnkL0Q9EMd-K1fAiHYJO63-HCsUN3Uh0jtwrnfBsSvn_MQu_dh4Xzgk1-RkwCEkJZpzV4o/pub");

	}
}
