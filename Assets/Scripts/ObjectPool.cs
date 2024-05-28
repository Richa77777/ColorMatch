using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private Queue<T> _poolQueue = new Queue<T>();

    private T _poolObjectPrefab;
    private Transform _poolParent;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        _poolObjectPrefab = prefab;
        _poolParent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            T newObj = Object.Instantiate(prefab, parent);
            newObj.gameObject.SetActive(false);

            _poolQueue.Enqueue(newObj);
        }
    }

    public T GetObject()
    {
        if (_poolQueue.Count > 0)
        {
            T obj = _poolQueue.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            T newObj = Object.Instantiate(_poolObjectPrefab, _poolParent);
            return newObj;
        }
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        _poolQueue.Enqueue(obj);
    }
}
