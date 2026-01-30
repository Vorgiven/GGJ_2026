using DG.Tweening;
using UnityEngine;

public class DraggableMask : MonoBehaviour
{
    [SerializeField] MaskTypeData associatedMaskType;
    public MaskTypeData MaskType => associatedMaskType;

    RectTransform rectTransform;
    Vector2 originalAnchoredPos;
    Tween activeTween;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalAnchoredPos = rectTransform.anchoredPosition;
    }

    public void BeginDrag()
    {
        KillTween();
    }

    public void EndDrag(float duration)
    {
        KillTween();

        activeTween = rectTransform
            .DOAnchorPos(originalAnchoredPos, duration)
            .SetEase(Ease.OutQuad);
    }

    public void FollowCursor(Vector2 parentLocalPos)
    {
        rectTransform.anchoredPosition = parentLocalPos;
    }

    void KillTween()
    {
        if (activeTween != null && activeTween.IsActive())
            activeTween.Kill();
    }
}
