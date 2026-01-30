using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class InteractionManager : MonoBehaviour
{
    DraggableMask currentDragingMask;
    Enemy currentHoveredEnemy;
    Canvas canvas;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    void Update()
    {
        DetectEnemyHover();
        if (Input.GetMouseButtonDown(0) && !currentDragingMask)
        {
            TryPickUp();
        }
        else if (Input.GetMouseButton(0) && currentDragingMask)
        {
            Drag();
        }
        else if (Input.GetMouseButtonUp(0) && currentDragingMask)
        {
            Release();
        }

        
    }

    void TryPickUp()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            var data = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(data, results);

            if (results.Count > 0)
            {
                var mask = results[0].gameObject.GetComponent<DraggableMask>();
                if (mask)
                {
                    currentDragingMask = mask;
                    currentDragingMask.BeginDrag();
                    Drag(); // snap to cursor immediately
                }
            }
        }

    }
    void DetectEnemyHover()
    {
        Camera cam = Camera.main;
        if (!cam) return;

        Vector2 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null)
        {
            Enemy enemyScript = hit.collider.GetComponent<Enemy>();
            if (enemyScript)
            {
                currentHoveredEnemy = enemyScript;
            }
        }
        else
        {
            currentHoveredEnemy = null;
        }
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
