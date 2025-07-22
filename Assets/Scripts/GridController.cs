using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{

    public Button _arrowLeft;
    public Button _arrowDown;
    public Button _arrowUp;
    public Button _arrowRight;

    public Color normalColor = Color.white;
    public Color pressedColor = Color.gray;

    private Image _arrowLeftImage;
    private Image _arrowDownImage;
    private Image _arrowUpImage;
    private Image _arrowRightImage;


    void Start ()
    {
        _arrowLeftImage = _arrowLeft.GetComponent<Image>();
        _arrowDownImage = _arrowDown.GetComponent<Image>();
        _arrowUpImage = _arrowUp.GetComponent<Image>();
        _arrowRightImage = _arrowRight.GetComponent<Image>();
    }

    void Update ()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");

        // Horizontal
        // Left Arrow
        if (_horizontal < 0)
            _arrowLeftImage.color = pressedColor;
        else
            _arrowLeftImage.color = normalColor;
        // Right Arrow
        if (_horizontal > 0)
            _arrowRightImage.color = pressedColor; 
        else
            _arrowRightImage.color = normalColor;

        // Vertical
        // Down Arrow
        if (_vertical < 0)
            _arrowDownImage.color = pressedColor;
        else
            _arrowDownImage.color = normalColor;
        // Up Arrow
        if (_vertical > 0)
            _arrowUpImage.color = pressedColor;
        else
            _arrowUpImage.color = normalColor;
    }

}
