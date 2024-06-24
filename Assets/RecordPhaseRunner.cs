using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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

public class RecordPhaseRunner : PhaseRunner
{
    [Header("Record Settings")]
    public Transform targetTransform;
    public string folderPath = "Assets/RecordedTransforms"; // 指定資料夾路徑
    public float recordInterval = 0.1f; // 記錄間隔時間
    public float recordDelay;
    public float recotdDuration;
    
    [Header("Debug(Dont modify from inspector)")]
    [SerializeField]
    private float timeSinceLastRecord = 0f;
    [SerializeField]
    private bool isRecording = false;
    [SerializeField]
    private float recordingStartTime = 0f;
    private List<TransformData> transformDataList = new List<TransformData>();

    public override void StartPhaseRunner() {
        isRunning = true;
        timeSinceLastRecord = 0f;
        isRecording = false;
        recordingStartTime = 0f;
    }

    public override void StopPhaseRunner() {
        isRunning = false;
    }

    void Update()
    {
        if (!isRunning)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartRecordingAfterDelay(recordDelay, recotdDuration)); // 延遲 recordDelay秒開始記錄 recordDuration 秒
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
        float timestamp = Time.time - recordingStartTime;
        TransformData data = new TransformData(targetTransform.position, targetTransform.rotation, timestamp);
        transformDataList.Add(data);
    }

    IEnumerator StartRecordingAfterDelay(float delay, float duration)
    {
        yield return new WaitForSeconds(delay);
        isRecording = true;
        recordingStartTime = Time.time;
        transformDataList.Clear(); // 清除之前的記錄
        yield return new WaitForSeconds(duration);
        isRecording = false;
        SaveTransformData();
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
        string json = JsonUtility.ToJson(new TransformDataList(transformDataList), true);
        File.WriteAllText(filePath, json);

        Debug.Log("Transform data saved to " + filePath);
    }

    [System.Serializable]
    public class TransformDataList
    {
        public List<TransformData> dataList;

        public TransformDataList(List<TransformData> list)
        {
            dataList = list;
        }
    }
}
