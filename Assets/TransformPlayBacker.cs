using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TransformPlayBacker : MonoBehaviour
{
    [Serializable]
    public class TransformData
    {
        public Vector3 position1;
        public Quaternion rotation1;
        public Vector3 position2;
        public Quaternion rotation2;
        public Vector3 position3; // 新增第三组 position
        public Quaternion rotation3; // 新增第三组 rotation
        public float timestamp;
        public float bassDrumHit;
        public float snareDrumHit;
        public float closedHiHatHit;
        public float tom1Hit;
        public float tom2Hit;
        public float floorTomHit;
        public float crashHit;
        public float rideHit;
        public float openHiHatHit; // 新增 openHiHatHit
    }

    [Serializable]
    public class TransformPlaybackData
    {
        public float bpm;
        public List<TransformData> dataList;
    }

    public enum PlayMode { A, B } // 新增播放模式枚举
    public PlayMode playMode = PlayMode.A; // 默认播放模式为 A

    public string jsonFilePath; // JSON 文件的路径
    public Transform targetTransform1;
    public Transform targetTransform2;
    public Transform targetTransform3; // 新增第三个 Transform
    public float playBackBPM; // 用于调整播放速度
    public float startOffsetBeat; // 新增 startOffsetBeat
    public Metronome metronome; // Metronome 组件引用

    public AudioSource bassDrumAudioSource; // Bass Drum 音效
    public AudioSource snareDrumAudioSource; // Snare Drum 音效
    public AudioSource closedHiHatAudioSource; // Closed Hi-Hat 音效
    public AudioSource tom1AudioSource; // Tom1 音效
    public AudioSource tom2AudioSource; // Tom2 音效
    public AudioSource floorTomAudioSource; // Floor Tom 音效
    public AudioSource crashAudioSource; // Crash 音效
    public AudioSource rideAudioSource; // Ride 音效
    public AudioSource openHiHatAudioSource; // Open Hi-Hat 音效

    public DrumHitIndicator drumHitIndicator; // 新增 DrumHitIndicator 组件引用
    public DrumSheetPlayer drumSheetPlayer; // 新增 DrumSheetPlayer 组件引用

    private TransformPlaybackData playbackData;
    private int currentIndex;
    private float playbackSpeedMultiplier;
    private float offsetDuration;
    public bool isPlaying = false; // 将 isPlaying 设置为 public
    private Coroutine playbackCoroutine;
    private float playbackStartTime; // 确保声明 playbackStartTime

    void OnEnable()
    {
        // 每次启用时读取 JSON 文件
        LoadJsonFile(jsonFilePath);

        // 初始化 drumHitIndicator
        if (drumHitIndicator != null)
        {
            drumHitIndicator.Initialize();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isPlaying)
            {
                StartPlayBack();
            }
            else
            {
                StopPlayBack();
            }
        }
    }

    public void StartPlayBack()
    {
        if (playbackData != null)
        {
            playbackSpeedMultiplier = playBackBPM / playbackData.bpm;
            offsetDuration = 60f / playBackBPM * startOffsetBeat;
        }
        else
        {
            playbackSpeedMultiplier = 1f;
            offsetDuration = 0f;
        }

        isPlaying = true;

        // 根据播放模式启动不同的 coroutine
        if (playMode == PlayMode.A)
        {
            playbackCoroutine = StartCoroutine(PlayBackCoroutineA());
        }
        else if (playMode == PlayMode.B)
        {
            playbackCoroutine = StartCoroutine(PlayBackCoroutineB());
        }
    }

    public void StopPlayBack()
    {
        if (playbackCoroutine != null)
        {
            StopCoroutine(playbackCoroutine);
        }

        isPlaying = false;

        // 停止播放 Metronome 和 Drum Sheet
        if (metronome != null)
        {
            metronome.StopMetronome();
        }

        if (drumSheetPlayer != null)
        {
            drumSheetPlayer.Pause();
        }
    }

    private IEnumerator PlayBackCoroutineA()
    {
        while (isPlaying)
        {
            // 开始播放 Metronome
            if (metronome != null)
            {
                metronome.bpm = playBackBPM; // 确保设置 bpm
                metronome.StopMetronome(); // 确保 metronome 停止
                metronome.StartMetronome(); // 重新启动 metronome
            }

            // 播放空白记录
            yield return new WaitForSeconds(offsetDuration);

            playbackStartTime = Time.time;
            currentIndex = 0;

            // 开始播放 Drum Sheet 影片
            if (drumSheetPlayer != null)
            {
                drumSheetPlayer.Play(0f, playBackBPM); // 从头开始播放，并调整速度
            }

            // 使用 PlayTransformData 简化代码
            yield return PlayTransformData();
        }
    }

    private IEnumerator PlayBackCoroutineB()
    {
        // 开始播放 Metronome
        if (metronome != null)
        {
            metronome.bpm = playBackBPM; // 确保设置 bpm
            metronome.StopMetronome(); // 确保 metronome 停止
            metronome.StartMetronome(); // 重新启动 metronome
        }

        // 播放空白记录
        yield return new WaitForSeconds(offsetDuration);

        playbackStartTime = Time.time;
        currentIndex = 0;

        // 开始播放 Drum Sheet 影片
        if (drumSheetPlayer != null)
        {
            drumSheetPlayer.Play(0f, playBackBPM); // 从头开始播放，并调整速度
        }

        // 播放整个 TransformPlayBackData 一次
        yield return PlayTransformData();

        // 循环播放 TransformPlayBackData 跳过开头一个 beat 的时间
        while (isPlaying)
        {
            playbackStartTime = Time.time;

            float skipTime = 60f / playbackData.bpm;

            // 重置 Metronome
            if (metronome != null)
            {
                metronome.bpm = playBackBPM; // 确保设置 bpm
                metronome.StopMetronome(); // 确保 metronome 停止
                metronome.StartMetronome(); // 重新启动 metronome
            }

            // 开始播放 Drum Sheet 影片并跳过开头一个 beat 的时间
            if (drumSheetPlayer != null)
            {
                drumSheetPlayer.Play(skipTime, playBackBPM);
            }

            // 调用 PlayTransformData 时传递 skipTime 以跳过开头一个 beat 的时间
            yield return PlayTransformData(skipTime);
        }
    }

    private IEnumerator PlayTransformData(float startTimeOffset = 0f)
    {
        if (startTimeOffset > 0)
        {
            // 计算跳过时间后应该从哪个索引开始播放
            currentIndex = GetStartIndexAfterSkipTime(startTimeOffset);
        }

        while (currentIndex < playbackData.dataList.Count - 1)
        {
            float elapsedTime = (Time.time - playbackStartTime) * playbackSpeedMultiplier + startTimeOffset;

            // 检查是否跳过了多个元素
            while (currentIndex < playbackData.dataList.Count - 1 && elapsedTime >= playbackData.dataList[currentIndex + 1].timestamp)
            {
                // 如果被跳过的元素中有击打事件，播放相应音效
                CheckAndPlayDrumHits(playbackData.dataList[currentIndex]);
                currentIndex++;
            }

            if (currentIndex < playbackData.dataList.Count - 1)
            {
                float targetTime = playbackData.dataList[currentIndex].timestamp;
                float nextTime = playbackData.dataList[currentIndex + 1].timestamp;

                // 计算插值因子
                float t = Mathf.InverseLerp(targetTime, nextTime, elapsedTime);

                // 使用线性插值更新 Transform
                UpdateTransforms(currentIndex, currentIndex + 1, t);
            }

            yield return null;
        }

        // 播放到最后一个 Transform
        UpdateTransforms(currentIndex, currentIndex, 1.0f);
    }

    private int GetStartIndexAfterSkipTime(float skipTime)
    {
        // 找到第一个 timestamp 大于 skipTime 的索引
        for (int i = 0; i < playbackData.dataList.Count; i++)
        {
            if (playbackData.dataList[i].timestamp >= skipTime)
            {
                return i;
            }
        }
        return 0; // 如果所有 timestamp 都小于 skipTime，则从头开始
    }

    void LoadJsonFile(string path)
    {
        if (File.Exists(path))
        {
            string jsonContent = File.ReadAllText(path);
            playbackData = JsonUtility.FromJson<TransformPlaybackData>(jsonContent);
        }
        else
        {
            Debug.LogError("JSON file not found: " + path);
        }
    }

    void UpdateTransforms(int indexA, int indexB, float t)
    {
        TransformData dataA = playbackData.dataList[indexA];
        TransformData dataB = playbackData.dataList[indexB];

        targetTransform1.position = Vector3.Lerp(dataA.position1, dataB.position1, t);
        targetTransform1.rotation = Quaternion.Lerp(dataA.rotation1, dataB.rotation1, t);
        targetTransform2.position = Vector3.Lerp(dataA.position2, dataB.position2, t);
        targetTransform2.rotation = Quaternion.Lerp(dataA.rotation2, dataB.rotation2, t);
        targetTransform3.position = Vector3.Lerp(dataA.position3, dataB.position3, t); // 更新第三个 Transform 的位置
        targetTransform3.rotation = Quaternion.Lerp(dataA.rotation3, dataB.rotation3, t); // 更新第三个 Transform 的旋转
    }

    void CheckAndPlayDrumHits(TransformData data)
    {
        if (data.bassDrumHit > 0f) 
        {
            PlayDrumHit(bassDrumAudioSource, data.bassDrumHit);
            if (drumHitIndicator != null) 
            {
                drumHitIndicator.TriggerBassDrum();
            }
        }
        if (data.snareDrumHit > 0f)
        {
            PlayDrumHit(snareDrumAudioSource, data.snareDrumHit);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerSnareDrum(); // 触发 Snare Drum Indicator 效果
            }
        }
        if (data.closedHiHatHit > 0f) 
        {
            PlayDrumHit(closedHiHatAudioSource, data.closedHiHatHit);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerClosedHiHat();
            }
        }
        if (data.tom1Hit > 0f) 
        {
            PlayDrumHit(tom1AudioSource, data.tom1Hit);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerTom1();
            }
        }
        if (data.tom2Hit > 0f) 
        {
            PlayDrumHit(tom2AudioSource, data.tom2Hit);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerTom2();
            }
        }
        if (data.floorTomHit > 0f) 
        {
            PlayDrumHit(floorTomAudioSource, data.floorTomHit);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerFloorTom();
            }
        }
        if (data.crashHit > 0f) 
        {
            PlayDrumHit(crashAudioSource, data.crashHit);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerCrash();
            }
        }
        if (data.rideHit > 0f) 
        {
            PlayDrumHit(rideAudioSource, data.rideHit);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerRide();
            }
        }
        if (data.openHiHatHit > 0f) 
        {
            PlayDrumHit(openHiHatAudioSource, data.openHiHatHit); // 处理 openHiHatHit
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerOpenHiHat();
            }
        }
    }

    void PlayDrumHit(AudioSource audioSource, float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume; // 设置音量
            audioSource.PlayOneShot(audioSource.clip); // 播放新的 clip
        }
    }
}
