using UnityEngine;
using System.Collections;

public class FloorTile : MonoBehaviour
{
    private static readonly int SwitchLight = Shader.PropertyToID("_SwitchLight");
    public Material tileMaterial;
    private Coroutine _currentCoroutine;
    
    private void Awake()
    {
        tileMaterial.SetFloat(SwitchLight, 1f); // Ensure light is off at start
    }


    public void ActivateTile(float maxValue = 1f, float duration = 0.3f)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(SmoothSwitch(maxValue, duration));
    }

    private IEnumerator SmoothSwitch(float targetValue, float duration)
    {
        var t = 0f;
        var startValue = 0f;

        // Switch on with Lerp: from 0 -> targetValue
        while (t < duration)
        {
            t += Time.deltaTime;
            var lerped = Mathf.Lerp(startValue, targetValue, t / duration);
            tileMaterial.SetFloat(SwitchLight, lerped);
            yield return null;
        }

        tileMaterial.SetFloat(SwitchLight, targetValue);

        // Wait a little with the light switched on
        yield return new WaitForSeconds(0.1f);

        // Switch off with Lerp: from targetValue -> 0
        t = 0f;
        startValue = targetValue;

        while (t < duration)
        {
            t += Time.deltaTime;
            var lerped = Mathf.Lerp(startValue, 0f, t / duration);
            tileMaterial.SetFloat(SwitchLight, lerped);
            yield return null;
        }

        tileMaterial.SetFloat(SwitchLight, 1f);
    }
}

