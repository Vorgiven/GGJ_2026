using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Mask type")]
public class MaskTypeData : ScriptableObject
{
    public Sprite maskSprite;
    public MaskType maskType;
    public enum MaskType
    {
        Animal,
        Hospital,
        Horror
    }

}
