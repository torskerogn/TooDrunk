using UnityEngine;

public class Item : MonoBehaviour
{
    public int pointValue = 1; // set to -10 on beer prefab

    void OnMouseDown()
    {
        GameManager.Instance.AddScore(pointValue);
        Destroy(gameObject);
    }

    void Update()
    {
        // destroy if it falls off screen (below Y = -6 adjust as needed)
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}