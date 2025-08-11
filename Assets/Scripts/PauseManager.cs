using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    //public AudioSource musicSource;

    private AudioSource _audioSource;

    public bool isPaused;
    public bool isSetting;
    public bool hasStarted;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        
        //Get an AudioSource
        _audioSource = FindAnyObjectByType<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && hasStarted) // Or your pause key
        {
            if (isPaused && !isSetting)
                Resume();
            else
            {
                if (!isSetting)
                {
                    Pause();
                }
                else
                {
                    NotSetting();
                }

            }

        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        _audioSource.Play();
        // musicSource.Play();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        _audioSource.Pause();
        //musicSource.Pause();
    }

    public void Setting()
    {
        settingsMenuUI.SetActive(true);

        isSetting = true;
    }
    public void NotSetting()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        isSetting = false;
    }

    
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Or use Application.Quit() for builds
    }
}