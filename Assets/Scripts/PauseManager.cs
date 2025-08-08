using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public AudioSource musicSource;

    private AudioSource _audioSource;

    public bool isPaused;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        
        //Get an AudioSource
        _audioSource = FindAnyObjectByType<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Or your pause key
        {
            if (isPaused)
                Resume();
            else
                Pause();
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