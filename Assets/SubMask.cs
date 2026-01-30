using UnityEngine;
using UnityEngine.UI;

[ClassInformation("Derived from DraggableMask", "SubMask will be used in GroupMask to set up",20,12)]
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
