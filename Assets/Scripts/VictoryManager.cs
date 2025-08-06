using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    public void NextHack()
    {
        int nextLevelIndex = LevelTracker.LastLevelIndex + 1;

        SceneManager.LoadScene(nextLevelIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
