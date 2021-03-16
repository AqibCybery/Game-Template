using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider SoundSlider, MusicSlider;
    AudioSource bgmusic;
    public Image musicON, musicOff, soundON, soundOff;

    // Start is called before the first frame update
    void Start()
    { 
        bgmusic = GameObject.FindGameObjectWithTag("menumusic").GetComponent<AudioSource>();
        Time.timeScale = 1f;
        if (PlayerPrefs.HasKey("music"))
        {
            bgmusic.volume = PlayerPrefs.GetInt("music");
            if (bgmusic.volume == 1)
                MusicOn();
            else
                MusicOff();
        }
        if (PlayerPrefs.HasKey("Sound"))
        {
            AudioListener.volume = PlayerPrefs.GetInt("Sound");
            if (AudioListener.volume == 1)
                SoundOn();
            else
                SoundOff();
        }
    }
    public void MusicOn()
    {
        bgmusic.volume = 1f;
        musicON.enabled = true;
        musicOff.enabled = false;
        PlayerPrefs.SetInt("music", 1);
        UIManager.Instance.btnsound();

    }
    public void MusicOff()
    {
        bgmusic.volume = 0f;
        musicON.enabled = false;
        musicOff.enabled = true;
        PlayerPrefs.SetInt("music", 0);
        UIManager.Instance.btnsound();

    }
    public void SoundOn()
    {
        AudioListener.volume = 1f;
        soundON.enabled = true;
        soundOff.enabled = false;
        PlayerPrefs.SetInt("Sound", 1);
        UIManager.Instance.btnsound();

    }
    public void SoundOff()
    {
        AudioListener.volume = 0f;
        soundON.enabled = false;
        soundOff.enabled = true;
        PlayerPrefs.SetInt("Sound", 0);
        UIManager.Instance.btnsound();

    }
    
     public void Close()
    {
        Destroy(this.gameObject);
    }
}
