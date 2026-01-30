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
        if (!maskToFollow) return;

        UpdateFollowPosition();
    }

    void UpdateFollowPosition()
    {
        if (!maskToFollowRectTrans || !canvas) return;

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
    }
}
