using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManiquinHead : MaskGroupSlot
{
    [SerializeField] SubMask SubMaskA;
    [SerializeField] SubMask SubMaskB;
    public void SwapMaskDataGrp(MaskGroup _newMaskGrp)
    {
        CurrentlyEquippedMaskGrp = _newMaskGrp;
        if (CurrentlyEquippedMaskGrp == null || _newMaskGrp.MaskGroupData == null)
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
