using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ListOfList<T>
{
    [SerializeField] public List<T> list = new List<T>();

    // Add array to lists
    public void AddArray(T[] array)
    {
        foreach (T item in array)
            list.Add(item);
    }
}
