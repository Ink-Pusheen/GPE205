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
    public AudioSource audioSource { private get; set; }

    [SerializeField] SoundArray[] soundArray;

    public void PlaySoundOneShot(AudioSource audioPlayer, int chosenSoundArray, int chosenSoundIndex)
    {
        AudioClip chosenClip = soundArray[chosenSoundArray].soundClips[chosenSoundIndex];

        audioPlayer.PlayOneShot(chosenClip);
    }

    public void PlayRandomSoundFromArray(AudioSource audioPlayer, int chosenSoundArray)
    {
        int maxRandom = soundArray[chosenSoundArray].soundClips.Length;
        AudioClip chosenClip = soundArray[chosenSoundArray].soundClips[UnityEngine.Random.Range(0, maxRandom)];

        audioPlayer.PlayOneShot(chosenClip);
    }
}


