using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MaskGroupSlot : MonoBehaviour
{
    public MaskGroup CurrentlyEquippedMaskGrp;

    RectTransform rectTransform;
    public RectTransform RectTransform => rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        CurrentlyEquippedMaskGrp = GetComponentInChildren<MaskGroup>();
    }
    
}
