using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ResultScreen : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public int nextScene = 4;
    public float pauseAfterPair3 = 5f; // 5 seconds after pair 3
    
    // Pair 1
    public GameObject loserPair1Prefab;
    public GameObject winnerPair1Prefab;
    public Vector3 loserPair1Pos = new Vector3(-5, 5, 0);
    public Vector3 loserPair1Rot = new Vector3(0, 0, 0);
    public Vector3 winnerPair1Pos = new Vector3(-5, -5, 0);
    
    // Pair 2
    public GameObject loserPair2Prefab;
    public GameObject winnerPair2Prefab;
    public Vector3 loserPair2Pos = new Vector3(0, 5, 0);
    public Vector3 loserPair2Rot = new Vector3(0, 0, 0);
    public Vector3 winnerPair2Pos = new Vector3(0, -5, 0);
    
    // Pair 3
    public GameObject loserPair3Prefab;
    public GameObject winnerPair3Prefab;
    public Vector3 loserPair3Pos = new Vector3(5, 5, 0);
    public Vector3 loserPair3Rot = new Vector3(0, 0, 0);
    public Vector3 winnerPair3Pos = new Vector3(5, -5, 0);
    
    private bool pair1Spawned = false;
    private bool pair2Spawned = false;
    private bool pair3Spawned = false;
    private float pair3SpawnTime = 0f;

    void Update()
    {
        if (!videoPlayer.isPrepared) return;
        
        double time = videoPlayer.time;
        
        // Pair 1 at 6 seconds
        if (time >= 6.0 && time < 6.5 && !pair1Spawned)
        {
            Instantiate(loserPair1Prefab, loserPair1Pos, Quaternion.Euler(loserPair1Rot));
            Instantiate(winnerPair1Prefab, winnerPair1Pos, Quaternion.identity);
            pair1Spawned = true;
        }
        
        // Pair 2 at 7 seconds
        if (time >= 7.0 && time < 7.5 && !pair2Spawned)
        {
            Instantiate(loserPair2Prefab, loserPair2Pos, Quaternion.Euler(loserPair2Rot));
            Instantiate(winnerPair2Prefab, winnerPair2Pos, Quaternion.identity);
            pair2Spawned = true;
        }
        
        // Pair 3 at 8 seconds
        if (time >= 8.0 && time < 8.5 && !pair3Spawned)
        {
            Instantiate(loserPair3Prefab, loserPair3Pos, Quaternion.Euler(loserPair3Rot));
            Instantiate(winnerPair3Prefab, winnerPair3Pos, Quaternion.identity);
            pair3Spawned = true;
            pair3SpawnTime = Time.time;
        }
        
        // 5 seconds after pair 3 spawns, go to next scene
        if (pair3Spawned && Time.time >= pair3SpawnTime + pauseAfterPair3)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}