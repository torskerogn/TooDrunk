using UnityEngine;
using System.Collections;

public class SpawnerOrange : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public float startInterval = 4f;
    public float endInterval = 1f;
    public float spawnVariance = 0.5f;
    public float launchForce = 15f;
    public float spawnXRange = 2.5f;
    public float maxAngle = 20f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (GameManager2.Instance.timeRemaining > 0)
        {
            float randomX = Random.Range(-spawnXRange, spawnXRange);
            Vector3 spawnPos = new Vector3(randomX, transform.position.y, 0);

            int randomIndex = Random.Range(0, itemPrefabs.Length);
            GameObject item = Instantiate(itemPrefabs[randomIndex], spawnPos, Quaternion.identity);

            float randomAngle = Random.Range(-maxAngle, maxAngle);
            Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.up;

            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1f;
            rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
            rb.angularVelocity = Random.Range(-200f, 200f);

            float progress = 1f - (GameManager2.Instance.timeRemaining / 60f);
            float currentInterval = Mathf.Lerp(startInterval, endInterval, progress);
            yield return new WaitForSeconds(currentInterval + Random.Range(-spawnVariance, spawnVariance));
        }
    }
}