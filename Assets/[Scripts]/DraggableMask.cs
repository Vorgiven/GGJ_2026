using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableMask : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] Image image;
    [SerializeField] float scaleAmnt = 1.15f;
    public bool Dragged = false;
    public Image ImageComponent => image;

    RectTransform rectTransform;
    Tween lerpBackToPositionTween;
    Sequence shakeSequence;
    Quaternion baseRotation;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Force center anchoring so (0,0) is always visual center
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        baseRotation = rectTransform.localRotation;
    }

    public virtual void BeginDrag()
    {
        Dragged = true;
        transform.SetAsFirstSibling();
        image.raycastTarget = false;
        KillTween();

        shakeSequence = DOTween.Sequence()
               .Append(rectTransform.DOLocalRotate(new Vector3(0, 0, 6f), 0.05f).SetEase(Ease.Linear))
               .Append(rectTransform.DOLocalRotate(Vector3.zero, 0.05f).SetEase(Ease.Linear))
               .Append(rectTransform.DOLocalRotate(new Vector3(0, 0, -6f), 0.05f).SetEase(Ease.Linear))
               .Append(rectTransform.DOLocalRotate(Vector3.zero, 0.05f).SetEase(Ease.Linear))
               .SetLoops(-1, LoopType.Restart);
    }

    public void EndDrag(float duration, RectTransform newParent = null)
    {
    
        KillTween();
        image.raycastTarget = true;

        if (newParent != null)
        {
            rectTransform.SetParent(newParent, true);
        }

        // Always return to visual center
        lerpBackToPositionTween = rectTransform
            .DOAnchorPos(Vector2.zero, duration)
            .SetEase(Ease.OutQuad).OnComplete(() => { Dragged = false; });
    }

    public void FollowCursor(Vector2 parentLocalPos)
    {
        rectTransform.anchoredPosition = parentLocalPos;
    }

    void KillTween()
    {
        if (lerpBackToPositionTween != null && lerpBackToPositionTween.IsActive())
        {
            lerpBackToPositionTween.Kill();
            lerpBackToPositionTween = null;
        }
        if (shakeSequence != null && shakeSequence.IsActive())
        {
            shakeSequence.Kill();
            shakeSequence = null;
            rectTransform.localRotation = baseRotation;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.DOScale(new Vector2(scaleAmnt, scaleAmnt),.15f).SetEase(Ease.InQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.DOScale(Vector2.one, .15f).SetEase(Ease.OutQuad);
    }
}
