using TMPro;
using UnityEngine;

public class PopText : MonoBehaviour
{
    public float lifetime = 2f;
    public float moveSpeed = 20f;

    private TextMeshProUGUI _tmp;
    private Color _startColor;
    private float _elapsed;

    private void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        _startColor = _tmp.color;
    }

    private void Update()
    {
        _elapsed += Time.deltaTime;
        transform.Translate(Vector3.up * (moveSpeed * Time.deltaTime));
        var alpha = Mathf.Lerp(_startColor.a, 0, _elapsed / lifetime);
        _tmp.color = new Color(_startColor.r, _startColor.g, _startColor.b, alpha);
        if (_elapsed >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
