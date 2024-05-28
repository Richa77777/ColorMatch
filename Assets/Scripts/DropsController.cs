using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsController : MonoBehaviour
{
    [SerializeField] private Drop _dropPrefab;
    [SerializeField] private Transform _dropSpawnPoint;

    public ObjectPool<Drop> DropPool { get; private set; }

    private void Start()
    {
        DropPool = new ObjectPool<Drop>(_dropPrefab, 5, this.transform);
    }

    public void SpawnDrop(float fallSpeed)
    {
        Drop drop = DropPool.GetObject();
        drop.transform.position = _dropSpawnPoint.position;
        drop.SetFallSpeed(fallSpeed);
    }
}