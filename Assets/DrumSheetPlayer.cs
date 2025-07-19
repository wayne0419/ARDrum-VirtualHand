using UnityEngine;
using UnityEngine.Video;

public class DrumSheetPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component.
    public float drumSheetBPM; // The original Beats Per Minute (BPM) of the drum sheet video.

    void Start()
    {
        // If the videoPlayer is not assigned in the Inspector, try to get it from this GameObject.
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
    }

    /// <summary>
    /// Plays the drum sheet video, adjusting its playback speed and starting position.
    /// </summary>
    /// <param name="skipBeat">The beat position from which to start playback. Defaults to 0 (beginning).</param>
    /// <param name="playBackBPM">The desired playback BPM. The video's speed will be adjusted relative to its original BPM. Defaults to 120 BPM.</param>
    public void Play(float skipBeat = 0f, float playBackBPM = 120f)
    {
        if (videoPlayer != null)
        {
            // Calculate the playback speed multiplier to match the desired BPM.
            // If playBackBPM is higher than drumSheetBPM, the video will play faster.
            float playbackSpeedMultiplier = playBackBPM / drumSheetBPM;
            videoPlayer.playbackSpeed = playbackSpeedMultiplier;

            // If the video is currently playing, stop it before starting a new playback.
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Stop(); 
            }

            videoPlayer.Play();

            // Convert the 'skipBeat' (in terms of the original drum sheet BPM) into video time.
            // (60f / drumSheetBPM) gives the duration of one beat in seconds at the original BPM.
            float drumSheetStartTime = skipBeat * (60f / drumSheetBPM);
            videoPlayer.time = drumSheetStartTime;
        }
    }

    /// <summary>
    /// Pauses the currently playing drum sheet video.
    /// </summary>
    public void Pause()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
        }
    }

    /// <summary>
    /// Stops the drum sheet video and resets its playback position to the beginning.
    /// </summary>
    public void Stop()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
    }
}