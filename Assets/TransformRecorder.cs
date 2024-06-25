using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TransformRecorder : MonoBehaviour
{
    // 記錄 position 和 rotation 的結構體
    [System.Serializable]
    public struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;
        public float timestamp;

        public TransformData(Vector3 pos, Quaternion rot, float time)
        {
            position = pos;
            rotation = rot;
            timestamp = time;
        }
    }
    [Header("Reference")]
    public Metronome metronome; // 參考 Metronome 組件

    [Header("Record Settings")]
    public Transform targetTransform; // 要記錄的 Transform
    public string folderPath = "Assets/RecordedTransforms"; // 指定資料夾路徑
    public float recordInterval = 0.1f; // 記錄間隔時間
    public float bpm = 120f; // beats per minute
    public float recordDelayBeats = 4f; // 延遲的 beats 數量
    public float recordDurationBeats = 4f; // 記錄持續的 beats 數量
    
    [Header("Debug(dont modify from inspector)")]
    [SerializeField]
    private float timeSinceLastRecord = 0f;
    [SerializeField]
    private bool isRecording = false;
    [SerializeField]
    private bool isRecordingInProgress = false;
    [SerializeField]
    private float recordingStartTime = 0f;
    private List<TransformData> transformDataList = new List<TransformData>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRecordingInProgress)
        {
            if (metronome != null)
            {
                metronome.bpm = bpm;
                metronome.StartMetronome();
            }
            StartCoroutine(StartRecordingAfterBeats(recordDelayBeats, recordDurationBeats));
        }

        if (isRecording)
        {
            timeSinceLastRecord += Time.deltaTime;

            if (timeSinceLastRecord >= recordInterval)
            {
                RecordTransform();
                timeSinceLastRecord = 0f;
            }
        }
    }

    void RecordTransform()
    {
        if (targetTransform != null)
        {
            float timestamp = Time.time - recordingStartTime;
            TransformData data = new TransformData(targetTransform.position, targetTransform.rotation, timestamp);
            transformDataList.Add(data);
        }
    }

    System.Collections.IEnumerator StartRecordingAfterBeats(float delayBeats, float recordBeats)
    {
        isRecordingInProgress = true;
        float beatDuration = 60f / bpm;
        yield return new WaitForSeconds(delayBeats * beatDuration);
        isRecording = true;
        recordingStartTime = Time.time;
        transformDataList.Clear(); // 清除之前的記錄
        timeSinceLastRecord = 0f; // 重置時間
        yield return new WaitForSeconds(recordBeats * beatDuration);
        isRecording = false;
        SaveTransformData();

        if (metronome != null)
        {
            metronome.StopMetronome();
        }

        isRecordingInProgress = false;
    }

    public void SaveTransformData()
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // 獲取當前資料夾中的文件數
        int fileCount = Directory.GetFiles(folderPath, "*.json").Length;
        string filePath = Path.Combine(folderPath, fileCount + ".json");

        // 將 transformDataList 轉換為 JSON 格式並寫入文件
        TransformDataList dataList = new TransformDataList(transformDataList, bpm);
        string json = JsonUtility.ToJson(dataList, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Transform data saved to " + filePath);
    }

    [System.Serializable]
    public class TransformDataList
    {
        public List<TransformData> dataList;
        public float bpm;

        public TransformDataList(List<TransformData> list, float bpmValue)
        {
            dataList = list;
            bpm = bpmValue;
        }
    }
}