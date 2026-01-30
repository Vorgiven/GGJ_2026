using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MaskDrawer : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    RectTransform rectTransform;
    Tween DrawerTween;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        rectTransform.position = new Vector2(150, -400);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleDrawer(bool toggle)
    {
        if (DrawerTween != null && DrawerTween.IsActive())
        {
            DrawerTween.Kill();
            DrawerTween = null;
        }
           
        if (toggle)
        {
            DrawerTween = rectTransform.DOAnchorPosY(0, 0.2f).SetEase(Ease.InOutSine);
        }
        else
        {
            DrawerTween = rectTransform.DOAnchorPosY(-400, 0.2f).SetEase(Ease.InOutSine);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if(InteractionManager.Instance.CurrentlyDraggingMask == null)
        {
            ToggleDrawer(true);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (InteractionManager.Instance.CurrentlyDraggingMask is not MaskGroup)
        {
            ToggleDrawer(false);
        }
       
    }
}
