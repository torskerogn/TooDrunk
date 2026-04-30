using System.Collections;
using UnityEngine;

public class HelmetMover : MonoBehaviour
{
    public PlayerSide owner = PlayerSide.Bottom;
    public float gravityScale = 2f;
    // World-Y the helmet should JUST reach at peak height (set just above the drunk's head).
    // For Top owner, set this BELOW the start position (their "up" is world-down) and use a
    // negative gravityScale so gravity pulls them back toward home.
    public float throwApexY = 2f;
    // Cap on the horizontal flick speed so the helmet can't fly out of the scene sideways.
    public float maxHorizontalSpeed = 6f;
    // How long the helmet sits on the head after a successful throw before returning home.
    public float celebrationSeconds = 0.6f;
    // Offset from the HeadTarget center where the helmet sits when mounted.
    public Vector3 mountOffset = new Vector3(0f, 0.3f, 0f);

    [HideInInspector] public bool isLaunched = false;
    [HideInInspector] public bool isMounted = false;

    private Rigidbody2D rb;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;
    private HeadTarget currentHead = null;
    private bool returnsArmed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;
        SetKinematic();
    }

    void Update()
    {
        if (!isLaunched) return;

        bool launchesUpInWorld = owner == PlayerSide.Bottom;
        float homeY = originalPosition.y;
        float currentY = transform.position.y;

        if (!returnsArmed)
        {
            if (launchesUpInWorld ? currentY > homeY : currentY < homeY)
                returnsArmed = true;
        }
        else
        {
            if (launchesUpInWorld ? currentY < homeY : currentY > homeY)
                ResetHelmet();
        }
    }

    public void StartDrag()
    {
        StopAllCoroutines();
        SetKinematic();
        isLaunched = false;
        isMounted = false;
    }

    public void MoveKinematic(Vector2 position)
    {
        rb.MovePosition(position);
    }

    public void Release(Vector2 velocity)
    {
        float effectiveG = Mathf.Abs(gravityScale * Physics2D.gravity.y);
        if (effectiveG > 0f)
        {
            float dy = Mathf.Abs(throwApexY - transform.position.y);
            float vyMax = Mathf.Sqrt(2f * effectiveG * dy);
            if (owner == PlayerSide.Bottom)
                velocity.y = Mathf.Min(velocity.y, vyMax);
            else
                velocity.y = Mathf.Max(velocity.y, -vyMax);
        }
        velocity.x = Mathf.Clamp(velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);

        rb.isKinematic = false;
        rb.gravityScale = gravityScale;
        rb.linearVelocity = velocity;
        isLaunched = true;
        returnsArmed = false;
    }

    public void MountHelmet(Transform headTransform)
    {
        isMounted = true;
        isLaunched = false;
        currentHead = headTransform.GetComponent<HeadTarget>();
        transform.SetParent(headTransform);
        transform.localPosition = mountOffset;
        SetKinematic();

        if (CoordinationFlow.Instance != null)
            CoordinationFlow.Instance.AddScore(owner);

        StartCoroutine(CelebrateThenReturn());
    }

    IEnumerator CelebrateThenReturn()
    {
        yield return new WaitForSeconds(celebrationSeconds);
        ResetHelmet();
    }

    public void CancelDrag()
    {
        ResetHelmet();
    }

    public void ResetHelmet()
    {
        StopAllCoroutines();

        if (currentHead != null)
        {
            currentHead.UnmountHelmet();
            currentHead = null;
        }

        isMounted = false;
        isLaunched = false;
        returnsArmed = false;
        transform.SetParent(null);
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        transform.localScale = originalScale;
        SetKinematic();
    }

    private void SetKinematic()
    {
        rb.isKinematic = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
