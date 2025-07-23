using UnityEngine;
using System.Collections;

public class FloorTile : MonoBehaviour
{
    public Material tileMaterial;
    private Coroutine currentCoroutine;

    public void ActivateTile(float maxValue = 1f, float duration = 0.3f)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(SmoothSwitch(maxValue, duration));
    }

    private IEnumerator SmoothSwitch(float targetValue, float duration)
    {
        float t = 0f;
        float startValue = 0f;

        // Switch on with Lerp: from 0 -> targetValue
        while (t < duration)
        {
            t += Time.deltaTime;
            float lerped = Mathf.Lerp(startValue, targetValue, t / duration);
            tileMaterial.SetFloat("_SwitchLight", lerped);
            yield return null;
        }

        tileMaterial.SetFloat("_SwitchLight", targetValue);

        // Wait a little with the light switched on
        yield return new WaitForSeconds(0.1f);

        // Switch off with Lerp: from targetValue -> 0
        t = 0f;
        startValue = targetValue;

        while (t < duration)
        {
            t += Time.deltaTime;
            float lerped = Mathf.Lerp(startValue, 0f, t / duration);
            tileMaterial.SetFloat("_SwitchLight", lerped);
            yield return null;
        }

        tileMaterial.SetFloat("_SwitchLight", 1f);
    }
}

