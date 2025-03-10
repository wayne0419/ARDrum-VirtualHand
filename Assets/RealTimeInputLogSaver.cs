using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RealTimeInputLogSaver : MonoBehaviour
{
    public RealTimeInputTracker inputTracker; // Reference to the RealTimeInputTracker component
    public CorrectRateCalculator correctRateCalculator;
    public string directoryPath;

    private void Update()
    {
        // Check if the space key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SaveInputLog();
        }
    }

    private void SaveInputLog()
    {
        // Create the InputLogs directory if it doesn't exist
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Prepare the data to be saved
        InputLogData logData = new InputLogData
        {
            playBackBPM = inputTracker.transformPlayBacker.playBackBPM,
            correctRate = correctRateCalculator.correctRate,
            jsonFilePath = inputTracker.transformPlayBacker.jsonFilePath,
            inputLog = inputTracker.inputLog
        };

        // 檔名
        string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
        int fileCount = Directory.GetFiles(directoryPath, "*.json").Length;
        string filePath = Path.Combine(directoryPath, fileCount + "_" + inputTracker.transformPlayBacker.playBackBPM + "bpm_" + $"{correctRateCalculator.correctRate:P0}_" + timestamp + ".json");

        // Convert the data to JSON and save it to the file
        string json = JsonUtility.ToJson(logData, true);
        File.WriteAllText(filePath, json);

        Debug.Log($"Input log saved to: {filePath}");
    }

    [System.Serializable]
    public class InputLogData
    {
        
        public float playBackBPM;
        public float correctRate;
        public string jsonFilePath;
        public List<RealTimeInputTracker.HitDrumInputData> inputLog;
    }
}
