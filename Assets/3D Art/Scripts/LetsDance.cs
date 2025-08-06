using UnityEngine;

public class LetsDance : MonoBehaviour
{
    // private static readonly int IsDancing = Animator.StringToHash("isDancing");
    private static readonly int Win = Animator.StringToHash("Win");
    private static readonly int Lose = Animator.StringToHash("Lose");
    public Animator characterAnimator;

    public float retriggerDelay = 0.5f;
    private float _retriggerTimer;

    
    private string _currentDirection = ""; // track last input
    
    
    public bool wining = false;
    public bool losing = false;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (wining)
        {
            characterAnimator.SetBool(Win, true);
        }

        if (losing)
        {
            characterAnimator.SetBool(Lose, true);
        }
    }
    


    void Update()
    {
        /*string direction = GetInputDirection();

        bool isInputActive = !string.IsNullOrEmpty(direction);
        characterAnimator.SetBool("IsDancing", isInputActive);*/
        
        string direction = InputDirectionManager.GetDirectionName();
        bool isInputActive = !string.IsNullOrEmpty(direction);

        // Update IsDancing
        // characterAnimator.SetBool(IsDancing, isInputActive);

        if (isInputActive)
        {
            if (direction != _currentDirection)
            {
                // New direction — trigger immediately
                _retriggerTimer = retriggerDelay;
                // characterAnimator.ResetAllTriggers();
                characterAnimator.SetTrigger(direction);
                _currentDirection = direction;
            }
            else
            {
                // Same direction — countdown to retrigger
                _retriggerTimer -= Time.deltaTime;
                if (_retriggerTimer <= 0f)
                {
                    // characterAnimator.ResetAllTriggers();
                    characterAnimator.SetTrigger(direction);
                    _retriggerTimer = retriggerDelay;
                }
            }
        }
        else
        {
            // No input
            _currentDirection = "";
            _retriggerTimer = 0f;
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
}
