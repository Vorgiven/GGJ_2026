using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MaskDrawer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform rectTransform;
    Tween DrawerTween;

    // These define the “open” and “closed” positions in local space
    [SerializeField] float closedY = -400f;
    [SerializeField] float openY = 0f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        // Use anchoredPosition instead of world position
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, closedY);
    }

    public void ToggleDrawer(bool toggle)
    {
        if (DrawerTween != null && DrawerTween.IsActive())
        {
            DrawerTween.Kill();
            DrawerTween = null;
        }

        float targetY = toggle ? openY : closedY;

        DrawerTween = rectTransform
            .DOAnchorPosY(targetY, 0.2f)
            .SetEase(Ease.InOutSine);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (InteractionManager.Instance.CurrentlyDraggingMask == null)
            ToggleDrawer(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (InteractionManager.Instance.CurrentlyDraggingMask is not MaskGroup)
            ToggleDrawer(false);
    }
}
