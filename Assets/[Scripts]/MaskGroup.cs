using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskGroup : DraggableMask
{
    MaskGroupSlot currentSlot;
    [SerializeField] MaskGroupData maskGrpData;
    public MaskGroupData MaskGroupData => maskGrpData;

    public void SetMashGroupSlot(MaskGroupSlot _newSlot)
    {
        if (currentSlot is ManiquinHead maniquinHead)
        {
            maniquinHead.SwapMaskDataGrp(null);
        }
        currentSlot = _newSlot;
        if (currentSlot is ManiquinHead maniquinHeadTemp)
        {
            maniquinHeadTemp.SwapMaskDataGrp(this);
        }
    }
    public override void BeginDrag()
    {
        base.BeginDrag();
        if(currentSlot is ManiquinHead)
        {
            InteractionManager.Instance.MaskDrawer.ToggleDrawer(true);
        }
    }
}
