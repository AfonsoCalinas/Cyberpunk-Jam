using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public Image _speak1;
    public Image _speak2;
    public Image _speak3;
    public Image _speak4;
    public Image _speak5;
    public Image _speak6;
    public Image _arrowTooltip2;
    public Image _arrowTooltip4;
    public Image _arrowTooltip5;
    private int step = 0;
    [SerializeField] private GameObject _middleNotePrefab;
    [SerializeField] private RectTransform _parentCanvas;
    [SerializeField] public RectTransform _spawnPoint;
    public TMP_Text _hpPlusUpgradeText;
    public TMP_Text _hpBarUpgradeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _speak1.gameObject.SetActive(true);
        _speak2.gameObject.SetActive(false);
        _speak3.gameObject.SetActive(false);
        _speak4.gameObject.SetActive(false);
        _speak5.gameObject.SetActive(false);
        _speak6.gameObject.SetActive(false);

        _arrowTooltip2.gameObject.SetActive(false);
        _arrowTooltip4.gameObject.SetActive(false);
        _arrowTooltip5.gameObject.SetActive(false);

        _hpPlusUpgradeText.gameObject.SetActive(false);
        _hpBarUpgradeText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            step++;
            switch (step)
            {
                case 1:
                    _speak1.gameObject.SetActive(false);
                    _speak2.gameObject.SetActive(true);
                    _arrowTooltip2.gameObject.SetActive(true);
                    GameObject note = Instantiate(_middleNotePrefab, _parentCanvas);
                    RectTransform noteRect = note.GetComponent<RectTransform>();
                    noteRect.anchoredPosition = _spawnPoint.anchoredPosition;
                    break;
                case 2:
                    _speak2.gameObject.SetActive(false);
                    _speak3.gameObject.SetActive(true);
                    _arrowTooltip2.gameObject.SetActive(false);
                    break;
                case 3:
                    _speak3.gameObject.SetActive(false);
                    _speak4.gameObject.SetActive(true);
                    _arrowTooltip4.gameObject.SetActive(true);
                    break;
                case 4:
                    _speak4.gameObject.SetActive(false);
                    _speak5.gameObject.SetActive(true);
                    _arrowTooltip4.gameObject.SetActive(false);
                    _arrowTooltip5.gameObject.SetActive(true);
                    _hpPlusUpgradeText.gameObject.SetActive(true);
                    _hpBarUpgradeText.gameObject.SetActive(true);
                    break;
                case 5:
                    _speak5.gameObject.SetActive(false);
                    _speak6.gameObject.SetActive(true);
                    _arrowTooltip5.gameObject.SetActive(false);
                    _hpPlusUpgradeText.gameObject.SetActive(false);
                    _hpBarUpgradeText.gameObject.SetActive(false);
                    break;
                case 6:
                    SceneManager.LoadScene("GameScene");
                    break;
                default:
                    break;
            }
        }
    }
}
