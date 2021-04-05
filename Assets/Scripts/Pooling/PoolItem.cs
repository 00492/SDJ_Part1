using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItem : MonoBehaviour
{
    public Action<PoolItem> _callback;

    public void AddToPool(Action<PoolItem> callback)
    {
        _callback = callback;
        Remove();
    }

    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public virtual void Remove()
    {
        _callback?.Invoke(this);
        gameObject.SetActive(false);
    }
}
