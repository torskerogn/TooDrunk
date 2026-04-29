using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    public Camera blueCamera;
    public Camera orangeCamera;

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleInput(Mouse.current.position.ReadValue());
        }

        if (Touchscreen.current != null)
        {
            foreach (var touch in Touchscreen.current.touches)
            {
                if (touch.press.wasPressedThisFrame)
                {
                    HandleInput(touch.position.ReadValue());
                }
            }
        }
    }

    void HandleInput(Vector2 screenPos)
    {
        Camera cam;

        if (screenPos.y > Screen.height / 2f)
            cam = blueCamera;
        else
            cam = orangeCamera;

        Vector2 worldPos = cam.ScreenToWorldPoint(screenPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null)
        {
            ItemBlue itemB = hit.collider.GetComponent<ItemBlue>();
            if (itemB != null) itemB.Tapped();

            ItemOrange itemO = hit.collider.GetComponent<ItemOrange>();
            if (itemO != null) itemO.Tapped();
        }
    }
}