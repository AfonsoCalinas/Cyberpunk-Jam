using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;
using TMPro;


public class MenuManager : MonoBehaviour
{
    public bool simulateMobileInEditor;
    public GameObject quitButton;
    private bool _isMobile;
    
    public TMP_FontAsset unlockedFontAsset;
    public TMP_FontAsset lockedFontAsset;
    public TMP_FontAsset completedFontAsset; 
    
    

    public Button[] levelButtons; // Assign these in the Inspector

    private void Start()
    {
        UpdateLevelButtons();
        
        _isMobile = Application.isMobilePlatform;

#if UNITY_EDITOR
        if (simulateMobileInEditor)
            _isMobile = true;
#endif

        quitButton.SetActive(!_isMobile);
    }

    private void UpdateLevelButtons()
    {
        var unlockedIndex = LevelTracker.GetUnlockedLevelIndex();

        for (var i = 0; i < levelButtons.Length; i++)
        {
            var sceneBuildIndex = 1 + i; // Level1 starts at index 2

            var isUnlocked = sceneBuildIndex <= unlockedIndex;
            var isCompleted = LevelTracker.IsLevelCompleted(sceneBuildIndex);
            
            levelButtons[i].interactable = isUnlocked;

            var cb = levelButtons[i].colors;
            cb.normalColor = isUnlocked ? Color.HSVToRGB(0,0,0.9f,true) : Color.HSVToRGB(0, 0, 0.2f,true);
            cb.highlightedColor = isUnlocked ? Color.HSVToRGB(0,0,1,true) : Color.HSVToRGB(0, 0, 0.3f,true);
            cb.pressedColor = isUnlocked ? Color.HSVToRGB(0, 0, 0.75f, true) : Color.HSVToRGB(0, 0, 0.2f,true);
            levelButtons[i].colors = cb;

            // 🔠 Change font asset
            var tmpText = levelButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (tmpText != null)
            {
                if (isCompleted && completedFontAsset != null)
                    tmpText.font = completedFontAsset;
                else
                    tmpText.font = isUnlocked switch
                    {
                        true when unlockedFontAsset != null => unlockedFontAsset,
                        false when lockedFontAsset != null => lockedFontAsset,
                        _ => tmpText.font
                    };
            }

            if (!isCompleted) continue;
            // Example 1: Change button color
            var colors = levelButtons[i].colors;
            colors.normalColor = Color.HSVToRGB(.12f,1f,0.9f,true);
            colors.highlightedColor = Color.HSVToRGB(.12f,.5f,1f,true);
            colors.pressedColor = Color.HSVToRGB(.12f,1f,0.9f,true);
            levelButtons[i].colors = colors;

            // Example 2: Add a checkmark or trophy icon if you use UI Image or text
            // button.GetComponentInChildren<Text>().text += " ✓";
        }
    }
    
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
        var unlockedIndex = LevelTracker.GetUnlockedLevelIndex();
        SceneManager.LoadScene(unlockedIndex);
    }
    
    public void OnResetProgressPressed()
    {
        LevelTracker.ResetProgress();
        Debug.Log("Progress reset.");
        UpdateLevelButtons();
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
