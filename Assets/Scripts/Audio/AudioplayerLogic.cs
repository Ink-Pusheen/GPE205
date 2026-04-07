using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class SoundArray
{
    public string name; //name of array for the specified sounds
    public AudioClip[] soundClips; //Array of said sounds
}

[Serializable]
public class AudioplayerLogic
{
    [SerializeField] SoundArray[] soundArray;

    public void PlaySoundOneShot(AudioSource audioPlayer, int chosenSoundArray, int chosenSoundIndex)
    {
        AudioClip chosenClip = soundArray[chosenSoundArray].soundClips[chosenSoundIndex];

        audioPlayer.PlayOneShot(chosenClip);
    }
}


