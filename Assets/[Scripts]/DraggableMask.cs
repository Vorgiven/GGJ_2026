using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DraggableMask : MonoBehaviour
{
    RectTransform rectTransform;
    Vector2 originalAnchoredPos;
    Tween activeTween;

    [SerializeField] Mask maskScriptable;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalAnchoredPos = rectTransform.anchoredPosition;
    }

    public void KillTween()
    {
        if (activeTween != null && activeTween.IsActive())
            activeTween.Kill();
    }

    public void TweenTo(Vector2 target, float duration)
    {
        KillTween();
        activeTween = rectTransform.DOAnchorPos(target, duration).SetEase(Ease.OutQuad);
    }

    public void ResetPosition(float duration)
    {
        KillTween();
        activeTween = rectTransform.DOAnchorPos(originalAnchoredPos, duration).SetEase(Ease.OutQuad);
    }

    public void FollowCursor(Vector2 target)
    {
        rectTransform.anchoredPosition = target;
    }

    public Mask Mask => maskScriptable;
}
