using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private DropsController _dropsController;

    [Space(2f), Header("Databases")]
    [SerializeField] private ColorsDataBase _colorDatabase;
    [SerializeField] private DifficultiesDataBase _difficultiesDatabase;

    public DropsController DropsController => _dropsController;
    public IReadOnlyCollection<Color32> Colors => _colorDatabase.Colors;
    public IReadOnlyCollection<Difficulty> Difficulties => _difficultiesDatabase.Difficulties;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DropsController.SpawnDrop(0.5f);
    }
}
