using System;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // spawn it gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        
        // assign audio clip
        audioSource.clip = audioClip;
        
        // assign volume
        audioSource.volume = volume;
        
        // play sound
        audioSource.Play();
        
        // get lenght of sound fx clip
        float clipLenght = audioSource.clip.length;
        
        // destroy the clip after it is done playing
        Destroy(audioSource.gameObject, clipLenght);
    }
}
