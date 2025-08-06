using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level)*20f );
    }
    
    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level)*20f);
    }
    
    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level)*20f);
    }
    
    public float GetSliderValue(string parameter)
    {
        if (audioMixer.GetFloat(parameter, out float dB))
        {
            return Mathf.Pow(10f, dB / 20f); // Convert dB back to [0.0001 - 1] linear
        }

        return 1f; // default full volume
    }

}
