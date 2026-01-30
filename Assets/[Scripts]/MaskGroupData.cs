using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Mask group", fileName = "MaskGroup_")]
public class MaskGroupData : ScriptableObject
{
    public MaskTypeData MaskA;
    public MaskTypeData MaskB;

    public enum MaskGroupTypes
    {
        Animal,
        Hospital,
        Horror
    }
}
