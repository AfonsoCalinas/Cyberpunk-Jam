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

        // Pick random prefab type/lane
        int nLane = Random.Range(0, _spawnPoints.Length);

        if (nLane == 0)
        {
            int nNote = Random.Range(0, _leftNotePrefabs.Length);

            GameObject note = Instantiate(_leftNotePrefabs[nNote], _parentCanvas);

            RectTransform noteRect = note.GetComponent<RectTransform>();
            noteRect.anchoredPosition = _spawnPoints[nLane].anchoredPosition;

            string sceneName = SceneManager.GetActiveScene().name;
            float noteSpeed = GetSpeedForScene(sceneName);

            NoteObject noteScript = note.GetComponent<NoteObject>();
            if (noteScript != null)
            {
                noteScript.speed = noteSpeed;
            }
        }
        else if (nLane == 1)
        {
            int nNote = Random.Range(0, _middleNotePrefabs.Length);

            GameObject note = Instantiate(_middleNotePrefabs[nNote], _parentCanvas);

            RectTransform noteRect = note.GetComponent<RectTransform>();
            noteRect.anchoredPosition = _spawnPoints[nLane].anchoredPosition;

            string sceneName = SceneManager.GetActiveScene().name;
            float noteSpeed = GetSpeedForScene(sceneName);

            NoteObject noteScript = note.GetComponent<NoteObject>();
            if (noteScript != null)
            {
                noteScript.speed = noteSpeed;
            }
        }
        else
        {
            int nNote = Random.Range(0, _rightNotePrefabs.Length);

            GameObject note = Instantiate(_rightNotePrefabs[nNote], _parentCanvas);

            RectTransform noteRect = note.GetComponent<RectTransform>();
            noteRect.anchoredPosition = _spawnPoints[nLane].anchoredPosition;

            string sceneName = SceneManager.GetActiveScene().name;
            float noteSpeed = GetSpeedForScene(sceneName);

            NoteObject noteScript = note.GetComponent<NoteObject>();
            if (noteScript != null)
            {
                noteScript.speed = noteSpeed;
            }
        }
    }
    
    private float GetSpeedForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Level1Scene":
                return 200f;
            case "Level2Scene":
                return 250f;
            case "Level3Scene":
                return 300f;
            case "Level4Scene":
                return 350f;
            default:
                return 200f;
        }
    }
}
