using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class BeatManager : MonoBehaviour
{
    //[Header("Audio Settings")]
    //[SerializeField] private float bpm;
    // [SerializeField] private AudioSource music;
    
    private float _bpm;

    //public AudioSource music;
    private AudioSource _audioSource;

    [Header("Beat Intervals")]
    [SerializeField] private Intervals[] intervals;

    private void Start()
    {
        _audioSource = FindAnyObjectByType<AudioSource>();
        _bpm = LevelSettings.GetBpmForCurrentLevel();
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (Intervals interval in intervals)
        {
            // Time in Beats
            float sampledTime = (_audioSource.timeSamples / (_audioSource.clip.frequency * interval.GetBeatLenght(_bpm)));
            interval.CheckForNewInterval(sampledTime);
        }
    }
    
}

[System.Serializable]
public class Intervals
{
    [Tooltip("Steps means subdivisions: 1 = whole note, 2 = half notes, 4 = quarter notes, etc.")]
    [SerializeField] private float steps = 0.25f;

 
    [Tooltip("What should happen on this interval?")]
    [SerializeField] private UnityEvent trigger;
    private int _lastInterval;
    public void AddListener(UnityAction action) => trigger.AddListener(action);
    public void RemoveListener(UnityAction action) => trigger.RemoveListener(action);

    // Lenght of our current beat
    public float GetBeatLenght(float bpm)
    {
        return 60f / (bpm * steps);
    }
    
    // Have we crossed a new beat or not
    public void CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) == _lastInterval) return;
        _lastInterval = Mathf.FloorToInt(interval);
        trigger.Invoke();
    }
}