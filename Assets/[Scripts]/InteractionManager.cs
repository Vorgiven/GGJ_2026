using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour
{
    DraggableMask current;
    Canvas canvas;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryPickUp();

        if (Input.GetMouseButton(0) && current)
            Drag();

        if (Input.GetMouseButtonUp(0) && current)
            Release();
    }

    void TryPickUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            return;

        // Create pointer event at mouse position
        var data = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        // Raycast
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);

        if (results.Count == 0) return; // nothing hit

        // Take the first hit
        var firstHit = results[0];
        var mask = firstHit.gameObject.GetComponent<DraggableMask>();
        if (mask == null) return;

        current = mask;
        current.BeginDrag(canvas);

        Vector2 pos = GetCanvasCursorPos();
    }


    void Drag()
    {
        current.FollowCursor(GetCanvasCursorPos());
    }

    void Release()
    {
        current.EndDrag(0.25f);
        current = null;
    }

    Vector2 GetCanvasCursorPos()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out Vector2 pos
        );
        return pos;
    }
}
