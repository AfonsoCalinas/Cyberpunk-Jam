using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void Retry()
    {
        /*var nextLevelIndex = LevelTracker.LastLevelIndex +1;

        SceneManager.LoadScene(nextLevelIndex);*/
        
        // Timescale may have been changed on loss
        Time.timeScale = 1f;

        int buildIndexToLoad = LevelTracker.LastLevelIndex;

        // Fallback: if LastLevelIndex wasn't set, try to reload the currently active scene
        if (buildIndexToLoad < 0 || buildIndexToLoad >= SceneManager.sceneCountInBuildSettings)
        {
            buildIndexToLoad = SceneManager.GetActiveScene().buildIndex;
        }

        SceneManager.LoadScene(buildIndexToLoad);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}