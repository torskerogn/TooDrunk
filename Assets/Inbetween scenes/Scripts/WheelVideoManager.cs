using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class WheelVideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public int targetGameScene = 2;
    private double jumpTime = 12.0;
    private float pauseDuration = 2.5f;
    private bool hasPaused = false;
    private float pauseStartTime = 0f;

    void Update()
    {
        if (!videoPlayer.isPrepared) return;
        
        Debug.Log("Video time: " + videoPlayer.time); // THIS IS THE DEBUG CODE
        
        if (videoPlayer.time >= jumpTime && !hasPaused)
        {
            videoPlayer.Pause();
            hasPaused = true;
            pauseStartTime = Time.time;
            Debug.Log("Paused at: " + videoPlayer.time);
        }

        if (hasPaused && Time.time >= pauseStartTime + pauseDuration)
        {
            Debug.Log("Loading scene: " + targetGameScene);
            SceneManager.LoadScene(targetGameScene);
        }
    }
}