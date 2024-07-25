using UnityEngine;
using UnityEngine.Video;

public class DrumSheetPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public float drumSheetBPM; // Drum sheet 影片的原始 BPM

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
    }

    public void Play(float startTime = 0f, float playBackBPM = 120f)
    {
        if (videoPlayer != null)
        {
            videoPlayer.time = startTime;
            float playbackSpeedMultiplier = playBackBPM / drumSheetBPM;
            videoPlayer.playbackSpeed = playbackSpeedMultiplier;
            videoPlayer.Play();
        }
        Debug.Log("Play Drum Sheet!");
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
