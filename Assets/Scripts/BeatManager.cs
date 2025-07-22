using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private float _bpm;
    [SerializeField] private AudioSource _audioSource;

    [Header("Beat Intervals")]
    [SerializeField] private Intervals[] _intervals;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Intervals interval in _intervals)
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
    [SerializeField] private float _steps;

    [Tooltip("What should happen on this interval?")]
    [SerializeField] private UnityEvent _trigger;
    private int _lastInterval;
    public void AddListener(UnityAction action) => _trigger.AddListener(action);
    public void RemoveListener(UnityAction action) => _trigger.RemoveListener(action);

    // Lenght of our current beat
    public float GetBeatLenght(float bpm)
    {
        return 60f / (bpm * _steps);
    }
    
    // Have we crossed a new beat or not
    public void CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != _lastInterval)
        {
            _lastInterval = Mathf.FloorToInt(interval);
            _trigger.Invoke();
        }
    }
}