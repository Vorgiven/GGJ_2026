using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MaskGroupSlot : MonoBehaviour
{
    [SerializeField] bool isDrawerSlot;
    public bool IsDrawer => isDrawerSlot;

    private MaskGroupData currentlyEquippedMaskGrpData;
    [SerializeField] SubMask SubMaskA;
    [SerializeField] SubMask SubMaskB;
    RectTransform rectTransform;
    public RectTransform RectTransform => rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        SwapMaskDataGrp(null);
    }
    public void SwapMaskDataGrp(MaskGroupData _newMaskGrpData)
    {
        if(!isDrawerSlot)
        {
            currentlyEquippedMaskGrpData = _newMaskGrpData;
            if (_newMaskGrpData == null)
            {
                SubMaskA.gameObject.SetActive(false);
                SubMaskB.gameObject.SetActive(false);
                return;
            }
            SubMaskA.gameObject.SetActive(true);
            SubMaskB.gameObject.SetActive(true);
            SubMaskA.ChangeMask(_newMaskGrpData.MaskA);
            SubMaskB.ChangeMask(_newMaskGrpData.MaskB);
        }

    }
}
