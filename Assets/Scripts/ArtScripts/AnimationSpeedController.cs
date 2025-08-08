using UnityEngine;
// using UnityEngine.SceneManagement;

public class AnimationSpeedController : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        animator.speed = LevelSettings.GetAnimSpeedForCurrentLevel();
    }

    /*private static float GetSpeedForCurrentLevel()
    {
        var index = SceneManager.GetActiveScene().buildIndex;

        // Assuming: MainMenu = 0, Tutorial = 1, Level1 = 2, Level2 = 3, etc.
        switch (index)
        {
            case 2: return 1.0f;   // Level1
            case 3: return 1.25f;  // Level2
            case 4: return 1.5f;   // Level3
            case 5: return 1.75f;  // Level4
            default: return 1.0f;  // Default speed
        }
    }*/
}