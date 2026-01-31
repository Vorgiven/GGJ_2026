using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MaskGroupSlot : MonoBehaviour
{
    [SerializeField] bool isDrawerSlot;
    public bool IsDrawer => isDrawerSlot;

    private MaskGroup currentlyEquippedMaskGrp;
    public MaskGroup CurrentlyEquippedMaskGrp => currentlyEquippedMaskGrp;
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
        currentlyEquippedMaskGrp = GetComponentInChildren<MaskGroup>();
    }
    public void SwapMaskDataGrp(MaskGroup _newMaskGrp)
    {
        if(!isDrawerSlot)
        {
            currentlyEquippedMaskGrp = _newMaskGrp;
            if (currentlyEquippedMaskGrp==null || _newMaskGrp.MaskGroupData == null)
            {
                SubMaskA.gameObject.SetActive(false);
                SubMaskB.gameObject.SetActive(false);
                return;
            }
            SubMaskA.gameObject.SetActive(true);
            SubMaskB.gameObject.SetActive(true);
            SubMaskA.ChangeMask(_newMaskGrp.MaskGroupData.MaskA);
            SubMaskB.ChangeMask(_newMaskGrp.MaskGroupData.MaskB);
            InteractionManager.Instance.MaskDrawer.ToggleDrawer(false);
        }

    }
}
