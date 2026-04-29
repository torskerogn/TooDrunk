using UnityEngine;

public class BikeBalance : MonoBehaviour
{
    [Header("Balance")]
    public float gravity = 120f;
    public float playerForce = 200f;
    public float damping = 0.995f;
    public float fallAngle = 45f;

    [Header("Instability")]
    public float randomDrift = 10f;   // konstant uro

    [Header("Recovery")]
    public float resetSpeed = 60f;

    private float angle = 0f;
    private float angularVelocity = 0f;
    private bool isFallen = false;

    void Start()
    {
        // 🔥 Giv den et lille start-skub
        angle = Random.Range(-5f, 5f);
    }

    void Update()
    {
        if (isFallen)
        {
            Recover();
            return;
        }

        ApplyGravity();
        ApplyDrift();
        HandleInput();
        Simulate();
        ApplyRotation();
        CheckFall();
    }

    void ApplyGravity()
    {
        angularVelocity += Mathf.Sin(angle * Mathf.Deg2Rad) * gravity * Time.deltaTime;
    }

    void ApplyDrift()
    {
        // 🔥 Lille konstant tilfældig påvirkning
        angularVelocity += Random.Range(-randomDrift, randomDrift) * Time.deltaTime;
    }

    void HandleInput()
    {
        float input = Input.GetAxis("Horizontal");
        angularVelocity -= input * playerForce * Time.deltaTime;
    }

    void Simulate()
    {
        angularVelocity *= damping;
        angle += angularVelocity * Time.deltaTime;
    }

    void ApplyRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void CheckFall()
    {
        if (Mathf.Abs(angle) > fallAngle)
            isFallen = true;
    }

    void Recover()
    {
        angle = Mathf.MoveTowards(angle, 0f, resetSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Mathf.Abs(angle) < 1f)
        {
            angle = Random.Range(-5f, 5f); // starter ustabil igen
            angularVelocity = 0f;
            isFallen = false;
        }
    }
}