using UnityEngine;

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
        }
        else if (nLane == 1)
        {
            int nNote = Random.Range(0, _middleNotePrefabs.Length);

            GameObject note = Instantiate(_middleNotePrefabs[nNote], _parentCanvas);

            RectTransform noteRect = note.GetComponent<RectTransform>();
            noteRect.anchoredPosition = _spawnPoints[nLane].anchoredPosition;
        }
        else
        {
            int nNote = Random.Range(0, _rightNotePrefabs.Length);

            GameObject note = Instantiate(_rightNotePrefabs[nNote], _parentCanvas);

            RectTransform noteRect = note.GetComponent<RectTransform>();
            noteRect.anchoredPosition = _spawnPoints[nLane].anchoredPosition;
        }

        
    }
}
