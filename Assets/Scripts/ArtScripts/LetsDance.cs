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
    
    
    public bool wining;
    public bool losing;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
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


    private void Update()
    {
        var direction = InputDirectionManager.GetDirectionName();
        var isInputActive = !string.IsNullOrEmpty(direction);

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
                if (!(_retriggerTimer <= 0f)) return;
                // characterAnimator.ResetAllTriggers();
                characterAnimator.SetTrigger(direction);
                _retriggerTimer = retriggerDelay;
            }
        }
        else
        {
            // No input
            _currentDirection = "";
            _retriggerTimer = 0f;
        }
    }
}
