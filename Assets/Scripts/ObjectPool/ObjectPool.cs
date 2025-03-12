using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Queue<T> _pool;
    private readonly int _initialSize;

    public ObjectPool(T prefab, int initialSize = 10, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;
        _initialSize = initialSize;
        _pool = new Queue<T>();

        for (var i = 0; i < _initialSize; i++)
        {
            AddObjectToPool();
        }
    }

    private T AddObjectToPool()
    {
        T obj = Object.Instantiate(_prefab, _parent);
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
        return obj;
    }

    public T Get()
    {
        if (_pool.Count == 0)
        {
            AddObjectToPool();
        }
        
        T obj = _pool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
}
