using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHighlighter : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonDirection
    {
        public string direction;
        public Button button;
    }
    
    [Header("Directional Buttons")]
    [Tooltip("Directional Buttons list, where you can add direction strings like \"W\", \"WA\", etc., and drag in the corresponding Button objects.\n\nMake sure you also add a fallback mapping:\n\nDirection: \"Default\"\n\nButton: your default _midMiddle button")]
    public ButtonDirection[] buttonMappings;
    
    
    private Dictionary<string, Button> _buttonDict;
    

    private Button _lastHighlighted;
    private NoteObject _activeNote;

    [SerializeField] private AudioClip hitSound;
    private string _directionName;

    private void Awake()
    {
        // Initialize dictionary
        _buttonDict = new Dictionary<string, Button>();
        foreach (var mapping in buttonMappings)
        {
            if (!_buttonDict.ContainsKey(mapping.direction))
            {
                _buttonDict.Add(mapping.direction, mapping.button);
            }
        }
        /*_directionName = InputDirectionManager.GetDirectionName();*/
    }

    private void Update()
    {
        _directionName = InputDirectionManager.GetDirectionName();
        
        Button toHighlight = GetButtonForInput(_directionName);

        if (toHighlight != _lastHighlighted)
        {
            ResetAllButtons();
            HighlightButton(toHighlight);
            _lastHighlighted = toHighlight;
        }

        if (_activeNote && _directionName == _activeNote._direction)
        {
            GameManager._instance.NoteHit();
            Destroy(_activeNote.gameObject);
            Debug.Log("note destroyed");
            _activeNote = null;
        }
    }

    private Button GetButtonForInput(string directionName)
    {
        return _buttonDict.TryGetValue(directionName, out var btn) ? btn : _buttonDict.GetValueOrDefault("Default", null);

    }

    private void HighlightButton(Button btn)
    {
        if (btn)
        {
            ColorBlock cb = btn.colors;
            cb.normalColor = Color.black;
            btn.colors = cb;
        }

        // play sfx
        SoundFXManager.instance.PlaySoundFXClip(hitSound, transform, .1f);
    }

    private void ResetAllButtons()
    {
        foreach (Button btn in _buttonDict.Values)
        {
            if (btn)
            {
                ColorBlock cb = btn.colors;
                cb.normalColor = Color.gray;
                btn.colors = cb;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_activeNote == null && other.TryGetComponent(out NoteObject note) &&
            other.CompareTag(note.activatorTag))
        {
            _activeNote = note;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_activeNote != null && other.CompareTag(_activeNote.activatorTag))
        {
            _activeNote = null;

        }
    }
}

