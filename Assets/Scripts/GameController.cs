using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private ColorsDataBase _colorDataBase;
    [SerializeField] private DifficultiesDataBase _difficultiesDataBase;

    public IReadOnlyCollection<Color32> Colors => _colorDataBase.Colors;
    public IReadOnlyCollection<Difficulty> Difficulties => _difficultiesDataBase.Difficulties;

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
}
