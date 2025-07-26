using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2Scene");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level3Scene");
    }

    public void Level4()
    {
        SceneManager.LoadScene("Level4Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
