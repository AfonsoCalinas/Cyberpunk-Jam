using UnityEngine;
using UnityEngine.UI;

public class InputHighlighter : MonoBehaviour
{
    [Header("Buttons")]
    public Button _topLeft;
    public Button _topMiddle;
    public Button _topRight;
    public Button _midLeft;
    public Button _midMiddle;
    public Button _midRight;
    public Button _bottomLeft;
    public Button _bottomMiddle;
    public Button _bottomRight;

    private Button _lastHighlighted;

    private Note _activeNote;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(h, v);

        Button toHighlight = GetButtonForInput(input);

        if (toHighlight != _lastHighlighted)
        {
            ResetAllButtons();
            HighlightButton(toHighlight);
            _lastHighlighted = toHighlight;
        }

        /* if (_activeNote != null && GetInputDirection() == _activeNote.direction)
        {
            GameManager._instance.NoteHit();
            Destroy(_activeNote.gameObject);
            _activeNote = null;
        } */
    }

    Button GetButtonForInput(Vector2 input)
    {
        if (input == Vector2.zero) return _midMiddle;

        if (input.x == -1 && input.y == 1) return _topLeft;
        if (input.x == 0 && input.y == 1) return _topMiddle;
        if (input.x == 1 && input.y == 1) return _topRight;
        if (input.x == -1 && input.y == 0) return _midLeft;
        if (input.x == 1 && input.y == 0) return _midRight;
        if (input.x == -1 && input.y == -1) return _bottomLeft;
        if (input.x == 0 && input.y == -1) return _bottomMiddle;
        if (input.x == 1 && input.y == -1) return _bottomRight;

        return _midMiddle;
    }

    string GetInputDirection()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h == -1 && v == 1) return "WA";
        if (h == 1 && v == 1) return "WD";
        if (h == -1 && v == -1) return "SA";
        if (h == 1 && v == -1) return "SD";
        if (h == -1) return "A";
        if (h == 1) return "D";
        if (v == 1) return "W";
        if (v == -1) return "S";

        return "";
    }

    void HighlightButton(Button btn)
    {
        if (btn != null)
        {
            ColorBlock cb = btn.colors;
            cb.normalColor = Color.green;
            btn.colors = cb;
        }
    }

    void ResetAllButtons()
    {
        Button[] allButtons = {
            _topLeft, _topMiddle, _topRight,
            _midLeft, _midMiddle, _midRight,
            _bottomLeft, _bottomMiddle, _bottomRight
        };

        foreach (Button btn in allButtons)
        {
            if (btn != null)
            {
                ColorBlock cb = btn.colors;
                cb.normalColor = Color.white;
                btn.colors = cb;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
            _activeNote = other.GetComponent<Note>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
            _activeNote = null;
    }
}
