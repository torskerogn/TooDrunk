using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ResultScreen : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public int nextScene = 4; // change this for each winner/loser scene
    
    // Pair 1
    public GameObject loserPair1Prefab; // tabtelefon_0
    public GameObject winnerPair1Prefab; // vindøl_0
    
    // Pair 2
    public GameObject loserPair2Prefab; // tabcykel_0
    public GameObject winnerPair2Prefab; // vindcykel_0
    
    // Pair 3
    public GameObject loserPair3Prefab; // tabkaraoke_0
    public GameObject winnerPair3Prefab; // vindhjælp_0
    
    // Spawn positions
    public Vector3 loserSpawnPos = new Vector3(-5, 0, 0);
    public Vector3 winnerSpawnPos = new Vector3(5, 0, 0);
    
    private bool pair1Spawned = false;
    private bool pair2Spawned = false;
    private bool pair3Spawned = false;

    void Update()
    {
        if (!videoPlayer.isPrepared) return;
        
        double time = videoPlayer.time;
        
        // Pair 1 at 6 seconds
        if (time >= 6.0 && !pair1Spawned)
        {
            Instantiate(loserPair1Prefab, loserSpawnPos, Quaternion.identity);
            Instantiate(winnerPair1Prefab, winnerSpawnPos, Quaternion.identity);
            pair1Spawned = true;
        }
        
        // Pair 2 at 7 seconds
        if (time >= 7.0 && !pair2Spawned)
        {
            Instantiate(loserPair2Prefab, loserSpawnPos, Quaternion.identity);
            Instantiate(winnerPair2Prefab, winnerSpawnPos, Quaternion.identity);
            pair2Spawned = true;
        }
        
        // Pair 3 at 8 seconds
        if (time >= 8.0 && !pair3Spawned)
        {
            Instantiate(loserPair3Prefab, loserSpawnPos, Quaternion.identity);
            Instantiate(winnerPair3Prefab, winnerSpawnPos, Quaternion.identity);
            pair3Spawned = true;
        }
        
        // When video ends, go to next scene
        if (videoPlayer.isPrepared && !videoPlayer.isPlaying && videoPlayer.time > 0)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}