using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BeatManager _beatManager;
    [SerializeField] private GameObject[] _arrowPrefabs;   // UI arrow prefabs
    [SerializeField] private RectTransform[] _spawnPoints; // UI spawn points
    [SerializeField] private RectTransform _parentCanvas;  // The Canvas we're spawning the arrow prefabs to hit

    public void SpawnArrow()
    {
        // Pick random arrow type/lane
        int lane = Random.Range(0, _arrowPrefabs.Length);

        // Instantiate under the Canvas so it's visible
        GameObject arrow = Instantiate(_arrowPrefabs[lane], _parentCanvas);

        // Set its anchored position to match the spawn point
        RectTransform arrowRect = arrow.GetComponent<RectTransform>();
        arrowRect.anchoredPosition = _spawnPoints[lane].anchoredPosition;
    }
}
