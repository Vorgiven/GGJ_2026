using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Mask type",fileName ="MaskType_")]
public class MaskTypeData : ScriptableObject
{
    public Sprite maskSprite;
    public Color maskColor;
    public MaskType maskType;
    public enum MaskType
    {
        Cat,
        Dog,
        Surgical,
        Respiratory,
        Zombie,
        Vampire
    }

}
