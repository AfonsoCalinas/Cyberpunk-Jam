using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelTracker
{
    public static int lastLevelIndex = -1;
    
    private const string UnlockedLevelKey = "UnlockedLevel";

    public static int GetUnlockedLevelIndex()
    {
        return PlayerPrefs.GetInt(UnlockedLevelKey, 1); // 1 = TutorialScene index
    }

    public static void UnlockNextLevel()
    {
        int current = GetUnlockedLevelIndex();
        if (current < SceneManager.sceneCountInBuildSettings - 1)
        {
            PlayerPrefs.SetInt(UnlockedLevelKey, current + 1);
            PlayerPrefs.Save();
        }
    }

    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey(UnlockedLevelKey);
    }

    public static void OnLevelCompleted()
    {
        lastLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
