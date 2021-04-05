using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private PoolItem _prefab = default;
    [SerializeField, Range(0, 100)] private int _defaultSize = 0;

    private List<PoolItem> _inactives = new List<PoolItem>();
    private List<PoolItem> _actives = new List<PoolItem>();

    protected virtual void Start()
    {
        for (int i = 0; i < _defaultSize; i++)
        {
            AddToPool();
        }
    }

    protected virtual void AddToPool()
    {
        PoolItem obj = Instantiate(_prefab, transform);
        obj.AddToPool(OnRemoveCallback);
    }

    public virtual PoolItem GetAPoolObject()
    {
        int index = _inactives.Count - 1;
        if (index < 0)
        {
            AddToPool();
            index = 0;
        }
        PoolItem obj = _inactives[index];
        _inactives.RemoveAt(index);
        _actives.Add(obj);
        obj.Activate();
        return obj;
    }

    protected virtual void OnRemoveCallback(PoolItem obj)
    {
        _actives.Remove(obj);
        _inactives.Add(obj);
    }
}
