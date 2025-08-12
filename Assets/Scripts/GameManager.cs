using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{

    // public AudioSource _music;

    private AudioSource _audioSource;
    private static bool _startMusic;
    public static GameManager _instance;
    private static readonly int Expression = Shader.PropertyToID("_Expression");
    private int _currentScore;
    private int _scoreMultiplier = 1;
    private const int ScorePerNote = 300;
    private int _combo;
    public TMP_Text _currentScoreText;
    public TMP_Text _scoreMultiplierText;
    public TMP_Text _hpPlusUpgradeText;
    public TMP_Text _hpBarUpgradeText;
    public Slider _healthBar;
    public RectTransform _healthBarRect;
    public GameObject _clickAnyButton;
    public TextMeshProUGUI levelTitle;
    public float _health = 1f;
    public float hitHealAmount = 0.5f;
    public float missDamageAmount = 0.1f;
    private bool _sceneScheduled;
    public Canvas _canvas;
    public GameObject _perfectPopupText;
    public GameObject _missPopupText;
    public Transform _perfectTransform;
    private Vector3 _perfectPosition;
    public Transform _missTransform;
    private Vector3 _missPosition;
    
    [SerializeField] private AudioClip goodSound;
    [SerializeField] private AudioClip badSound;

    public Material _dancerMat;
    private LetsDance _letsDance;
    private float _animTrim;
    private const float WaitTime = 2f;

    public Slider progressBar;
    public ParticleEffectController particleEffectController;
    public PauseManager pauseManager;
    
    private void Awake()
    {
        LevelTracker.SetCurrentLevel(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        _dancerMat.SetFloat(Expression, 0);
        
        
        _instance = this;

        _startMusic = false;

        _clickAnyButton.SetActive(true);
        


        _currentScoreText.text = "Score\n0";

        _scoreMultiplierText.text = "Multiplier\nx1";

        pauseManager.hasStarted = false;

        _healthBar.value = _health;

        _missPosition = _missTransform.position;
        
        _perfectPosition = _perfectTransform.position;
        
        _sceneScheduled = false;

        _letsDance = FindAnyObjectByType<LetsDance>();
        _audioSource = FindAnyObjectByType<AudioSource>();
        // _audioSource = beatManager.music;
        
        var clip = LevelSettings.GetMusic();
        if (clip != null)
        {
            _audioSource.clip = clip;

        }
        else
        {
            Debug.LogWarning("Music clip not found or missing in Resources/Music/");
        }
        
        _animTrim = LevelSettings.GetAnimEndingForCurrentLevel();

        if (_audioSource.clip != null)
        {
            progressBar.minValue = 0;
            progressBar.maxValue = _audioSource.clip.length;
        }
        UpdateLevelTitle();
    }

    private void Update()
    {

        if (!_startMusic && Input.anyKeyDown /*&& !Input.GetKeyDown(KeyCode.Space)*/)
        {
            _clickAnyButton.SetActive(false);

            _startMusic = true;

            _audioSource.Play();

            ScheduleSceneTransition();

            _letsDance.StartDancing();

            pauseManager.hasStarted = true;

        }

        if (_audioSource.isPlaying)
        {
            progressBar.value = _audioSource.time;
        }
        
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal();
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(ResizeHealthBar(50f, 0.3f));
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SimulateWin();
        }
        
        // Speed up the whole game
        if (Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = +2f; // 2x faster
            Debug.Log("Debug: Time scale set to +2x");
        }

        // Reset to normal speed
        if (Input.GetKeyDown(KeyCode.G))
        {
            Time.timeScale = 1f;
            Debug.Log("Debug: Time scale reset to normal");
        }
#endif
    }
    
    
#if UNITY_EDITOR
    private void SimulateWin()
    {
        // Call your normal level-win logic here
        GoToVictoryScene();
    }
#endif


    private void UpdateLevelTitle()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        // Assuming your levels start at build index 2 (MainMenu = 0, Tutorial = 1, Level 1 = 2)
        int levelNumber = buildIndex - 1;
        

        if (levelTitle != null)
        {
            levelTitle.SetText("Level " + levelNumber);
        }
    }
    
    public void NoteHit()
    {
        Debug.Log("Hit on time");

        // SpawnFloatingText(_perfectPopupText, new Vector2(0, 222));
        SpawnFloatingText(_perfectPopupText, _perfectPosition);

        // play sfx
        SoundFXManager.instance.PlaySoundFXClip( goodSound, transform, 2f);
        
        _combo++;

        if (_combo == 10 || _combo == 20 || _combo == 40)
        {
            _scoreMultiplier += 3;

            _scoreMultiplierText.text = "Multiplier\nx" + _scoreMultiplier;

            Heal();

            StartCoroutine(ShowHpPlusUpgradeText());

            _dancerMat.SetFloat(Expression, 3);
            if (_combo == 40)
            {
                StartCoroutine(ResizeHealthBar(50f, 0.3f));
                StartCoroutine(ShowHpBarUpgradeText());
                _dancerMat.SetFloat(Expression, 4);
            }
        }
        else
        {
            _dancerMat.SetFloat(Expression, 2); 
            

            Invoke(nameof(HoldExpression), WaitTime);
        }

        _currentScore += ScorePerNote * _scoreMultiplier;
        _currentScoreText.text = "Score\n" + _currentScore;

        Debug.Log(_currentScore);
    }

    private void Heal()
    {
        _health = Mathf.Clamp01(_health + hitHealAmount);
        _healthBar.value = _health;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed!");

        _dancerMat.SetFloat(Expression, 1);
        Invoke(nameof(HoldExpression), WaitTime);
        
        // SpawnFloatingText(_missPopupText, new Vector2(-220, 197));
        SpawnFloatingText(_missPopupText, _missPosition);
        
        // play sfx
        SoundFXManager.instance.PlaySoundFXClip( badSound, transform, 2f);

        
        // Combo Break (reset _combo and multiplier)
        _combo = 0;

        _scoreMultiplier = 1;
        _scoreMultiplierText.text = "Multiplier\nx" + _scoreMultiplier;

        _health = Mathf.Clamp01(_health - missDamageAmount);
        _healthBar.value = _health;

        if (_health <= 0f)
        {
            _dancerMat.SetFloat(Expression, 1);
            Debug.Log("Game Over!");
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void HoldExpression()
    {
        
        _dancerMat.SetFloat(Expression, 0);
        // return null;

    }
    
    private IEnumerator ShowHpPlusUpgradeText()
    {
        _hpPlusUpgradeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        _hpPlusUpgradeText.gameObject.SetActive(false);
    }

    private IEnumerator ShowHpBarUpgradeText()
    {
        _hpBarUpgradeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        _hpBarUpgradeText.gameObject.SetActive(false);
    }

    private IEnumerator ResizeHealthBar(float delta, float duration)
    {
        RectTransform rt = _healthBarRect.GetComponent<RectTransform>();
        var initialWidth = rt.rect.width;
        var targetWidth = initialWidth + delta;
        var elapsed = 0f;

        while (elapsed < duration)
        {
            var newWidth = Mathf.Lerp(initialWidth, targetWidth, elapsed / duration);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
    }

    private void ScheduleSceneTransition()
    {
        if (_audioSource.clip != null && !_sceneScheduled)
        {
            var delay = Mathf.Max(_audioSource.clip.length - 5f, 0f);
            _sceneScheduled = true;
            Invoke(nameof(GoToVictoryScene), delay);
            
            
            var animDelay = delay - _animTrim+ 5f;
        
            Invoke(nameof(TransAnim), animDelay);
        }
    }

    private void TransAnim()
    {
        _letsDance.StopDancing();
    }

    private void GoToVictoryScene()
    {
        _dancerMat.SetFloat(Expression, 4);
        
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        var totalScenes = SceneManager.sceneCountInBuildSettings;

        // Calculate the index of the 4th scene from the end
        var fourthFromLastIndex = totalScenes - 4;
        
        // var fourthFromLastIndex = totalScenes;

        LevelTracker.OnLevelCompleted();
        if (currentIndex == fourthFromLastIndex)
        {
            SceneManager.LoadScene("GameCompleteScene");
        }
        else
        {
            LevelTracker.UnlockNextLevel();
            SceneManager.LoadScene("VictoryScene");
        }
    }

    private void SpawnFloatingText(GameObject popupText, Vector3 screenPosition)
    {
        GameObject instance = Instantiate(popupText, _canvas.transform);
        instance.GetComponent<RectTransform>().position = screenPosition;
    }
}
