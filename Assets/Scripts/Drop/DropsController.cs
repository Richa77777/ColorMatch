using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropsController : MonoBehaviour
{
    [SerializeField] private Drop _dropPrefab;
    [SerializeField] private Transform[] _dropSpawnPoints = new Transform[3];

    public ObjectPool<Drop> DropPool { get; private set; }

    private void Start()
    {
        DropPool = new ObjectPool<Drop>(_dropPrefab, 5, this.transform);
    }

    public void SpawnDrop(float fallSpeed)
    {
        Drop drop = DropPool.GetObject();

        List<Color32> colors = new List<Color32>(GameController.Instance.Colors.Take(GameController.Instance.CurrentDifficulty.NumberOfColors));
        int randomColorIndex = Random.Range(0, colors.Count);

        drop.SpriteRenderer.color = GameController.Instance.Colors.ElementAt(randomColorIndex);
        drop.transform.position = _dropSpawnPoints[Random.Range(0, 3)].position;
        drop.SetFallSpeed(fallSpeed);
    }
}