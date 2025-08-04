using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool simulateMobileInEditor = false;
    public GameObject quitButton;
    private bool _isMobile;
    void Start()
    { 
        _isMobile = Application.isMobilePlatform;

#if UNITY_EDITOR
        if (simulateMobileInEditor)
            _isMobile = true;
#endif

        quitButton.SetActive(!_isMobile);
    }
    
    /*public void Level1()
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
    }*/
    
    public void LoadLevelByIndex(int levelNumber)
    {
        // Assuming Level1Scene starts at index 2
        int baseIndex = 2;

        int buildIndex = baseIndex + (levelNumber - 1);

        if (buildIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(buildIndex);
        }
        else
        {
            Debug.LogWarning("Invalid level number.");
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
