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

        var data = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);

        foreach (var r in results)
        {
            var mask = r.gameObject.GetComponent<DraggableMask>();
            if (!mask) continue;

            current = mask;
            current.BeginDrag(canvas);

            Vector2 pos = GetCanvasCursorPos();
            current.TweenTo(pos, 0.15f);
            break;
        }
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
