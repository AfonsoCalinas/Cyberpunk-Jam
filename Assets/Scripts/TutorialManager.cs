using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    public static TutorialManager _instance;
    [Header("Native parameters")]
    public GameObject speak1;
    public GameObject speak1m;
    public GameObject speak2;
    public GameObject speak3;
    public GameObject speak4;
    public GameObject speak5;
    public GameObject speak6;
    public GameObject speak7;
    public GameObject gameInputs;
    public GameObject tutorialInputs;
    public GameObject arrowTooltip1;
    public GameObject arrowTooltip2;
    public GameObject arrowTooltip3;
    public GameObject arrowTooltip4;
    
    [Space]
    [SerializeField] private GameObject middleNotePrefab;
    [SerializeField] private RectTransform parentCanvas;
    [SerializeField] public RectTransform spawnPoint;
    private int _step = 0;
    /*public TMP_Text hpPlusUpgradeText;
    public TMP_Text hpBarUpgradeText;*/
    [Space]
    [Header("GameManager")]
    private int _currentScore = 0;
    private int _scoreMultiplier = 1;
    private int _scorePerNote = 300;
    private int _combo = 0;
    [Space]
    public TMP_Text _currentScoreText;
    public TMP_Text _scoreMultiplierText;
    public TMP_Text _hpPlusUpgradeText;
    public TMP_Text _hpBarUpgradeText;
    public Slider _healthBar;
    public float _health = 1f;
    public float missDamageAmount = 0.1f;

    [Space]
    public Canvas _canvas;
    public GameObject _perfectPopupText;
    public GameObject _missPopupText;
    public Transform _perfectTransform;
    private Vector3 _perfectPosition;
    public Transform _missTransform;
    private Vector3 _missPosition;
    [Space]
    [SerializeField] private AudioClip goodSound;
    [SerializeField] private AudioClip badSound;
    [Space]
    public Material _dancerMat;
    private float waitTime = 2f;
    [Space]
    public Slider progressBar;

    private int _progress = 0;
    public ParticleEffectController particleEffectController;
    
    private Coroutine noteSpawnerCoroutine;
    public PauseManager pauseManager;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _instance = this;
        
        speak1.SetActive(true);
        speak1m.SetActive(false);
        speak2.SetActive(false);
        speak3.SetActive(false);
        speak4.SetActive(false);
        speak5.SetActive(false);
        speak6.SetActive(false);
        speak7.SetActive(false);
        
        gameInputs.SetActive(true);
        tutorialInputs.SetActive(false);

        arrowTooltip1.SetActive(false);
        arrowTooltip2.SetActive(false);
        arrowTooltip3.SetActive(false);
        arrowTooltip4.SetActive(false);

        _hpPlusUpgradeText.gameObject.SetActive(false);
        _hpBarUpgradeText.gameObject.SetActive(false);
        
//copied parameters from GameManager
        
        _dancerMat.SetFloat("_Expression", 0);
        


        _currentScoreText.text = "Score\n0";

        _scoreMultiplierText.text = "Multiplier\nx1";

        _healthBar.value = _health;

        _missPosition = _missTransform.position;
        
        _perfectPosition = _perfectTransform.position;

        progressBar.minValue = 0;
        progressBar.maxValue = 8;

        _progress = 0;



    }

    // Update is called once per frame
    private void Update()
    {
        progressBar.value = _progress;
    }

    public void Next()
    {
        _step++;
        _progress++;
        SwitchStep();
    }
    public void Back()
    {
        _step--;
        _progress--;
        SwitchStep();
    }

    void SwitchStep()
    {
        
        switch (_step)
        {
            
            case 1:
                /*show the keys you have to press*/
                speak1.SetActive(false);
                speak1m.SetActive(true);
                speak2.SetActive(false);
                tutorialInputs.SetActive(true);
                gameInputs.SetActive(false);
                arrowTooltip1.SetActive(false);
                
                /*stop notes when backwards*/
                if (noteSpawnerCoroutine != null)
                {
                    StopCoroutine(noteSpawnerCoroutine);
                    noteSpawnerCoroutine = null;
                }
                break;
            case 2:
                /*Hit the notes at the sound of the Beat!*/
                tutorialInputs.SetActive(false);
                gameInputs.SetActive(true);
                speak1m.SetActive(false);

                speak2.SetActive(true);
                speak3.SetActive(false);
                arrowTooltip1.SetActive(true);
                arrowTooltip4.SetActive(false);
                
                /*GameObject note = Instantiate(middleNotePrefab, parentCanvas);
                RectTransform noteRect = note.GetComponent<RectTransform>();
                noteRect.anchoredPosition = spawnPoint.anchoredPosition;*/
                
                // Start periodic spawning if not already started
                if (noteSpawnerCoroutine == null)
                {
                    noteSpawnerCoroutine = StartCoroutine(SpawnNotesPeriodically(4,3f));
                }


                break;
            case 3:
                /*stop notes*/
                if (noteSpawnerCoroutine != null)
                {
                    StopCoroutine(noteSpawnerCoroutine);
                    noteSpawnerCoroutine = null;
                }
                
                InputDirectionManager.EnableInput();
                
                // _health = Mathf.Clamp01(_health + 0.5f);
                _healthBar.maxValue = _healthBar.value;
                
                /*This way you can input the code played in music*/
                speak2.SetActive(false);
                speak3.SetActive(true);
                speak4.SetActive(false);
                arrowTooltip1.SetActive(false);
                arrowTooltip2.SetActive(false);
                
                arrowTooltip4.SetActive(true);
                break;
            case 4:
                /*be careful don't miss notes*/
                _healthBar.maxValue = _healthBar.value;
                /*_health = Mathf.Clamp01(_health + 0.5f);
                _healthBar.value = _health;*/
                
                // Start periodic spawning if not already started
                if (noteSpawnerCoroutine == null)
                {
                    noteSpawnerCoroutine = StartCoroutine(SpawnNotesPeriodically(5,1f));
                }
                /*Disable all axis inputs during this step*/
                InputDirectionManager.DisableInput();

                speak3.SetActive(false);
                arrowTooltip4.SetActive(false);


                speak4.SetActive(true);
                speak5.SetActive(false);
                arrowTooltip2.SetActive(true);
                arrowTooltip3.SetActive(false);
                break;
            case 5:
                /*stop notes*/
                if (noteSpawnerCoroutine != null)
                {
                    StopCoroutine(noteSpawnerCoroutine);
                    noteSpawnerCoroutine = null;
                }
                InputDirectionManager.EnableInput();
                
                speak4.SetActive(false);
                speak5.SetActive(true);
                speak6.SetActive(false);
                arrowTooltip2.SetActive(false);
                arrowTooltip3.SetActive(true);
                _hpPlusUpgradeText.gameObject.SetActive(true);
                _hpBarUpgradeText.gameObject.SetActive(true);
                break;
            case 6:
                speak5.SetActive(false);
                speak6.SetActive(true);
                speak7.SetActive(false);
                arrowTooltip3.SetActive(false);
                _hpPlusUpgradeText.gameObject.SetActive(false);
                _hpBarUpgradeText.gameObject.SetActive(false);

                break;
            case 7:
                speak6.SetActive(false);
                speak7.SetActive(true);

                break;
            case 8:
                // SceneManager.LoadScene("Level1Scene");
                LevelTracker.OnLevelCompleted();
                LevelTracker.UnlockNextLevel();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
                break;
        }
    }
    
    private IEnumerator SpawnNotesPeriodically(int noteCountLimit, float time)
    {
        int spawnedCount = 0;
        
        while (spawnedCount < noteCountLimit)
        {
            GameObject note = Instantiate(middleNotePrefab, parentCanvas);
            RectTransform noteRect = note.GetComponent<RectTransform>();
            noteRect.anchoredPosition = spawnPoint.anchoredPosition;

            spawnedCount++;
            yield return new WaitForSeconds(time); // spawn every 1 second (adjust as needed)
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

            _health = Mathf.Clamp01(_health + 0.5f);
            _healthBar.value = _health;

            StartCoroutine(ShowHPPlusUpgradeText());

            _dancerMat.SetFloat("_Expression", 3);
            if (_combo == 40)
            {
                StartCoroutine(ResizeHealthBar(50f, 0.3f));
                StartCoroutine(ShowHPBarUpgradeText());
                _dancerMat.SetFloat("_Expression", 4);
            }
        }
        else
        {
            _dancerMat.SetFloat("_Expression", 2); 
            

            Invoke("HoldExpression", waitTime);
        }

        _currentScore += _scorePerNote * _scoreMultiplier;
        _currentScoreText.text = "Score\n" + _currentScore;

        Debug.Log(_currentScore);
    }
    
    public void NoteMissed()
    {
        Debug.Log("Missed!");

        _dancerMat.SetFloat("_Expression", 1);
        Invoke("HoldExpression", waitTime);
        
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


    }

    private void HoldExpression()
    {
        
        _dancerMat.SetFloat("_Expression", 0);
        // return null;

    }
    
    private IEnumerator ShowHPPlusUpgradeText()
    {
        _hpPlusUpgradeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        _hpPlusUpgradeText.gameObject.SetActive(false);
    }

    private IEnumerator ShowHPBarUpgradeText()
    {
        _hpBarUpgradeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        _hpBarUpgradeText.gameObject.SetActive(false);
    }

    private IEnumerator ResizeHealthBar(float delta, float duration)
    {
        RectTransform rt = _healthBar.GetComponent<RectTransform>();
        float initialWidth = rt.rect.width;
        float targetWidth = initialWidth + delta;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float newWidth = Mathf.Lerp(initialWidth, targetWidth, elapsed / duration);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
    }





    public void SpawnFloatingText(GameObject popupText, Vector3 screenPosition)
    {
        GameObject instance = Instantiate(popupText, _canvas.transform);
        instance.GetComponent<RectTransform>().position = screenPosition;
    }
}
