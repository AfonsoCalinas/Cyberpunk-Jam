using UnityEngine;

public class LetsDance : MonoBehaviour
{
    public Animator characterAnimator;

    public float retriggerDelay = 0.5f;
    private float retriggerTimer = 0f;

    
    private string currentDirection = ""; // track last input
    
    
    public bool wining = false;
    public bool losing = false;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (wining)
        {
            characterAnimator.SetBool("Win", true);
        }

        if (losing)
        {
            characterAnimator.SetBool("Lose", true);
        }
    }
    


    void Update()
    {
        string direction = GetInputDirection();

        bool isInputActive = !string.IsNullOrEmpty(direction);
        characterAnimator.SetBool("IsDancing", isInputActive);

        if (isInputActive)
        {
            if (direction != currentDirection)
            {
                // New direction — trigger immediately
                retriggerTimer = retriggerDelay;
                // characterAnimator.ResetAllTriggers();
                characterAnimator.SetTrigger(direction);
                currentDirection = direction;
            }
            else
            {
                // Same direction — countdown to retrigger
                retriggerTimer -= Time.deltaTime;
                if (retriggerTimer <= 0f)
                {
                    // characterAnimator.ResetAllTriggers();
                    characterAnimator.SetTrigger(direction);
                    retriggerTimer = retriggerDelay;
                }
            }
        }
        else
        {
            // No input
            currentDirection = "";
            retriggerTimer = 0f;
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
}
