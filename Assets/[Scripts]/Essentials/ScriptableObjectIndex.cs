using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectIndex : ScriptableObject
{
    private int index;
    public void SetIndex(int index) => this.index = index;
    public int GetIndex() => index;
    protected virtual void OnDisable()
    {
        index = -1;
    }
}
