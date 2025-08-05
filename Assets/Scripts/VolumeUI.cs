using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider soundfxSlider;
    private SoundMixerManager soundMixer;

    void Start()
    {
        // Get reference
        soundMixer = FindObjectOfType<SoundMixerManager>();

        // Update slider visuals
        masterSlider.value = soundMixer.GetSliderValue("masterVolume");
        musicSlider.value  = soundMixer.GetSliderValue("musicVolume");
        soundfxSlider.value    = soundMixer.GetSliderValue("soundFXVolume");

        // Optional: Hook slider value changes to update the mixer
        masterSlider.onValueChanged.AddListener(soundMixer.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(soundMixer.SetMusicVolume);
        soundfxSlider.onValueChanged.AddListener(soundMixer.SetSoundFXVolume);
    }
}