using UnityEngine;
using DG.Tweening;

public class DraggableMask : MonoBehaviour
{
    [SerializeField] MaskTypeData mask;
    RectTransform rectTransform;
    Transform originalParent;
    Vector2 originalAnchoredPos;
    Tween activeTween;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalParent = transform.parent;
        originalAnchoredPos = rectTransform.anchoredPosition;
    }

    public void BeginDrag(Canvas canvas)
    {
        KillTween();

        originalParent = transform.parent;
        transform.SetParent(canvas.transform, true);
    }


    public void EndDrag(float duration)
    {
        KillTween();

        transform.SetParent(originalParent, true);

        rectTransform
            .DOAnchorPos(originalAnchoredPos, duration)
            .SetEase(Ease.OutQuad);
    }


    public void FollowCursor(Vector2 canvasPos)
    {
        rectTransform.anchoredPosition = canvasPos;
    }

    public void TweenTo(Vector2 canvasPos, float duration)
    {
        KillTween();
        activeTween = rectTransform
            .DOAnchorPos(canvasPos, duration)
            .SetEase(Ease.OutQuad);
    }

    void KillTween()
    {
        if (activeTween != null && activeTween.IsActive())
            activeTween.Kill();
    }
}
