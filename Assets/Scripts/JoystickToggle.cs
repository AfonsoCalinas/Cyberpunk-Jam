using System;
using UnityEngine;

public class JoystickToggle : MonoBehaviour
{
    public bool simulateMobileInEditor = false;
    public GameObject joystick;
    public GameObject pauseButton;
    private bool isMobile;
    void Start()
    { 
        isMobile = Application.isMobilePlatform;

#if UNITY_EDITOR
        if (simulateMobileInEditor)
            isMobile = true;
#endif

        gameObject.SetActive(isMobile);
    }

    private void Update()
    {
        if (GameManager._instance != null)
        {
            if (isMobile && GameManager._instance.pauseManager.isPaused)
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
            if (isMobile && TutorialManager._instance.pauseManager.isPaused)
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

