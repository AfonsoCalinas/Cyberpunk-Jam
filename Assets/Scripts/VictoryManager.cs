using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

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
