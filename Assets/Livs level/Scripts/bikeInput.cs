using UnityEngine;

public class BikeInput : MonoBehaviour
{
    public float counterForce = 80f;
    private BikeAutoTilt tilt;

    void Start()
    {
        tilt = GetComponent<BikeAutoTilt>();

        // Is the reference found at all?
        if (tilt == null)
            Debug.LogError("BikeAutoTilt NOT found on this GameObject!");
        else
            Debug.Log("BikeAutoTilt found OK");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Debug.Log("LEFT pressed");

        if (Input.GetKeyDown(KeyCode.RightArrow))
            Debug.Log("RIGHT pressed");

        if (tilt == null) return;

        if (Input.GetKey(KeyCode.LeftArrow))
            tilt.angularVelocity -= counterForce * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow))
            tilt.angularVelocity += counterForce * Time.deltaTime;
    }
}