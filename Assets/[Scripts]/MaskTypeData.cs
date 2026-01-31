using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Mask type",fileName ="MaskType_")]
public class MaskTypeData : ScriptableObject
{
    public Sprite maskSprite;
    public AnimatorOverrideController animatorOverride;
    public Color maskColor;
    public MaskType maskType;
    [Header("Offset Character")]
    public bool flipCharacter = true;
    public Vector3 offsetPosCharacter;
    public float scaleCharacter = 1;
    [Header("Offset Mask")]
    //public bool flipMask = true;
    public Vector3 offsetPosMask;
    public float scaleMask = 1;

    [Header("Offset Mask Custom")]
    public List<SpriteMaskOffset> spriteMaskOffsets = new List<SpriteMaskOffset>();
    public enum MaskType
    {
        CAT_BLUE,
        CAT_RED,
        DEMON_BLUE,
        DEMON_RED,
        MAGICAL_BLUE,
        MAGICAL_RED
    }

}
[System.Serializable]
public class SpriteMaskOffset
{
    public MaskTypeData maskTarget;
    public Vector3 offsetPosMask;
    public float scaleMask = 1;
}