using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Action OnMiss;
    public Action OnLose;

    [Space(2f), Header("Databases")]
    [SerializeField] private ColorsDatabase _colorDatabase;
    [SerializeField] private DifficultiesDatabase _difficultiesDatabase;

    [Space(2f), Header("Controllers")]
    [SerializeField] private DropsController _dropsController;
    [SerializeField] private DropHitController _dropHitController;

    [Space(2f), Header("Background")]
    [SerializeField] private Sprite[] _backgrounds = new Sprite[4];
    [SerializeField] private Image _backgroundImage;

    [SerializeField] private GameObject _loseTab;

    private int _misses = 0;
    private Coroutine _gameProcess;

    public int ScoreForMatch { get; private set; } = 1;
    public int ScoreForMismatch { get; private set; } = -1;

    public Difficulty CurrentDifficulty { get; private set; }
    public DropsController DropsController => _dropsController;
    public DropHitController DropHitController => _dropHitController;

    public IReadOnlyCollection<Color32> Colors => _colorDatabase.Colors;
    public IReadOnlyCollection<Difficulty> Difficulties => _difficultiesDatabase.Difficulties;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;

        _backgroundImage.sprite = PlayerPrefs.HasKey("Background") ? _backgrounds[PlayerPrefs.GetInt("Background")] : _backgrounds[0];

        CurrentDifficulty = Difficulties.ElementAt(PlayerPrefs.GetInt("Difficulty")); // Устанавливаем сложность

        if (_gameProcess != null)
        {
            StopCoroutine(_gameProcess);
            _gameProcess = null;
        }

        _gameProcess = StartCoroutine(GameProcess());
    }

    private void OnEnable()
    {
        OnMiss += AddMiss;
    }

    private void OnDisable()
    {
        OnMiss -= AddMiss;
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        CurrentDifficulty = difficulty;
    }

    private void AddMiss()
    {
        _misses++;

        if (_misses == CurrentDifficulty.MissesForLose)
        {
            Lose();
            OnLose?.Invoke();
        }
    }

    private void Lose()
    {
        StopCoroutine(_gameProcess);
        _gameProcess = null;

        _loseTab.SetActive(true);
    }

    private IEnumerator GameProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(CurrentDifficulty.DropFrequency);
            _dropsController.SpawnDrop(CurrentDifficulty.DropSpeed);
        }
    }
}
