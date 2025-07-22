using UnityEngine;
using TMPro;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _instance = this;

        _currentScoreText.text = "Score\n0";

        _scoreMultiplierText.text = "Multiplier\nx1";
    }

    // Update is called once per frame
    void Update()
    {
        if (!_startMusic)
        {
            if (Input.anyKeyDown)
            {
                _startMusic = true;

                _music.Play();
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit on time");

        _combo += 1;

        if (_combo == 2 || _combo == 4 || _combo == 6)
        {
            _scoreMultiplier += 3;

            _scoreMultiplierText.text = "Multiplier\nx" + _scoreMultiplier;
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
    }
}
