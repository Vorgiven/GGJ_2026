using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] MaskDrawer maskDrawer;
    public MaskDrawer MaskDrawer => maskDrawer;

    DraggableMask currentlyDraggingMask;
    public DraggableMask CurrentlyDraggingMask => currentlyDraggingMask;

    SubMask equipedSubMask;
    MaskGroup equipeedMaskGrp;

    Enemy currentHoveredEnemy;
    MaskGroupSlot currentlyHoveredGrpSlot;

    Canvas canvas;
    public static InteractionManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    void Update()
    {
        DetectEnemyHover();

        if (Input.GetMouseButtonDown(0) && !currentlyDraggingMask)
        {
            TryPickUp();
        }
        else if (Input.GetMouseButton(0) && currentlyDraggingMask)
        {
            Drag();
        }
        else if (Input.GetMouseButtonUp(0) && currentlyDraggingMask)
        {
            Release();
        }
        
    }
    public void OnHoverGrpSlot(MaskGroupSlot _grpSlot)
    {
        currentlyHoveredGrpSlot = _grpSlot;
    }
    void DetectMaskGroup()
    {
        // Default state every frame
        currentlyHoveredGrpSlot = null;

        if (!EventSystem.current.IsPointerOverGameObject())
            return;

        var data = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);

        if (results.Count == 0)
            return;

        // Find the first MaskGroupSlot in the hit chain
        foreach (var r in results)
        {
            var slot = r.gameObject.GetComponent<MaskGroupSlot>();
            if (slot)
            {
                currentlyHoveredGrpSlot = slot;
                break;
            }
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
                    currentlyDraggingMask = mask;
                    if (currentlyDraggingMask is SubMask subMask)
                    {
                        equipedSubMask = subMask;
                    }
                    else if (currentlyDraggingMask is MaskGroup maskGrp)
                    {
                        equipeedMaskGrp = maskGrp;
                    }
                    currentlyDraggingMask.BeginDrag();
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
        Vector2 localPos = GetParentLocalCursorPos(currentlyDraggingMask.transform.parent as RectTransform);
        currentlyDraggingMask.FollowCursor(localPos);

        if (currentlyDraggingMask is MaskGroup maskGroup)
        {
            DetectMaskGroup();
        }
    }

    void Release()
    {
        if(equipedSubMask)
        {
            equipedSubMask.EndDrag(0.25f);
            equipedSubMask = null;
        }
        if (equipeedMaskGrp)
        {
            if (currentlyHoveredGrpSlot)
            {
                equipeedMaskGrp.SetMashGroupSlot(currentlyHoveredGrpSlot);
                equipeedMaskGrp.EndDrag(0.25f, currentlyHoveredGrpSlot.RectTransform);
                maskDrawer.ToggleDrawer(false);
                currentlyHoveredGrpSlot = null;
            }
            else
            {
                equipeedMaskGrp.EndDrag(0.25f);
            }
            equipeedMaskGrp = null;
        }
        currentlyDraggingMask = null;
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
