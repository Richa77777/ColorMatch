using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulties Database", menuName = "Game/Difficulties Database")]
public class DifficultiesDatabase : ScriptableObject
{
    [SerializeField] private List<Difficulty> _difficulties = new List<Difficulty>();

    public IReadOnlyCollection<Difficulty> Difficulties => _difficulties;
}
