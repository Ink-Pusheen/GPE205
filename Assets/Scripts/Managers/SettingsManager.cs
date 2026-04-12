using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    //Instance
    public static SettingsManager instance;

    //Audio Stuff
    public AudioMixer MasterMixer;

    public Slider mainSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    //Json
    private string settingsJsonPath;

    private void Awake()
    {
        //Load json
        settingsJsonPath = Application.persistentDataPath + "/" + "Settings.json";

        LoadJson();
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (File.Exists(settingsJsonPath))
        {
            UpdateMixers();
        }
    }

    private void UpdateMixers()
    {
        string json = File.ReadAllText(settingsJsonPath);
        SettingsSave savedSettings = JsonUtility.FromJson<SettingsSave>(json);

        float masterVol = 0;
        if (savedSettings.savedMainVolume == 0) masterVol = 0;
        else masterVol = Mathf.Log10(savedSettings.savedMainVolume) * 20;
        MasterMixer.SetFloat("MainVolume", masterVol);

        float musicVol = 0;
        if (savedSettings.savedMainVolume == 0) musicVol = 0;
        else musicVol = Mathf.Log10(savedSettings.savedMusicVolume) * 20;
        MasterMixer.SetFloat("MusicVolume", musicVol);

        float sfxVol = 0;
        if (savedSettings.savedMainVolume == 0) sfxVol = 0;
        else sfxVol = Mathf.Log10(savedSettings.savedSFXVolume) * 20;
        MasterMixer.SetFloat("SFXVolume", sfxVol);
    }

    public void LoadJson()
    {
        if (!File.Exists(settingsJsonPath))
        {
            mainSlider.value = 1;
            musicSlider.value = 1;
            sfxSlider.value = 1;

            return;
        }

        string json = File.ReadAllText(settingsJsonPath);
        SettingsSave savedSettings = JsonUtility.FromJson<SettingsSave>(json);

        mainSlider.value = savedSettings.savedMainVolume;

        musicSlider.value = savedSettings.savedMusicVolume;

        sfxSlider.value = savedSettings.savedSFXVolume;

        //Update the sliders
        OnMainSliderChange();
        OnMusicSliderChange();
        OnSFXSliderChange();
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

    public void SaveSettings()
    {
        SettingsSave savedSettings = new();

        savedSettings.savedMainVolume = mainSlider.value;
        savedSettings.savedMusicVolume = musicSlider.value;
        savedSettings.savedSFXVolume = sfxSlider.value;

        string json = JsonUtility.ToJson(savedSettings);
        File.WriteAllText(settingsJsonPath, json);
    }

    //Default to previously saved values
    public void CancelSaveSettings()
    {
        LoadJson();
    }
}
