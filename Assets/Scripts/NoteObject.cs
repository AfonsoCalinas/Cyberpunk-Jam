using UnityEngine;
using UnityEngine.Serialization;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
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

        if (canBePressed && InputDirectionManager.GetDirectionName() == _direction)
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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_activatorTag))
        {
            canBePressed = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(_activatorTag)) return;
        canBePressed = false;
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