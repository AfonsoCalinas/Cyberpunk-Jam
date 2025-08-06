using System;
using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider soundfxSlider;
    private SoundMixerManager _soundMixer;


    void Start()
    {
        // Get reference
        _soundMixer = FindAnyObjectByType<SoundMixerManager>();

        // Update slider visuals
        masterSlider.value = _soundMixer.GetSliderValue("masterVolume");
        musicSlider.value  = _soundMixer.GetSliderValue("musicVolume");
        soundfxSlider.value    = _soundMixer.GetSliderValue("soundFXVolume");

        // Optional: Hook slider value changes to update the mixer
        masterSlider.onValueChanged.AddListener(_soundMixer.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(_soundMixer.SetMusicVolume);
        soundfxSlider.onValueChanged.AddListener(_soundMixer.SetSoundFXVolume);
    }
}