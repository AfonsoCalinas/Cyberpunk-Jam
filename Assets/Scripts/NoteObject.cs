using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    private bool _hasBeenPressed;
    public string activatorTag = "Activator";


    [Header("Movement Settings")]
    [SerializeField] public float speed = 200f; // Units per second
    public string direction;

    // Update is called once per frame
    private void Update()
    {
        // Move upward
        transform.Translate(speed * Time.deltaTime * Vector3.up, Space.World);

        // string inputDir = GetInputDirection();

        if (!canBePressed || InputDirectionManager.GetDirectionName() != direction) return;
        if (GameManager._instance != null)
        {
            GameManager._instance.NoteHit();
            
            if (GameManager._instance.particleEffectController != null)
            {
                GameManager._instance.particleEffectController.PlayParticlesForDirection(direction);
            }
        }
        else
        {
            TutorialManager._instance.NoteHit();
            
            if (TutorialManager._instance.particleEffectController != null)
            {
                TutorialManager._instance.particleEffectController.PlayParticlesForDirection(direction);
            }
        }

            
        _hasBeenPressed = true;
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(activatorTag))
        {
            canBePressed = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(activatorTag)) return;
        canBePressed = false;
        if (_hasBeenPressed) return;
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