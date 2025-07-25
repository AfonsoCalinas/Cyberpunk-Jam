using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool _canBePressed;
    private bool _hasBeenPressed = false;
    public string _direction;
    public string _activatorTag = "Activator";
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 50f; // Units per second

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.up, Space.World);

        string inputDir = GetInputDirection();

        if (_canBePressed && inputDir == _direction)
        {
            GameManager._instance.NoteHit();
            _hasBeenPressed = true;
            gameObject.SetActive(false);
        }
    }

    private string GetInputDirection()
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_activatorTag))
        {
            _canBePressed = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
         if (other.CompareTag(_activatorTag))
        {
            _canBePressed = false;
            if(!_hasBeenPressed){
                GameManager._instance.NoteMissed();
            }
            
        }
    }
}