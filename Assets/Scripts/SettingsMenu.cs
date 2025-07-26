using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume (float volume) {
        float volumeDb = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("volume", volumeDb);
    }

}