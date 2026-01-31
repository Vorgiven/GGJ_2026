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
    public void RecallMask()
    {
        CurrentlyEquippedMaskGrp.EndDrag(0.25f, rectTransform);
    }
}
