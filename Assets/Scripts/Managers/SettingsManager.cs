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

    [SerializeField] float mainVolume; //Float values respective 0 - 1f
    [SerializeField] float musicVolume;
    [SerializeField] float sfxVolume;

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

        mainVolume = savedSettings.savedMainVolume;
        mainSlider.value = mainVolume;

        musicVolume = savedSettings.savedMusicVolume;
        musicSlider.value = musicVolume;

        sfxVolume = savedSettings.savedSFXVolume;
        sfxSlider.value = sfxVolume;

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
