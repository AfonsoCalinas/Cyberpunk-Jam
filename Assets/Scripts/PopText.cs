using TMPro;
using UnityEngine;

public class PopText : MonoBehaviour
{
    public float lifetime = 2f;
    public float moveSpeed = 20f;

    private TextMeshProUGUI tmp;
    private Color startColor;
    private float elapsed;

    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        startColor = tmp.color;
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        float alpha = Mathf.Lerp(startColor.a, 0, elapsed / lifetime);
        tmp.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        if (elapsed >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
