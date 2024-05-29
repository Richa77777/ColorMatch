using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Color Database", menuName = "Game/Color Database")]
public class ColorsDatabase : ScriptableObject
{
    [SerializeField] private List<Color32> _colors = new List<Color32>();

    public IReadOnlyCollection<Color32> Colors => _colors;
}
