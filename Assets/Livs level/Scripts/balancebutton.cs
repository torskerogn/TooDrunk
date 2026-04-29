using UnityEngine;
using UnityEngine.EventSystems;

public class BalanceButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public BikeBalance target;
    public bool isLeft = true;

    private bool held = false;

    void Update()
    {
        if (held)
        {
            if (isLeft) target.PushLeft();
            else target.PushRight();
        }
    }

    public void OnPointerDown(PointerEventData eventData) { held = true; }
    public void OnPointerUp(PointerEventData eventData) { held = false; }
}
