using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItem : MonoBehaviour
{
    private Action<PoolItem> OnInactive;
    private void OnDisable()
    {
        OnInactive?.Invoke(this);
    }
    public void Subscribe(Action<PoolItem> action)
    {
        OnInactive += action;
    }
}
public interface IPoolItem
{
    public void OnInactive(PoolItem poolItem);
}