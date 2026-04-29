using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public float minSpawnTime = 0.5f;
    public float maxSpawnTime = 1.5f;
    public float launchForce = 5f;
    public float spawnXRange = 2.5f;
    

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (GameManager.Instance.timeRemaining > 0)
        {
            float randomX = Random.Range(-spawnXRange, spawnXRange);
            Vector3 spawnPos = new Vector3(randomX, transform.position.y, 0);

            int randomIndex = Random.Range(0, itemPrefabs.Length);
            GameObject item = Instantiate(itemPrefabs[randomIndex], spawnPos, Quaternion.identity);

            float aimX = -randomX * 0.1f + Random.Range(-0.3f, 0.3f);
            Vector2 direction = new Vector2(aimX, 1f).normalized;

            item.GetComponent<Rigidbody2D>().AddForce(direction * launchForce, ForceMode2D.Impulse);
            item.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-200f, 200f);

            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }
}