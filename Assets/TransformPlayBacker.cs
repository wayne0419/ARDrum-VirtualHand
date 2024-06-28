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
    }

    [Serializable]
    public class TransformPlaybackData
    {
        public float bpm;
        public List<TransformData> dataList;
    }

    public string jsonFilePath; // JSON 文件的路徑
    public Transform targetTransform1;
    public Transform targetTransform2;
    public float playBackBPM; // 用於調整播放速度
    public Metronome metronome; // Metronome 組件引用

    private TransformPlaybackData playbackData;
    private int currentIndex;
    private float playbackStartTime;
    private float playbackSpeedMultiplier;
    public bool isPlaying = false; // 將 isPlaying 設置為 public

    void OnEnable()
    {
        // 讀取 JSON 文件
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

            // 計算經過的時間，考慮播放速度倍率
            float elapsedTime = (Time.time - playbackStartTime) * playbackSpeedMultiplier;
            float targetTime = playbackData.dataList[currentIndex].timestamp;
            float nextTime = playbackData.dataList[currentIndex + 1].timestamp;

            // 計算插值因子
            float t = Mathf.InverseLerp(targetTime, nextTime, elapsedTime);

            // 使用線性插值更新 Transform
            UpdateTransforms(currentIndex, currentIndex + 1, t);

            // 檢查是否需要移動到下一個時間點
            if (elapsedTime >= nextTime)
            {
                currentIndex++;
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

        // 開始播放 Metronome
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
}
