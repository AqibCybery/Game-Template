using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Utility;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectSelection : MonoBehaviour
{
    GameObject[] objects;
    public GameObject Objectssparent;
    public int[] carsprice;

    public GameObject buybutton, nextbutton, scorebg;
    public Text score;
    public GameObject lockbutton;

    private void Awake()
    {
        TGS_Constants.CurrentScene = ScenesList.ObjectSelection;

    }
    void Start()
    {
        objects = new GameObject[Objectssparent.transform.childCount];
        TGS_Constants.currentObject = 0;
        for (int i = 0; i < (Objectssparent.transform.childCount); i++)
        {

            objects[i] = Objectssparent.transform.GetChild(i).transform.gameObject;
            objects[TGS_Constants.currentObject].SetActive(true);
        }
        score.text = "$" + PlayerPrefs.GetInt("score").ToString();
        CheckObjectUnlock();
        buybutton.transform.GetChild(0).GetComponent<Text>().text = carsprice[TGS_Constants.currentObject].ToString();
    }

    public void Right()
    {
        TGS_Constants.currentObject++;
        if (TGS_Constants.currentObject == objects.Length)
            TGS_Constants.currentObject = 0;
        for (int i = 0; i < objects.Length; i++)
        {
            if (TGS_Constants.currentObject == i)
            {
                objects[i].gameObject.SetActive(true);
            }
            else
            {
                objects[i].gameObject.SetActive(false);
            }

        }


        CheckObjectUnlock();

    }

    public void Left()
    {
        TGS_Constants.currentObject--;
        if (TGS_Constants.currentObject < 0)
            TGS_Constants.currentObject = (objects.Length - 1);
        for (int i = 0; i < objects.Length; i++)
        {
            if (TGS_Constants.currentObject == i)
            {
                objects[i].gameObject.SetActive(true);
            }

            else
            {
                objects[i].gameObject.SetActive(false);
            }

        }


        CheckObjectUnlock();
        UIManager.Instance.btnsound();

    }

    public void Next()
    {

        UIManager.Instance.btnsound();

    }



    void CheckObjectUnlock()
    {
        if (PlayerPrefs.GetInt("unlockobject" + TGS_Constants.currentObject) == TGS_Constants.currentObject)
        {

            if (buybutton.activeSelf)
            {
                buybutton.SetActive(false);
            }
            if (lockbutton.activeSelf)
            {
                lockbutton.SetActive(false);
            }
            if (!nextbutton.activeSelf)
            {
                nextbutton.SetActive(true);
            }
            if (scorebg.activeSelf)
            {
                scorebg.SetActive(false);
            }
        }
        else
        {
            if (!buybutton.activeSelf)
            {
                buybutton.SetActive(true);
            }
            if (!lockbutton.activeSelf)
                lockbutton.SetActive(true);
            if (nextbutton.activeSelf)
                nextbutton.SetActive(false);
            if (!scorebg.activeSelf)
            {
                scorebg.SetActive(true);
            }
        }

        if (scorebg.activeSelf)
            scorebg.transform.GetChild(0).GetComponent<Text>().text = "$" + carsprice[TGS_Constants.currentObject].ToString();

    }

    public void Buy()
    {
        if (PlayerPrefs.GetInt("score") >= carsprice[TGS_Constants.currentObject])
        {
            PlayerPrefs.SetInt("unlockobject" + TGS_Constants.currentObject, TGS_Constants.currentObject);
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") - carsprice[TGS_Constants.currentObject]);
            score.text = PlayerPrefs.GetInt("score").ToString();
            UIManager.Instance.btnsound();

        }
        CheckObjectUnlock();

    }

    public void LoadScene()
    {
        TGS_Constants.scenetoload = 1;
        UIManager.SceneToLoad();

    }
}
