using UnityEngine;

public class JoystickToggle : MonoBehaviour
{
    public bool simulateMobileInEditor = false;

    void Start()
    {
        bool isMobile = Application.isMobilePlatform;

#if UNITY_EDITOR
        if (simulateMobileInEditor)
            isMobile = true;
#endif

        gameObject.SetActive(isMobile);
    }
}

