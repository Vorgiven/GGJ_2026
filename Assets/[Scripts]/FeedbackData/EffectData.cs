using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Event/Effect", fileName ="Effect_")]
public class EffectData : ScriptableObjectIndex
{
    public string effectName;
    public GameObject effectObject;
    private int index;
}
