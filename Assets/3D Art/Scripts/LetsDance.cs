using UnityEngine;

public class LetsDance : MonoBehaviour
{
    public Animator characterAnimator;

    public bool wining = false;
    public bool losing = false;
    
    private string currentDirection = ""; // track last input

    
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
    
    /*
    private (Vector2Int, string) GetInputDirection()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2Int dir = new Vector2Int(Mathf.RoundToInt(h), Mathf.RoundToInt(v));
        string name = "";

        if (dir.x == -1 && dir.y == 1) name = "WA";
        else if (dir.x == 1 && dir.y == 1) name = "WD";
        else if (dir.x == -1 && dir.y == -1) name = "SA";
        else if (dir.x == 1 && dir.y == -1) name = "SD";
        else if (dir.x == -1) name = "A";
        else if (dir.x == 1) name = "D";
        else if (dir.y == 1) name = "W";
        else if (dir.y == -1) name = "S";

        return (dir, name);
    }*/


    // Update is called once per frame
    void Update()
    {
         /*Vector2Int inputDir = new Vector2Int(
             Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")),
             Mathf.RoundToInt(Input.GetAxisRaw("Vertical"))
         );*/
         
         /*
         var (inputDir, inputName) = GetInputDirection();

         if (inputDir == Vector2Int.zero)
         {
             // No input — keep idle tile on
             characterAnimator.SetBool("isDancing", false);
             characterAnimator.SetFloat("StepX", 0f);
             characterAnimator.SetFloat("StepY", 0f);
             characterAnimator.SetBool("isDiagonal", false);
         }
         else 
         {
             characterAnimator.SetFloat("StepX", inputDir.x);
             characterAnimator.SetFloat("StepY", inputDir.y);
             characterAnimator.SetTrigger("Dance");
             characterAnimator.SetBool("isDancing", true);
             
             bool isDiagonal = inputDir.x != 0 && inputDir.y != 0;
             characterAnimator.SetBool("isDiagonal", isDiagonal);
             characterAnimator.SetTrigger("X");
         }*/
         string direction = GetInputDirection();
         
         bool isInputActive = !string.IsNullOrEmpty(direction);
         characterAnimator.SetBool("IsDancing", isInputActive);

         if (!string.IsNullOrEmpty(direction) && direction != currentDirection)
         {
             // characterAnimator.ResetAllTriggers(); // Custom method below
             characterAnimator.SetTrigger(direction);
             currentDirection = direction;
         }
         else if (string.IsNullOrEmpty(direction))
         {
             currentDirection = "";
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
