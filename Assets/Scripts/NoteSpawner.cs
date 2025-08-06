using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BeatManager _beatManager;
    [SerializeField] private GameObject[] _leftNotePrefabs;
    [SerializeField] private GameObject[] _middleNotePrefabs;
    [SerializeField] private GameObject[] _rightNotePrefabs;
    [SerializeField] private RectTransform[] _spawnPoints;
    [SerializeField] private RectTransform _parentCanvas;

    public void SpawnNote()
    {
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

        var noteSpeed = GetSpeedForCurrentLevel();

        var noteScript = note.GetComponent<NoteObject>();
        if (noteScript != null)
        {
            noteScript.speed = noteSpeed;
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
}
