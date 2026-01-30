using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class DraggableMask : MonoBehaviour
{
    Transform originalTransform;
    [SerializeField] Mask maskScriptable;

    private void Start()
    {
        originalTransform = transform.parent.transform;
    }
    public Mask Mask=> maskScriptable;
}
