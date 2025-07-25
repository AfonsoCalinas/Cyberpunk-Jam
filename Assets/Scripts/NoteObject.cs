using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool _canBePressed;
    public KeyCode[] _keysToPress;
    private bool _hasBeenPressed = false;
    public string activatorTag = "Activator";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AreKeysPressed())
        {
            if (_canBePressed)
            {
                GameManager._instance.NoteHit();
                _hasBeenPressed = true;
                gameObject.SetActive(false);
            }
        }
    }

    private bool AreKeysPressed()
    {
        // All keys in the array must be pressed for a hit
        foreach (KeyCode key in _keysToPress)
        {
            if (!Input.GetKey(key)) return false;
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(activatorTag))
        {
            _canBePressed = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag(activatorTag))
        {
            _canBePressed = false;
            if(!_hasBeenPressed){
                GameManager._instance.NoteMissed();
            }
            
        }
    }
}
