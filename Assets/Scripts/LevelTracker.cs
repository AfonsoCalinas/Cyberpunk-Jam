using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelTracker
{
  
    private const string UnlockedLevelKey = "UnlockedLevel";
    private const string CompletedPrefix = "LevelCompleted_"; // Used per-level

    private const int TutorialSceneIndex = 1; // Adjust if your tutorial is at another index

    public static int LastLevelIndex { get; private set; } = -1;

    public static int GetUnlockedLevelIndex()
    {
        // Always return at least the tutorial index
        return Mathf.Max(PlayerPrefs.GetInt(UnlockedLevelKey, TutorialSceneIndex), TutorialSceneIndex);
    }

    public static void UnlockNextLevel()
    {
        int currentUnlocked = GetUnlockedLevelIndex();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        // Ensure we're not unlocking beyond the last level (skip non-level scenes if needed)
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings && nextSceneIndex > currentUnlocked)
        {
            PlayerPrefs.SetInt(UnlockedLevelKey, nextSceneIndex);
            PlayerPrefs.Save();
        }
    }

    public static void MarkLevelCompleted(int buildIndex)
    {
        PlayerPrefs.SetInt(CompletedPrefix + buildIndex, 1);
        PlayerPrefs.Save();
    }

    public static bool IsLevelCompleted(int buildIndex)
    {
        return PlayerPrefs.GetInt(CompletedPrefix + buildIndex, 0) == 1;
    }
    
    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey(UnlockedLevelKey);
        PlayerPrefs.SetInt(UnlockedLevelKey, TutorialSceneIndex); // Always unlock tutorial
        
        // Optionally delete all completed flags
        for (int i = TutorialSceneIndex; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            PlayerPrefs.DeleteKey(CompletedPrefix + i);
        }
        
        PlayerPrefs.Save();
    }

    public static void OnLevelCompleted()
    {
        LastLevelIndex = SceneManager.GetActiveScene().buildIndex;
        MarkLevelCompleted(LastLevelIndex);
        UnlockNextLevel();
    }
}
