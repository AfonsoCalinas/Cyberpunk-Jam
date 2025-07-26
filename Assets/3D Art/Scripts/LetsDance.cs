using UnityEngine;

public class LetsDance : MonoBehaviour
{
    public Animator characterAnimator;

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

    // Update is called once per frame
    void Update()
    {
         Vector2Int inputDir = new Vector2Int(
             Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")),
             Mathf.RoundToInt(Input.GetAxisRaw("Vertical"))
         );

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
         }
         
    }
}
