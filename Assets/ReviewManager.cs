using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReviewManager : MonoBehaviour
{
    public string userRecordFilePath;
    public string targetRecordFilePath;

    public TransformRecorder.TransformDataList userTransformDataList;
    public TransformRecorder.TransformDataList targetTransformDataList;

    private void Start()
    {
        LoadTransformData(userRecordFilePath, out userTransformDataList);
        LoadTransformData(targetRecordFilePath, out targetTransformDataList);
    }

    private void LoadTransformData(string filePath, out TransformRecorder.TransformDataList transformDataList)
    {
        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            transformDataList = JsonUtility.FromJson<TransformRecorder.TransformDataList>(jsonContent);
            Debug.Log($"Loaded transform data from {filePath}");
        }
        else
        {
            transformDataList = null;
            Debug.LogError($"File not found: {filePath}");
        }
    }
}
