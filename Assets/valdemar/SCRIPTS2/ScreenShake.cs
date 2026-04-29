using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;
    public Camera blueCamera;
    public Camera orangeCamera;

    private Vector3 blueOriginalPos;
    private Vector3 orangeOriginalPos;

    void Awake()
    {
        Instance = this;
        blueOriginalPos = blueCamera.transform.localPosition;
        orangeOriginalPos = orangeCamera.transform.localPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(DoShake(duration, magnitude));
    }

    IEnumerator DoShake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            blueCamera.transform.localPosition = blueOriginalPos + new Vector3(x, y, 0);
            orangeCamera.transform.localPosition = orangeOriginalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        blueCamera.transform.localPosition = blueOriginalPos;
        orangeCamera.transform.localPosition = orangeOriginalPos;
    }
}