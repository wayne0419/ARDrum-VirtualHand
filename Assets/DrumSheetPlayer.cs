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

    public void Play(float skipBeat = 0f, float playBackBPM = 120f)
    {
        if (videoPlayer != null)
        {
            float playbackSpeedMultiplier = playBackBPM / drumSheetBPM;
            videoPlayer.playbackSpeed = playbackSpeedMultiplier;

            if (videoPlayer.isPlaying)
            {
                videoPlayer.Stop(); // 停止当前播放
            }

            videoPlayer.Play();

            // 将 skipBeat 转换为 drumSheetBPM 相关的时间
            float drumSheetStartTime = skipBeat * (60f / drumSheetBPM);
            videoPlayer.time = drumSheetStartTime;
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
