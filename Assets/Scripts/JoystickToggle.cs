using UnityEngine;

public class JoystickToggle : MonoBehaviour
{
    public bool simulateMobileInEditor;
    public GameObject joystick;
    public GameObject pauseButton;
    private bool _isMobile;

    private void Start()
    { 
        _isMobile = Application.isMobilePlatform;

#if UNITY_EDITOR
        if (simulateMobileInEditor)
            _isMobile = true;
#endif

        gameObject.SetActive(_isMobile);
    }

    private void Update()
    {
        if (GameManager._instance)
        {
            if (_isMobile && GameManager._instance.pauseManager.isPaused)
            {
                joystick.SetActive(false);
                // pauseButton.SetActive(false);
            }
            else
            {
                joystick.SetActive(true);
                pauseButton.SetActive(true);
            }
        }
        else
        {
            if (_isMobile && TutorialManager._instance.pauseManager.isPaused)
            {
                joystick.SetActive(false);
                // pauseButton.SetActive(false);
            }
            else
            {
                joystick.SetActive(true);
                pauseButton.SetActive(true);
            }
        }
    }
}

