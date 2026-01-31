using UnityEngine;

public class MaskUIFollow : MonoBehaviour
{
    [SerializeField] DraggableMask maskToFollow;
    RectTransform maskToFollowRectTrans;

    RectTransform rectTransform;
    Canvas canvas;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        maskToFollowRectTrans = maskToFollow.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!maskToFollowRectTrans || !canvas) return;

        UpdateFollowTransform();
    }

    void UpdateFollowTransform()
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(
            canvas.worldCamera,
            maskToFollowRectTrans.position
        );

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            screenPos,
            canvas.worldCamera,
            out Vector2 localPos
        );

        rectTransform.anchoredPosition = localPos;
        rectTransform.localRotation = maskToFollowRectTrans.localRotation;
        rectTransform.localScale = maskToFollowRectTrans.localScale;
    }
}
