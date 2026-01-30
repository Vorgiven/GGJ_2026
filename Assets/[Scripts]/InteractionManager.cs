using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionManager : MonoBehaviour
{
    DraggableMask currentDraggableMask;
    Canvas canvas;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryPickUp();

        if (Input.GetMouseButton(0) && currentDraggableMask)
            Drag();

        if (Input.GetMouseButtonUp(0) && currentDraggableMask)
            Release();
    }

    void TryPickUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            return;

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            DraggableMask mask = result.gameObject.GetComponent<DraggableMask>();
            if (mask)
            {
                currentDraggableMask = mask;

                Vector2 cursorPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.transform as RectTransform,
                    Input.mousePosition,
                    canvas.worldCamera,
                    out cursorPos
                );

                currentDraggableMask.TweenTo(cursorPos, 0.15f);
                break;
            }
        }
    }

    void Drag()
    {
        Vector2 cursorPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out cursorPos
        );

        currentDraggableMask.FollowCursor(cursorPos);
    }

    void Release()
    {
        currentDraggableMask.ResetPosition(0.25f);
        currentDraggableMask = null;
    }
}
