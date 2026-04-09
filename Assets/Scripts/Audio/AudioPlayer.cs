using UnityEngine;
using UnityEngine.Audio;

public class test
{
    public int num1;
    int num2;
}

public class AudioPlayer : MonoBehaviour
{
    public AudioplayerLogic AudioLogic;

    [SerializeField] AudioSource SFXMixer; //Mixer 0
    [SerializeField] AudioSource MusicMixer; //Mixer 1

    public void PlaySoundOneShot(int chosenMixer, int soundArray, int soundIndex)
    {
        AudioLogic.PlaySoundOneShot(ReturnMixer(chosenMixer), soundArray, soundIndex);
    }

    public void PlayRandomSoundFromArray(int chosenMixer, int soundArray)
    {
        AudioLogic.PlayRandomSoundFromArray(ReturnMixer(chosenMixer), soundArray);
    }

    public void MenuClick()
    {
        AudioLogic.PlayRandomSoundFromArray(ReturnMixer(0), 0);
    }

    private AudioSource ReturnMixer(int chosenMixer)
    {
        if (chosenMixer == 0) return SFXMixer;

        else return MusicMixer;
    }
}
