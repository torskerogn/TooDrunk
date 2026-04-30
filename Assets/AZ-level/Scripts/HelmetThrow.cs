using UnityEngine;

public class HelmetThrow : MonoBehaviour
{
    public HelmetMover helmetMover;
    public Camera playerCamera;
    public float minDragDistance = 0.5f;
    public float throwForceMultiplier = 1f;
    // World-Y line marking the edge of this player's drag region:
    //   Bottom owner: drag is clamped to y <= throwLineY (line sits just below the drunk's head).
    //   Top owner:    drag is clamped to y >= throwLineY (line sits just above the drunk's head).
    public float throwLineY = -1f;

    private const int MouseTouchId = -1;
    private const int NoTouchId = -999;

    private bool isDragging = false;
    private int activeTouchId = NoTouchId;
    private Vector3 dragStartPos;
    private Vector3 prevWorld;
    private Vector2 pendingPosition;

    void Update()
    {
        if (helmetMover.isMounted || helmetMover.isLaunched)
        {
            if (isDragging) CancelLocal();
            return;
        }

        if (!isDragging)
        {
            TryBeginDrag();
        }
        else
            UpdateDrag();
    }

    void FixedUpdate()
    {
        if (isDragging) helmetMover.MoveKinematic(pendingPosition);
    }

    void TryBeginDrag()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);
            if (t.phase != TouchPhase.Began) continue;
            if (!IsInPlayerHalf(t.position)) continue;
            Vector3 world = ScreenToWorld(t.position);
            if (HitsHelmet(world))
            {
                BeginDragAt(world);
                activeTouchId = t.fingerId;
                return;
            }
        }

        if (Input.touchCount == 0
            && Input.GetMouseButtonDown(0)
            && IsInPlayerHalf(Input.mousePosition))
        {
            Vector3 world = ScreenToWorld(Input.mousePosition);
            if (HitsHelmet(world))
            {
                BeginDragAt(world);
                activeTouchId = MouseTouchId;
            }
        }
    }

    void UpdateDrag()
    {
        if (activeTouchId >= 0)
        {
            bool found = false;
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                if (t.fingerId != activeTouchId) continue;
                found = true;

                Vector3 world = ScreenToWorld(t.position);

                if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
                {
                    EndDrag(world);
                }
                else
                {
                    pendingPosition = ClampDrag(world);
                    prevWorld = world;
                }
                break;
            }
            if (!found) EndDrag(prevWorld);
        }
        else if (activeTouchId == MouseTouchId)
        {
            Vector3 world = ScreenToWorld(Input.mousePosition);
            if (Input.GetMouseButtonUp(0))
            {
                EndDrag(world);
            }
            else if (Input.GetMouseButton(0))
            {
                pendingPosition = ClampDrag(world);
                prevWorld = world;
            }
            else
            {
                EndDrag(world);
            }
        }
    }

    void BeginDragAt(Vector3 world)
    {
        isDragging = true;
        dragStartPos = world;
        prevWorld = world;
        pendingPosition = ClampDrag(world);
        helmetMover.StartDrag();
    }

    void EndDrag(Vector3 finalWorld)
    {
        float dragDistance = Vector3.Distance(dragStartPos, finalWorld);
        if (dragDistance >= minDragDistance && Time.deltaTime > 0f)
        {
            Vector2 velocity = (Vector2)(finalWorld - prevWorld) / Time.deltaTime * throwForceMultiplier;
            helmetMover.Release(velocity);
        }
        else
        {
            helmetMover.CancelDrag();
        }
        isDragging = false;
        activeTouchId = NoTouchId;
    }

    void CancelLocal()
    {
        isDragging = false;
        activeTouchId = NoTouchId;
    }

    Vector3 ClampDrag(Vector3 world)
    {
        if (helmetMover.owner == PlayerSide.Bottom)
            world.y = Mathf.Min(world.y, throwLineY);
        else
            world.y = Mathf.Max(world.y, throwLineY);
        return world;
    }

    bool HitsHelmet(Vector3 world)
    {
        Collider2D hit = Physics2D.OverlapPoint(world);
        return hit != null && hit.gameObject == helmetMover.gameObject;
    }

    Vector3 ScreenToWorld(Vector3 screen)
    {
        Vector3 pos = playerCamera.ScreenToWorldPoint(screen);
        pos.z = 0f;
        return pos;
    }

    bool IsInPlayerHalf(Vector3 screen)
    {
        Vector3 vp = playerCamera.ScreenToViewportPoint(screen);
        if (vp.x < 0f || vp.x > 1f || vp.y < 0f || vp.y > 1f) return false;
        return helmetMover.owner == PlayerSide.Bottom ? vp.y <= 0.5f : vp.y >= 0.5f;
    }
}
