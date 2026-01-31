using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class DraggableMask : MonoBehaviour
{
    [SerializeField] Image image;
    public bool Dragged = false;
    public Image ImageComponent => image;

    RectTransform rectTransform;
    Tween activeTween;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Force center anchoring so (0,0) is always visual center
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
    }

    public virtual void BeginDrag()
    {
        Dragged = true;
        transform.SetAsFirstSibling();
        image.raycastTarget = false;
        KillTween();
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
        activeTween = rectTransform
            .DOAnchorPos(Vector2.zero, duration)
            .SetEase(Ease.OutQuad).OnComplete(() => { Dragged = false; });
    }

    public void FollowCursor(Vector2 parentLocalPos)
    {
        rectTransform.anchoredPosition = parentLocalPos;
    }

    void KillTween()
    {
        if (activeTween != null && activeTween.IsActive())
        {
            activeTween.Kill();
            activeTween = null;
        }
    }
}
