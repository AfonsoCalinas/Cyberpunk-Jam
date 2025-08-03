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
    private NoteObject _activeNote;

    [SerializeField] private AudioClip hitSound;

    void Update()
    {
        Vector2 input = InputDirectionManager.GetDirectionVector();
        Button toHighlight = GetButtonForInput(input);

        if (toHighlight != _lastHighlighted)
        {
            ResetAllButtons();
            HighlightButton(toHighlight);
            _lastHighlighted = toHighlight;
        }

        if (_activeNote != null && InputDirectionManager.GetDirectionName() == _activeNote._direction)
        {
            GameManager._instance.NoteHit();

            // destroy note
            Destroy(_activeNote.gameObject);
            _activeNote = null;
        }
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

    void HighlightButton(Button btn)
    {
        if (btn != null)
        {
            ColorBlock cb = btn.colors;
            cb.normalColor = Color.black;
            btn.colors = cb;
        }

        // play sfx
        SoundFXManager.instance.PlaySoundFXClip(hitSound, transform, .1f);
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
                cb.normalColor = Color.gray;
                btn.colors = cb;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_activeNote == null && other.TryGetComponent(out NoteObject note) &&
            other.CompareTag(note._activatorTag))
        {
            _activeNote = note;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_activeNote != null && other.CompareTag(_activeNote._activatorTag))
        {
            _activeNote = null;
        }
    }
}

