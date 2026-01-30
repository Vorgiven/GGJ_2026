using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private MaskTypeData maskType;
    [SerializeField] private DraggableMask maskEquipped;

    public void EquipMask(DraggableMask mask)
    {
        maskEquipped = mask;
        //if(mask.maskType == maskType)
        //{
        //    Debug.Log("Match");
        //}
        //else
        //{
        //    Debug.Log("Not Match");
        //}
    }
}
