using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [Space(2f), Header("Databases")]
    [SerializeField] private ColorsDataBase _colorDatabase;
    [SerializeField] private DifficultiesDataBase _difficultiesDatabase;

    [Space(2f), Header("Controllers")]
    [SerializeField] private DropsController _dropsController;
    [SerializeField] private DropHitController _dropHitController;

    [Space(2f), Header("Background")]
    [SerializeField] private Sprite[] _backgrounds = new Sprite[4];
    [SerializeField] private Image _backgroundImage;

    public int ScoreForMatch { get; private set; } = 1;
    public int ScoreForMismatch { get; private set; } = -1;

    public Difficulty CurrentDifficulty { get; private set; }
    public DropsController DropsController => _dropsController;
    public DropHitController DropHitController => _dropHitController;

    public IReadOnlyCollection<Color32> Colors => _colorDatabase.Colors;
    public IReadOnlyCollection<Difficulty> Difficulties => _difficultiesDatabase.Difficulties;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        _backgroundImage.sprite = PlayerPrefs.HasKey("Background") ? _backgrounds[PlayerPrefs.GetInt("Background")] : _backgrounds[0];

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            print("s");
        }
        else
        {
            Destroy(gameObject);
        }

        CurrentDifficulty = _difficultiesDatabase.Difficulties.FirstOrDefault();

        StartCoroutine(GameProcess());
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        CurrentDifficulty = difficulty;
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
