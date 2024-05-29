using System;
using System.Collections.Generic;
using UnityEngine;

public class DropHitController : MonoBehaviour
{
    [SerializeField] private DropHit _dropHitPrefab;

    public Action<Vector3, Vector3, Color32> OnDestroyDrop;
    public ObjectPool<DropHit> DropHitPool { get; private set; }

    private void Awake()
    {
        DropHitPool = new ObjectPool<DropHit>(_dropHitPrefab, 5, this.transform);
    }

    private void OnEnable()
    {
        List<DropHit> dropHitList = new List<DropHit>(DropHitPool.PoolQueue);

        foreach (DropHit dropHit in dropHitList)
        {
            dropHit.OnStopped += ReturnToPool;
        }

        OnDestroyDrop += CallDropHit;
    }

    private void OnDisable()
    {
        List<DropHit> dropHitList = new List<DropHit>(DropHitPool.PoolQueue);

        foreach (DropHit dropHit in dropHitList)
        {
            dropHit.OnStopped -= ReturnToPool;
        }

        OnDestroyDrop -= CallDropHit;
    }

    public void CallDropHit(Vector3 dropPosition, Vector3 dropRotation, Color32 color)
    {
        DropHit obj = DropHitPool.GetObject();

        ParticleSystem.MainModule mainModule = obj.ParticleSystem.main;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(color);

        obj.transform.position = dropPosition;
        obj.transform.rotation = Quaternion.Euler(dropRotation);
        obj.gameObject.SetActive(true);

    }

    public void ReturnToPool(DropHit obj)
    {
        DropHitPool.ReturnToPool(obj);
    }
}
