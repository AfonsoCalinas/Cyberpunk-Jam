using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BeatManager _beatManager;
    [SerializeField] private GameObject[] _leftNotePrefabs;   // UI note prefabs
    [SerializeField] private GameObject[] _middleNotePrefabs;   // UI note prefabs
    [SerializeField] private GameObject[] _rightNotePrefabs;   // UI note prefabs
    [SerializeField] private RectTransform[] _spawnPoints; // UI spawn points
    [SerializeField] private RectTransform _parentCanvas;  // The Canvas we're spawning the note prefabs to hit

    public void SpawnNote()
    {
        // Pick random prefab type/lane
        int nLane = Random.Range(0, _spawnPoints.Length);

        if (nLane == 0)
        {
            int nNote = Random.Range(0, _leftNotePrefabs.Length);

            // Instantiate under the Canvas so it's visible
            GameObject note = Instantiate(_leftNotePrefabs[nNote], _parentCanvas);

            // Set its anchored position to match the spawn point
            RectTransform noteRect = note.GetComponent<RectTransform>();
            noteRect.anchoredPosition = _spawnPoints[nLane].anchoredPosition;
        }
        else if (nLane == 1)
        {
            int nNote = Random.Range(0, _middleNotePrefabs.Length);

            // Instantiate under the Canvas so it's visible
            GameObject note = Instantiate(_middleNotePrefabs[nNote], _parentCanvas);

            // Set its anchored position to match the spawn point
            RectTransform noteRect = note.GetComponent<RectTransform>();
            noteRect.anchoredPosition = _spawnPoints[nLane].anchoredPosition;
        }
        else
        {
            int nNote = Random.Range(0, _rightNotePrefabs.Length);

            // Instantiate under the Canvas so it's visible
            GameObject note = Instantiate(_rightNotePrefabs[nNote], _parentCanvas);

            // Set its anchored position to match the spawn point
            RectTransform noteRect = note.GetComponent<RectTransform>();
            noteRect.anchoredPosition = _spawnPoints[nLane].anchoredPosition;
        }

        
    }
}
