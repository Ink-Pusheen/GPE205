using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    public float noiseVolume = 0;

    public float decayRate = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (noiseVolume == 0) return;

        if(noiseVolume > 0) noiseVolume -= decayRate * Time.deltaTime;

        else if(noiseVolume < 0) noiseVolume = 0;
    }
}
