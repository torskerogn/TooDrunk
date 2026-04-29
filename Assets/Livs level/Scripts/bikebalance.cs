using UnityEngine;

public class BikeBalance : MonoBehaviour
{
    [Header("Balance Settings")]
    public float drunkForce = 30f;
    public float counterForce = 60f;
    public float fallThreshold = 45f;
    public float resetSpeed = 40f;
    public float forceChangeInterval = 2f;

    [Header("State")]
    public bool isFallen = false;
    public float currentAngle = 90f;

    private float angularVelocity = 0f;
    private float currentDrunkForce = 0f;
    private float forceTimer = 0f;

    void Start()
    {
        currentAngle = 90f;
        PickNewDrunkForce();
    }

    void Update()
    {
        if (isFallen)
        {
            // Slowly reset back to upright
            currentAngle = Mathf.MoveTowards(currentAngle, 90f, resetSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, currentAngle - 90f);

            if (Mathf.Abs(currentAngle - 90f) < 1f)
            {
                currentAngle = 90f;
                angularVelocity = 0f;
                isFallen = false;
            }
            return;
        }

        // Change drunk force direction randomly over time
        forceTimer -= Time.deltaTime;
        if (forceTimer <= 0f)
            PickNewDrunkForce();

        // Apply drunk force
        angularVelocity += currentDrunkForce * Time.deltaTime;

        // Dampen slightly so it doesn't spin forever
        angularVelocity *= 0.98f;

        // Update angle
        currentAngle += angularVelocity * Time.deltaTime;

        // Check fall
        if (currentAngle < 90f - fallThreshold || currentAngle > 90f + fallThreshold)
            isFallen = true;

        // Apply rotation (90 = upright)
        transform.rotation = Quaternion.Euler(0, 0, currentAngle - 90f);
    }

    public void PushLeft()
    {
        if (!isFallen)
            angularVelocity -= counterForce * Time.deltaTime;
    }

    public void PushRight()
    {
        if (!isFallen)
            angularVelocity += counterForce * Time.deltaTime;
    }

    void PickNewDrunkForce()
    {
        // Random direction and strength
        float direction = Random.value > 0.5f ? 1f : -1f;
        currentDrunkForce = direction * drunkForce;
        forceTimer = forceChangeInterval + Random.Range(-0.5f, 0.5f);
    }
}
