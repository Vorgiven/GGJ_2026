using UnityEngine;
using UnityEngine.UI;

public class SubMask : DraggableMask
{
    [SerializeField] private MaskTypeData associatedMaskType;
    public MaskTypeData MaskType => associatedMaskType;
    public void ChangeMask(MaskTypeData _newMask)
    {
        associatedMaskType = _newMask;
        ImageComponent.color = _newMask.maskColor;
    }
}
