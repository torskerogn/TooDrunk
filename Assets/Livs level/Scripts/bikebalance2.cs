using UnityEngine;

public class BikeBalance : MonoBehaviour
{
    public float gravity = 100f;
    public float counterForce = 300f;
    public float maxCounterForce = 1200f;
    public float resetSpeed = 80f;
    public float fallWaitTime = 1f;
    public float startWaitTime = 1f;

    public float score = 0f;

    private float angle = 90f;
    private float velocity = 0f;
    private bool isFallen = false;
    private float fallTimer = 0f;
    private float startTimer = 0f;
    private float leftHoldTime = 0f;
    private float rightHoldTime = 0f;
    private bool holdingLeft = false;
    private bool holdingRight = false;

    public void OnLeftDown()  { holdingLeft = true; }
    public void OnLeftUp()    { holdingLeft = false; }
    public void OnRightDown() { holdingRight = true; }
    public void OnRightUp()   { holdingRight = false; }

    void Start()
    {
        startTimer = startWaitTime;
        angle = 90f + 10f;
    }

    void Update()
    {
        if (startTimer > 0f) { startTimer -= Time.deltaTime; return; }

        if (isFallen)
        {
            if (fallTimer > 0f) { fallTimer -= Time.deltaTime; return; }

            angle = Mathf.MoveTowards(angle, 90f, resetSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0f, 0f, angle - 90f);

            if (Mathf.Abs(angle - 90f) < 1f)
            {
                angle = 90f + 10f;
                velocity = 0f;
                isFallen = false;
                startTimer = startWaitTime;
            }
            return;
        }

        float fallAmount = Mathf.Abs(angle - 90f) / 90f;
        velocity += Mathf.Sign(angle - 90f) * gravity * (1f + fallAmount * 3f) * Time.deltaTime;

        if (holdingLeft)
        {
            leftHoldTime += Time.deltaTime;
            float force = Mathf.Min(counterForce * (1f + leftHoldTime * 3f), maxCounterForce);
            velocity += force * Time.deltaTime;
        }
        else leftHoldTime = 0f;

        if (holdingRight)
        {
            rightHoldTime += Time.deltaTime;
            float force = Mathf.Min(counterForce * (1f + rightHoldTime * 3f), maxCounterForce);
            velocity -= force * Time.deltaTime;
        }
        else rightHoldTime = 0f;

        velocity *= 0.97f;
        angle += velocity * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0f, 0f, angle - 90f);

        if (angle >= 75f && angle <= 105f)
            score += Time.deltaTime;

        if (angle < 0f || angle > 180f)
        {
            isFallen = true;
            fallTimer = fallWaitTime;
            leftHoldTime = 0f;
            rightHoldTime = 0f;
            velocity = 0f;
        }
    }
}