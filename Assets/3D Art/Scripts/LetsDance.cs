using UnityEngine;

public class LetsDance : MonoBehaviour
{
    public Animator characterAnimator;
    // public CharacterController characterController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

        /*if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D))
        {
            characterAnimator.SetBool("isDancing", true);
        }
        else 
        {
            characterAnimator.SetBool("isDancing", true);
        }*/
    }
}
