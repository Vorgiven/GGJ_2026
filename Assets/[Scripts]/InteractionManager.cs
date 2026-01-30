using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour
{
    DraggableMask currentDragingMask;
    Canvas canvas;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryPickUp();

        if (Input.GetMouseButton(0) && currentDragingMask)
            Drag();

        if (Input.GetMouseButtonUp(0) && currentDragingMask)
            Release();
    }

    void TryPickUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            return;

        var data = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);

        if (results.Count == 0) return;

        var firstHit = results[0];
        var mask = firstHit.gameObject.GetComponent<DraggableMask>();
        if (mask == null) return;

        currentDragingMask = mask;
        currentDragingMask.BeginDrag();
        Drag(); // snap to cursor immediately
    }

    void Drag()
    {
        Vector2 localPos = GetParentLocalCursorPos(currentDragingMask.transform.parent as RectTransform);
        currentDragingMask.FollowCursor(localPos);
    }

    void Release()
    {
        currentDragingMask.EndDrag(0.25f);
        currentDragingMask = null;
    }

    Vector2 GetParentLocalCursorPos(RectTransform parent)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parent,
            Input.mousePosition,
            canvas.worldCamera,
            out Vector2 pos
        );
        return pos;
    }
}
