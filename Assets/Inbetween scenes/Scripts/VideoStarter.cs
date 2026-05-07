using UnityEngine;
using UnityEngine.Video;

public class VideoStarter : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public float delayBeforePlay = 0.5f; // adjust this to 0.5-1.0 seconds

    void Start()
    {
        videoPlayer.playOnAwake = false;
        Invoke("PlayVideo", delayBeforePlay);
    }

    void PlayVideo()
    {
        videoPlayer.Play();
    }
}