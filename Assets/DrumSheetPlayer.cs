using UnityEngine;
using UnityEngine.Video;

public class DrumSheetPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
    }

    public void Play(float startTime = 0f)
    {
        if (videoPlayer != null)
        {
            videoPlayer.time = startTime;
            videoPlayer.Play();
        }
    }

    public void Pause()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
        }
    }

    public void Stop()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
    }
}
