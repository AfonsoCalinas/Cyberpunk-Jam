using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelTracker
{
    public static int lastLevelIndex = -1;

    public static void OnLevelCompleted()
    {
        lastLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
