using UnityEngine;

public class ItemBlue : MonoBehaviour
{
    public int pointValue = 1;
    public GameObject explosionPrefab;

    public void Tapped()
    {
        GameManager2.Instance.AddScore("blue", pointValue);

        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        if (pointValue < 0)
            ScreenShake.Instance.Shake(0.3f, 0.3f);

        Destroy(gameObject);
    }

    void Update()
    {
        if (transform.position.y > 13f)
        {
            if (pointValue > 0)
                GameManager2.Instance.AddScore("blue", -1);
                Destroy(gameObject);
        }
    }
}