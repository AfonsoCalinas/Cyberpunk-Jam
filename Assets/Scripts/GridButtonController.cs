using UnityEngine;
using UnityEngine.UI;

public class GridButtonController : MonoBehaviour
{
    public KeyCode[] _keyCombination;
    public Color _normalColor = Color.white;
    public Color _pressedColor = Color.gray;

    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        if (AreKeysPressed())
            _image.color = _pressedColor;
        else
            _image.color = _normalColor;
    }

    private bool AreKeysPressed()
    {
        foreach (KeyCode key in _keyCombination)
        {
            if (!Input.GetKey(key))
            {
                return false;
            }
        }
        return true;
    }
}