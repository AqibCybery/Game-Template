using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("UI Panels")]
    public List<PanelObj> UIPanels = new List<PanelObj>();
    public Text Score;
    //public	GameObject hudmenu,mainmenu,bikeselection,bikes,levelselection;
    //	public GameObject map;
    public AudioSource buttonsound;
    void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        Debug.Log("HI");
    }
    public void AddCoins(int Coins)
    {
        Save DataToSave = new Save();
        DataToSave = StoreData.LoadAsObj();
        DataToSave.Score += Coins;
        OnlineLogInManager.This.SetUserData(DataToSave);
        StoreData.CreateSaveGameObject(DataToSave);
    }

    public void Goto(ScenesList SelectedMenu)
    {
        SceneManager.LoadScene(SelectedMenu.ToString());
        AdsController.instance.ShowInterstitial();
    }
    public void OpenPanel(Panels SelectedMenu)
    {
       // Debug.Log(SelectedMenu.SelectedPanel.ToString());
        foreach (PanelObj p in UIPanels)
        {

            if (p.Name == SelectedMenu)
            {
                iTween.MoveFrom(p.PanelRef, p.PanelRef.transform.position + Vector3.left*10, 2);
                p.PanelRef.SetActive(true);
            }
        }
    }
    public void Next()
    {
        switch (TGS_Constants.CurrentScene)
        {
            case ScenesList.LevelSelection:
                SceneManager.LoadScene(ScenesList.GamePlay.ToString());
                break;
            case ScenesList.ObjectSelection:
                SceneManager.LoadScene(ScenesList.LevelSelection.ToString());
                break;
            case ScenesList.MainMenu:
                SceneManager.LoadScene(ScenesList.LevelSelection.ToString());
                break;

        }
    }
    public  void Back()
    {
        switch (TGS_Constants.CurrentScene)
        {
            case ScenesList.LevelSelection:
            case ScenesList.ObjectSelection:
                SceneManager.LoadScene(ScenesList.MainMenu.ToString());
                break;

        }

    }

    public static void InstantitatePrefab(string InstantiateObject)
    {
        //		Debug.Log (InstantiateObject);
        Instantiate(Resources.Load("Menus/" + InstantiateObject));
    }

    public IEnumerator InstantitatePrefabWithDelay(float Delay, string InstantiateObject)
    {
        Debug.Log(InstantiateObject);
        yield return new WaitForSeconds(Delay);
        InstantitatePrefab(InstantiateObject);
    }

    public static void EnableMenuusingtag(string enableObject)
    {
        GameObject.FindGameObjectWithTag(enableObject).SetActive(true);
    }

    public static void DisableMenuusingtag(string DisableObject)
    {
        GameObject.FindGameObjectWithTag(DisableObject).SetActive(false);
    }

    public static void EnableMenuusingname(string name)
    {
        GameObject.Find(name).transform.GetChild(0).gameObject.SetActive(true);
    }

    public static void DisableMenuusingname(string name)
    {
        GameObject.Find(name).transform.GetChild(0).gameObject.SetActive(false);

    }

    public void Enablehudmenu()
    {
        //if (!hudmenu.activeSelf)
        //	hudmenu.SetActive (true);
        //if (!map.activeSelf && (PlayerPrefs.GetInt("Mode")==2))
        //map.SetActive (true);
    }

    public void DisableHudmenu()
    {
        //if (hudmenu.activeSelf)
        //	hudmenu.SetActive (false);
        //if (map.activeSelf && (PlayerPrefs.GetInt("Mode")==2))
        //	map.SetActive (false);
    }

    public static void SceneToLoad()
    {
        SceneManager.LoadSceneAsync(TGS_Constants.scenetoload);
    }

    public void btnsound()
    {
        buttonsound.Play();
    }

}
