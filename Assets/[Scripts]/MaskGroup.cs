using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskGroup : DraggableMask
{
    MaskGroupSlot currentSlot;
    [SerializeField] MaskGroupData maskGrpData;

    public void SetMashGroupSlot(MaskGroupSlot _newSlot)
    {
        if (currentSlot != null)
        {
            currentSlot.SwapMaskDataGrp(null);
        }
        currentSlot = _newSlot;
        currentSlot.SwapMaskDataGrp(maskGrpData);
    }
    public override void BeginDrag()
    {
        base.BeginDrag();
        if(!transform.parent.GetComponent<MaskGroupSlot>().IsDrawer)
        {
            InteractionManager.Instance.MaskDrawer.ToggleDrawer(true);
        }
    }
}
