using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class WheelVideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public int targetGameScene = 2;
    private double jumpTime = 8.0; // changed to 8 seconds
    private float pauseDuration = 2.5f;
    private bool hasPaused = false;
    private float pauseStartTime = 0f;

    void Update()
    {
        if (videoPlayer.isPrepared && videoPlayer.time >= jumpTime && !hasPaused)
        {
            videoPlayer.Pause();
            hasPaused = true;
            pauseStartTime = Time.time;
        }

        if (hasPaused && Time.time >= pauseStartTime + pauseDuration)
        {
            SceneManager.LoadScene(targetGameScene);
        }
    }
}