using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public AudioSource _music;
    public static bool _startMusic;
    public static GameManager _instance;
    private int _currentScore = 0;
    private int _scoreMultiplier = 1;
    private int _scorePerNote = 300;
    private int _combo = 0;
    public TMP_Text _currentScoreText;
    public TMP_Text _scoreMultiplierText;
    public TMP_Text _hpPlusUpgradeText;
    public TMP_Text _hpBarUpgradeText;
    public Slider _healthBar; // Slider
    public RectTransform _healthBarRect;
    public TMP_Text _clickAnyButtonText;
    public float _health = 1f; // 0 to 1 range
    public float hitHealAmount = 0.05f;
    public float missDamageAmount = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _instance = this;

        _startMusic = false;

        _clickAnyButtonText.gameObject.SetActive(true);

        _currentScoreText.text = "Score\n0";

        _scoreMultiplierText.text = "Multiplier\nx1";

        _healthBar.value = _health;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_startMusic && Input.anyKeyDown)
        {
            _clickAnyButtonText.gameObject.SetActive(false);

            _startMusic = true;

            _music.Play();
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit on time");

        _combo++;

        if (_combo == 10 || _combo == 20 || _combo == 40)
        {
            _scoreMultiplier += 3;

            _scoreMultiplierText.text = "Multiplier\nx" + _scoreMultiplier;

            _health = Mathf.Clamp01(_health + 0.5f);
            _healthBar.value = _health;

            StartCoroutine(ShowHPPlusUpgradeText());

            if (_combo == 40)
            {
                StartCoroutine(ResizeHealthBar(50f, 0.3f));
                StartCoroutine(ShowHPBarUpgradeText());
            }
        }

        _currentScore += _scorePerNote * _scoreMultiplier;
        _currentScoreText.text = "Score\n" + _currentScore;

        Debug.Log(_currentScore);
    }

    public void NoteMissed()
    {
        Debug.Log("Missed!");

        // Combo Break (reset _combo and multiplier)
        _combo = 0;

        _scoreMultiplier = 1;
        _scoreMultiplierText.text = "Multiplier\nx" + _scoreMultiplier;

        _health = Mathf.Clamp01(_health - missDamageAmount);
        _healthBar.value = _health;

        if (_health <= 0f)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("GameOverScene");
        }
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

}
