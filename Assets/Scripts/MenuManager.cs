using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;


public class MenuManager : MonoBehaviour
{
    public bool simulateMobileInEditor = false;
    public GameObject quitButton;
    private bool _isMobile;
    
    

    public Button[] levelButtons; // Assign these in the Inspector

    void Start()
    {
        UpdateLevelButtons();
        
        _isMobile = Application.isMobilePlatform;

#if UNITY_EDITOR
        if (simulateMobileInEditor)
            _isMobile = true;
#endif

        quitButton.SetActive(!_isMobile);
    }

    void UpdateLevelButtons()
    {
        int unlockedIndex = LevelTracker.GetUnlockedLevelIndex();

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int sceneBuildIndex = 2 + i; // Level1 starts at index 2

            bool isUnlocked = sceneBuildIndex <= unlockedIndex;

            levelButtons[i].interactable = isUnlocked;

            ColorBlock cb = levelButtons[i].colors;
            cb.normalColor = isUnlocked ? Color.white : Color.gray;
            cb.highlightedColor = isUnlocked ? Color.white : Color.gray;
            cb.pressedColor = isUnlocked ? Color.white : Color.gray;
            levelButtons[i].colors = cb;

            // Optionally disable text or icon effects
            // levelButtons[i].GetComponentInChildren<Text>().color = isUnlocked ? Color.white : Color.gray;
        }
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

        int unlockedIndex = LevelTracker.GetUnlockedLevelIndex();

        if (buildIndex <= unlockedIndex)
        {
            if (buildIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(buildIndex);
            }
            else
            {
                Debug.LogWarning("Invalid level number.");
            }
        }
        else
        {
            Debug.Log("Level is locked.");
            // Optionally show UI feedback here (e.g. sound or popup)
        }
    }

    public void OnPlayButtonPressed()
    {
        int unlockedIndex = LevelTracker.GetUnlockedLevelIndex();
        SceneManager.LoadScene(unlockedIndex);
    }
    
    public void OnResetProgressPressed()
    {
        LevelTracker.ResetProgress();
        Debug.Log("Progress reset.");
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
    
    
    /*      credits links      */
    public void LinkSara()
    {
        Application.OpenURL("https://shiroexe.itch.io/");
    }
    
    public void LinkNuno()
    {
        Application.OpenURL("https://nunobaptista57.itch.io/");
    }
    
    public void LinkAfonso()
    {
        Application.OpenURL("https://pew-pew-tuga.itch.io/");
    }
    
    public void LinkRicardo()
    {
        Application.OpenURL("https://mrdummyacc-dot.itch.io/");
    }
}
