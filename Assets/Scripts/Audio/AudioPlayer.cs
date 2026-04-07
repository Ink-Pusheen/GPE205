using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    public AudioplayerLogic AudioLogic;

    [SerializeField] AudioSource SFXMixer; //Mixer 0
    [SerializeField] AudioSource MusicMixer; //Mixer 1

    public void PlaySoundOneShot(int chosenMixer, int soundArray, int soundIndex)
    {
        AudioLogic.PlaySoundOneShot(ReturnMixer(chosenMixer), soundArray, soundIndex);
    }

    private AudioSource ReturnMixer(int chosenMixer)
    {
        if (chosenMixer == 0) return SFXMixer;

        else return MusicMixer;
    }
}
