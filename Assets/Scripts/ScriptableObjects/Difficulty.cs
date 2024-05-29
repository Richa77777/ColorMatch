using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulty", menuName = "Game/Difficulty")]
public class Difficulty : ScriptableObject
{
    [SerializeField] private float _dropSpeed;
    [SerializeField] private float _dropFrequency;
    [SerializeField] private int _numberOfColors;

    public float DropSpeed => _dropSpeed;
    public float DropFrequency => _dropFrequency;
    public int NumberOfColors => _numberOfColors;
}