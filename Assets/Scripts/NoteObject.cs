using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool _canBePressed;
    private bool _hasBeenPressed = false;
    public string _activatorTag = "Activator";

    [Header("Movement Settings")]
    [SerializeField] public float _speed = 50f; // Units per second
    public string _direction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move upward
        transform.Translate(_speed * Time.deltaTime * Vector3.up, Space.World);

        // string inputDir = GetInputDirection();

        if (_canBePressed && InputDirectionManager.GetDirectionName() == _direction)
        {
            if (GameManager._instance != null)
            {
                GameManager._instance.NoteHit();
            
                if (GameManager._instance.particleEffectController != null)
                {
                    GameManager._instance.particleEffectController.PlayParticlesForDirection(_direction);
                }
            }
            else
            {
                TutorialManager._instance.NoteHit();
            
                if (TutorialManager._instance.particleEffectController != null)
                {
                    TutorialManager._instance.particleEffectController.PlayParticlesForDirection(_direction);
                }
            }

            
            _hasBeenPressed = true;
            gameObject.SetActive(false);
        }
    }


    /*private string GetInputDirection()
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
    }*/

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
                if (GameManager._instance != null)
                {
                    GameManager._instance.NoteMissed();
                }
                else
                {
                    TutorialManager._instance.NoteMissed();
                }

            }
            
        }
    }
}