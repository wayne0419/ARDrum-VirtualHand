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
        public float timestamp;
        public float bassDrumHit; // 新增 bassDrumHit
        public float snareDrumHit;
        public float closedHiHatHit;
        public float tom1Hit;
        public float tom2Hit;
        public float floorTomHit;
        public float crashHit;
        public float rideHit;
    }

    [Serializable]
    public class TransformPlaybackData
    {
        public float bpm;
        public List<TransformData> dataList;
    }

    public string jsonFilePath; // JSON 文件的路径
    public Transform targetTransform1;
    public Transform targetTransform2;
    public float playBackBPM; // 用于调整播放速度
    public Metronome metronome; // Metronome 组件引用

    public AudioSource bassDrumAudioSource; // Bass Drum 音效
    public AudioSource snareDrumAudioSource; // Snare Drum 音效
    public AudioSource closedHiHatAudioSource; // Closed Hi-Hat 音效
    public AudioSource tom1AudioSource; // Tom1 音效
    public AudioSource tom2AudioSource; // Tom2 音效
    public AudioSource floorTomAudioSource; // Floor Tom 音效
    public AudioSource crashAudioSource; // Crash 音效
    public AudioSource rideAudioSource; // Ride 音效

    private TransformPlaybackData playbackData;
    private int currentIndex;
    private float playbackStartTime;
    private float playbackSpeedMultiplier;
    public bool isPlaying = false; // 将 isPlaying 设置为 public

    void OnEnable()
    {
        // 每次启用时读取 JSON 文件
        LoadJsonFile(jsonFilePath);
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

        if (isPlaying)
        {
            if (playbackData == null || currentIndex >= playbackData.dataList.Count - 1)
            {
                StopPlayBack();
                return;
            }

            // 计算经过的时间，考虑播放速度倍率
            float elapsedTime = (Time.time - playbackStartTime) * playbackSpeedMultiplier;

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

                // 检查当前元素的击打事件，播放相应音效
                CheckAndPlayDrumHits(playbackData.dataList[currentIndex]);
            }
            else
            {
                // 如果已经播放到最后一个 Transform
                UpdateTransforms(currentIndex, currentIndex, 1.0f);
            }
        }
    }

    public void StartPlayBack()
    {
        if (playbackData != null)
        {
            playbackSpeedMultiplier = playBackBPM / playbackData.bpm;
        }
        else
        {
            playbackSpeedMultiplier = 1f;
        }

        playbackStartTime = Time.time;
        currentIndex = 0;
        isPlaying = true;

        // 开始播放 Metronome
        if (metronome != null)
        {
            metronome.bpm = playBackBPM;
            metronome.StartMetronome();
        }
    }

    public void StopPlayBack()
    {
        isPlaying = false;

        // 停止播放 Metronome
        if (metronome != null)
        {
            metronome.StopMetronome();
        }
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
    }

    void CheckAndPlayDrumHits(TransformData data)
    {
        if (data.bassDrumHit > 0f) PlayDrumHit(bassDrumAudioSource, data.bassDrumHit);
        if (data.snareDrumHit > 0f) PlayDrumHit(snareDrumAudioSource, data.snareDrumHit);
        if (data.closedHiHatHit > 0f) PlayDrumHit(closedHiHatAudioSource, data.closedHiHatHit);
        if (data.tom1Hit > 0f) PlayDrumHit(tom1AudioSource, data.tom1Hit);
        if (data.tom2Hit > 0f) PlayDrumHit(tom2AudioSource, data.tom2Hit);
        if (data.floorTomHit > 0f) PlayDrumHit(floorTomAudioSource, data.floorTomHit);
        if (data.crashHit > 0f) PlayDrumHit(crashAudioSource, data.crashHit);
        if (data.rideHit > 0f) PlayDrumHit(rideAudioSource, data.rideHit);
    }

    void PlayDrumHit(AudioSource audioSource, float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume; // 设置音量
            audioSource.Stop(); // 停止当前播放的 clip
            audioSource.Play(); // 播放新的 clip
        }
    }
}
