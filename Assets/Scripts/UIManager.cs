using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
    string _volume = "Volume";
    public AudioSource GameMusic;
    GameObject OptionsPanel;
    Slider VolumeSlider;
    void Awake()
    {
        GameMusic = GameObject.Find("AudioPlayer").GetComponent<AudioSource>();
        GameMusic.volume = PlayerPrefs.GetFloat(_volume);
        OptionsPanel = GameObject.Find("OptionsPanel");


        if (OptionsPanel != null)
        {
            VolumeSlider = OptionsPanel.GetComponentInChildren<Slider>();   
            VolumeSlider.value = PlayerPrefs.GetFloat(_volume);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        SetVolume();

        
    }

    void SetVolume()
    {
        if (VolumeSlider != null)
        {
            GameMusic.volume = VolumeSlider.value;
            PlayerPrefs.SetFloat(_volume, VolumeSlider.value);

        }
    }
}
