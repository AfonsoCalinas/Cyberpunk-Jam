using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class NoteSpawner : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private BeatManager beatManager;
    [SerializeField] private GameObject[] _leftNotePrefabs;
    [SerializeField] private GameObject[] _middleNotePrefabs;
    [SerializeField] private GameObject[] _rightNotePrefabs;
    [SerializeField] private RectTransform[] _spawnPoints;
    [SerializeField] private RectTransform _parentCanvas;
    private AudioSource _music;
    private bool _isClosing;
    private float _trim;
    private float _noteSpeed;

    private void Start()
    {
        _music = beatManager.music;
        _trim = GetEndingForCurrentLevel();
        _noteSpeed = GetSpeedForCurrentLevel();
    }

    private void Update()
    {
        if (!_music.clip) return;
        var delay = Mathf.Max(_music.clip.length - _trim, 0f);

        Invoke(nameof(StopSpawningNotes), delay);
    }
    
    public void SpawnNote()
    {
        if (_isClosing) return;
        var nLane = Random.Range(0, _spawnPoints.Length);
        var prefabArray = nLane switch
        {
            0 => _leftNotePrefabs,
            1 => _middleNotePrefabs,
            _ => _rightNotePrefabs
        };

        var nNote = Random.Range(0, prefabArray.Length);
        var note = Instantiate(prefabArray[nNote], _parentCanvas);

        var noteRect = note.GetComponent<RectTransform>();
        noteRect.anchoredPosition = _spawnPoints[nLane].anchoredPosition;

        

        var noteScript = note.GetComponent<NoteObject>();
        if (noteScript != null)
        {
            noteScript.speed = _noteSpeed;
        }
    }

    
    private static float GetSpeedForCurrentLevel()
    {
        var index = SceneManager.GetActiveScene().buildIndex;

        // Assuming Tutorial = 1, Level1 = 2, Level2 = 3, ...
        // Adjust these speeds as needed
        switch (index)
        {
            case 2: return 200f; // Level1
            case 3: return 250f; // Level2
            case 4: return 300f; // Level3
            case 5: return 350f; // Level4
            default: return 200f;
        }

    }
    
    private static float GetEndingForCurrentLevel()
    {
        var coda = SceneManager.GetActiveScene().buildIndex;

        
        // Assuming Tutorial = 1, Level1 = 2, Level2 = 3, ...
        // Adjust these speeds as needed
        switch (coda)
        {
            case 2: return 11f; // Level1
            case 3: return 10f; // Level2
            case 4: return 9f; // Level3
            case 5: return 8f; // Level4
            default: return 10f;
        }
        

    }
    
    public void StopSpawningNotes()
    {
        _isClosing = true;
    }
}
