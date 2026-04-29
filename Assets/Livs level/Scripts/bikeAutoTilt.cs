using UnityEngine;

public class BikeAutoTilt : MonoBehaviour
{
    public float drunkForce = 30f;
    public float fallThreshold = 45f;
    public float resetSpeed = 40f;
    public float forceChangeInterval = 2f;

    [HideInInspector] public float angularVelocity = 0f;

    private float currentAngle = 0f;
    private float currentDrunkForce = 0f;
    private float forceTimer = 0f;
    private bool isFallen = false;

    void Start()
    {
        PickNewDrunkForce();
    }

    void Update()
    {
        if (isFallen)
        {
            currentAngle = Mathf.MoveTowards(currentAngle, 0f, resetSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0f, 0f, currentAngle);
            if (Mathf.Abs(currentAngle) < 0.5f)
            {
                currentAngle = 0f;
                angularVelocity = 0f;
                isFallen = false;
            }
            return;
        }

        forceTimer -= Time.deltaTime;
        if (forceTimer <= 0f) PickNewDrunkForce();

        angularVelocity += currentDrunkForce * Time.deltaTime;
        angularVelocity *= 0.98f;

        currentAngle += angularVelocity * Time.deltaTime;

        if (Mathf.Abs(currentAngle) > fallThreshold)
            isFallen = true;

        transform.localRotation = Quaternion.Euler(0f, 0f, currentAngle);
    }

    void PickNewDrunkForce()
    {
        float direction = Random.value > 0.5f ? 1f : -1f;
        currentDrunkForce = direction * drunkForce;
        forceTimer = forceChangeInterval + Random.Range(-0.5f, 0.5f);
    }
}