using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer MasterMixer;

    public Slider mainSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnMainSliderChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMainSliderChange()
    {
        //Start with the slider value (Running 0 - 1)
        float newVolume = mainSlider.value;

        //If 0, default the audio to -80, essentially mute
        if (newVolume <= 0)
        {
            newVolume = -80;
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);

            newVolume *= 20;
        }

        //Set the new volume
        MasterMixer.SetFloat("MainVolume", newVolume);
    }

    public void OnMusicSliderChange()
    {
        //Start with the slider value (Running 0 - 1)
        float newVolume = musicSlider.value;

        //If 0, default the audio to -80, essentially mute
        if (newVolume <= 0)
        {
            newVolume = -80;
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);

            newVolume *= 20;
        }

        //Set the new volume
        MasterMixer.SetFloat("MusicVolume", newVolume);
    }

    public void OnSFXSliderChange()
    {
        //Start with the slider value (Running 0 - 1)
        float newVolume = sfxSlider.value;

        //If 0, default the audio to -80, essentially mute
        if (newVolume <= 0)
        {
            newVolume = -80;
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);

            newVolume *= 20;
        }

        //Set the new volume
        MasterMixer.SetFloat("SFXVolume", newVolume);
    }
}
