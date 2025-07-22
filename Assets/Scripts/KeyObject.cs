using UnityEngine;

public class KeyObject : MonoBehaviour
{
    public bool _canBePressed;
    public KeyCode _keyToPress;
    private bool _hasBeenPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_keyToPress))
        {
            if (_canBePressed)
            {
                GameManager._instance.NoteHit();
                _hasBeenPressed = true;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            _canBePressed = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Activator"))
        {
            _canBePressed = false;
            if(!_hasBeenPressed){
                GameManager._instance.NoteMissed();
            }
            
        }
    }
}
