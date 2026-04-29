using UnityEngine;

public class DrunkTilt : MonoBehaviour
{
    public float tiltAngle = 20f;
    public float tiltSpeed = 1.5f;

    private Vector3 pivotOffset;

    void Start()
    {
        // Anchor point at the bottom of the sprite
        // Assumes the sprite is centered, so bottom is -half the height
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float halfHeight = sr.bounds.extents.y;
        pivotOffset = new Vector3(0, -halfHeight, 0);
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * tiltSpeed) * tiltAngle;

        // Rotate around the bottom anchor point
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = transform.parent != null
            ? transform.parent.TransformPoint(pivotOffset)
            : transform.position;
    }
}
