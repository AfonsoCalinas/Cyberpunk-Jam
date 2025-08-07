using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    public static TutorialManager _instance;
    private static readonly int Expression = Shader.PropertyToID("_Expression");

    [Header("Native parameters")]
    public GameObject speak1;
    public GameObject speak1M;
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
    private readonly List<GameObject> _spawnedNotes = new List<GameObject>();
    [SerializeField] private GameObject middleNotePrefab;
    [SerializeField] private RectTransform parentCanvas;
    [SerializeField] public RectTransform spawnPoint;
    private int _step;
    /*public TMP_Text hpPlusUpgradeText;
    public TMP_Text hpBarUpgradeText;*/
    [Space]
    [Header("GameManager")]
    private int _currentScore;
    private int _scoreMultiplier = 1;
    private const int ScorePerNote = 300;
    private int _combo;
    [Space]
    public TMP_Text currentScoreText;
    public TMP_Text scoreMultiplierText;
    public TMP_Text hpPlusUpgradeText;
    public TMP_Text hpBarUpgradeText;
    public Slider healthBar;
    public float health = 1f;
    public float hitHealAmount = 0.5f;
    public float missDamageAmount = 0.1f;

    [Space]
    public Canvas canvas;
    public GameObject perfectPopupText;
    public GameObject missPopupText;
    public Transform perfectTransform;
    private Vector3 _perfectPosition;
    public Transform missTransform;
    private Vector3 _missPosition;
    [Space]
    [SerializeField] private AudioClip goodSound;
    [SerializeField] private AudioClip badSound;
    [Space]
    public Material dancerMat;

    private const float WaitTime = 2f;

    [Space]
    public Slider progressBar;

    private int _progress;
    public ParticleEffectController particleEffectController;
    
    private Coroutine _noteSpawnerCoroutine;
    public PauseManager pauseManager;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _instance = this;
        
        speak1.SetActive(true);
        speak1M.SetActive(false);
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

        hpPlusUpgradeText.gameObject.SetActive(false);
        hpBarUpgradeText.gameObject.SetActive(false);
        
//copied parameters from GameManager
        
        dancerMat.SetFloat(Expression, 0);



        currentScoreText.text = "Score\n0";

        scoreMultiplierText.text = "Multiplier\nx1";

        healthBar.value = health;

        _missPosition = missTransform.position;
        
        _perfectPosition = perfectTransform.position;

        progressBar.minValue = 0;
        progressBar.maxValue = 8;


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
                _currentScore = 0;
                currentScoreText.text = "Score\n" + _currentScore;
                DestroyAllSpawnedNotes();
                
                /*show the keys you have to press*/
                speak1.SetActive(false);
                speak1M.SetActive(true);
                speak2.SetActive(false);
                tutorialInputs.SetActive(true);
                gameInputs.SetActive(false);
                arrowTooltip1.SetActive(false);
                
                /*stop notes when backwards*/
                if (_noteSpawnerCoroutine != null)
                {
                    StopCoroutine(_noteSpawnerCoroutine);
                    _noteSpawnerCoroutine = null;
                }
                break;
            case 2:
                /*Hit the notes at the sound of the Beat!*/
                
                _currentScore = 0;
                currentScoreText.text = "Score\n" + _currentScore;
                
                tutorialInputs.SetActive(false);
                gameInputs.SetActive(true);
                speak1M.SetActive(false);

                speak2.SetActive(true);
                speak3.SetActive(false);
                arrowTooltip1.SetActive(true);
                arrowTooltip4.SetActive(false);
                
                // Start periodic spawning if not already started
                _noteSpawnerCoroutine ??= StartCoroutine(SpawnNotesPeriodically(4, 3f));


                break;
            case 3:
                /*stop notes*/
                if (_noteSpawnerCoroutine != null)
                {
                    StopCoroutine(_noteSpawnerCoroutine);
                    _noteSpawnerCoroutine = null;
                }
                DestroyAllSpawnedNotes();
                
                InputDirectionManager.EnableInput();
                
                // health = Mathf.Clamp01(health + 0.5f);
                healthBar.maxValue = healthBar.value;
                
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
                healthBar.maxValue = healthBar.value;
                
                // Start periodic spawning if not already started
                _noteSpawnerCoroutine ??= StartCoroutine(SpawnNotesPeriodically(5, 1f));
                
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
                if (_noteSpawnerCoroutine != null)
                {
                    StopCoroutine(_noteSpawnerCoroutine);
                    _noteSpawnerCoroutine = null;
                }
                
                DestroyAllSpawnedNotes();
                
                InputDirectionManager.EnableInput();
                
                speak4.SetActive(false);
                speak5.SetActive(true);
                speak6.SetActive(false);
                arrowTooltip2.SetActive(false);
                arrowTooltip3.SetActive(true);
                hpPlusUpgradeText.gameObject.SetActive(true);
                hpBarUpgradeText.gameObject.SetActive(true);
                break;
            case 6:
                speak5.SetActive(false);
                speak6.SetActive(true);
                speak7.SetActive(false);
                arrowTooltip3.SetActive(false);
                hpPlusUpgradeText.gameObject.SetActive(false);
                hpBarUpgradeText.gameObject.SetActive(false);

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
            var note = Instantiate(middleNotePrefab, parentCanvas);
            var noteRect = note.GetComponent<RectTransform>();
            noteRect.anchoredPosition = spawnPoint.anchoredPosition;
            
            _spawnedNotes.Add(note); // track the spawned note

            spawnedCount++;
            yield return new WaitForSeconds(time); // spawn every 1 second (adjust as needed)
        }
    }

    private void DestroyAllSpawnedNotes()
    {
        foreach (var note in _spawnedNotes.Where(note => note != null))
        {
            Destroy(note);
        }

        _spawnedNotes.Clear(); // cleanup the list
    }

    
    public void NoteHit()
    {
        Debug.Log("Hit on time");

        // SpawnFloatingText(perfectPopupText, new Vector2(0, 222));
        SpawnFloatingText(perfectPopupText, _perfectPosition);

        // play sfx
        SoundFXManager.instance.PlaySoundFXClip( goodSound, transform, 2f);
        
        _combo++;

        if (_combo == 10 || _combo == 20 || _combo == 40)
        {
            _scoreMultiplier += 3;

            scoreMultiplierText.text = "Multiplier\nx" + _scoreMultiplier;

            health = Mathf.Clamp01(health + hitHealAmount);
            healthBar.value = health;

            StartCoroutine(ShowHpPlusUpgradeText());

            dancerMat.SetFloat(Expression, 3);
            if (_combo == 40)
            {
                StartCoroutine(ResizeHealthBar(50f, 0.3f));
                StartCoroutine(ShowHpBarUpgradeText());
                dancerMat.SetFloat(Expression, 4);
            }
        }
        else
        {
            dancerMat.SetFloat(Expression, 2); 
            

            Invoke(nameof(HoldExpression), WaitTime);
        }

        _currentScore += ScorePerNote * _scoreMultiplier;
        currentScoreText.text = "Score\n" + _currentScore;

        Debug.Log(_currentScore);
    }
    
    public void NoteMissed()
    {
        Debug.Log("Missed!");

        dancerMat.SetFloat(Expression, 1);
        Invoke(nameof(HoldExpression), WaitTime);
        
        // SpawnFloatingText(missPopupText, new Vector2(-220, 197));
        SpawnFloatingText(missPopupText, _missPosition);
        
        // play sfx
        SoundFXManager.instance.PlaySoundFXClip( badSound, transform, 2f);

        
        // Combo Break (reset _combo and multiplier)
        _combo = 0;

        _scoreMultiplier = 1;
        scoreMultiplierText.text = "Multiplier\nx" + _scoreMultiplier;

        health = Mathf.Clamp01(health - missDamageAmount);
        healthBar.value = health;


    }

    private void HoldExpression()
    {
        
        dancerMat.SetFloat(Expression, 0);
        // return null;

    }
    
    private IEnumerator ShowHpPlusUpgradeText()
    {
        hpPlusUpgradeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        hpPlusUpgradeText.gameObject.SetActive(false);
    }

    private IEnumerator ShowHpBarUpgradeText()
    {
        hpBarUpgradeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        hpBarUpgradeText.gameObject.SetActive(false);
    }

    private IEnumerator ResizeHealthBar(float delta, float duration)
    {
        var rt = healthBar.GetComponent<RectTransform>();
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


    private void SpawnFloatingText(GameObject popupText, Vector3 screenPosition)
    {
        var instance = Instantiate(popupText, canvas.transform);
        instance.GetComponent<RectTransform>().position = screenPosition;
    }
}
