using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReviewManager : MonoBehaviour
{
    // 用戶記錄檔案的路徑
    public string userRecordFilePath;
    // 目標記錄檔案的路徑
    public string targetRecordFilePath;

    // 用戶的變換數據列表
    public TransformRecorder.TransformDataList userTransformDataList;
    // 目標的變換數據列表
    public TransformRecorder.TransformDataList targetTransformDataList;

    // 調整後的 BPM 值
    public float reviewBPM;

    // 各種鼓的打擊數據列表，儲存每種鼓的 hit value > 0 的 transformData 及其索引
    public List<TransformDataIndex> userSnareDrumHits;
    public List<TransformDataIndex> userBassDrumHits;
    public List<TransformDataIndex> userClosedHiHatHits;
    public List<TransformDataIndex> userTom1Hits;
    public List<TransformDataIndex> userTom2Hits;
    public List<TransformDataIndex> userFloorTomHits;
    public List<TransformDataIndex> userCrashHits;
    public List<TransformDataIndex> userRideHits;
    public List<TransformDataIndex> userOpenHiHatHits;

    public List<TransformDataIndex> targetSnareDrumHits;
    public List<TransformDataIndex> targetBassDrumHits;
    public List<TransformDataIndex> targetClosedHiHatHits;
    public List<TransformDataIndex> targetTom1Hits;
    public List<TransformDataIndex> targetTom2Hits;
    public List<TransformDataIndex> targetFloorTomHits;
    public List<TransformDataIndex> targetCrashHits;
    public List<TransformDataIndex> targetRideHits;
    public List<TransformDataIndex> targetOpenHiHatHits;

    // TransformData 和其索引的類
    [System.Serializable]
    public class TransformDataIndex
    {
        public TransformRecorder.TransformData data;
        public int index;

        public TransformDataIndex(TransformRecorder.TransformData data, int index)
        {
            this.data = data;
            this.index = index;
        }
    }

    private void OnEnable()
    {
        // 加載用戶和目標的變換數據
        LoadTransformData(userRecordFilePath, out userTransformDataList);
        LoadTransformData(targetRecordFilePath, out targetTransformDataList);

        // 調整 userTransformDataList 和 targetTransformDataList 的 timestamp 根據 reviewBPM 和它們各自的 bpm
        if (userTransformDataList != null)
        {
            AdjustTimestamps(userTransformDataList, reviewBPM);

            // 如果用戶變換數據不為空，獲取所有鼓的打擊數據
            userSnareDrumHits = GetDrumHits(userTransformDataList, "snareDrumHit");
            userBassDrumHits = GetDrumHits(userTransformDataList, "bassDrumHit");
            userClosedHiHatHits = GetDrumHits(userTransformDataList, "closedHiHatHit");
            userTom1Hits = GetDrumHits(userTransformDataList, "tom1Hit");
            userTom2Hits = GetDrumHits(userTransformDataList, "tom2Hit");
            userFloorTomHits = GetDrumHits(userTransformDataList, "floorTomHit");
            userCrashHits = GetDrumHits(userTransformDataList, "crashHit");
            userRideHits = GetDrumHits(userTransformDataList, "rideHit");
            userOpenHiHatHits = GetDrumHits(userTransformDataList, "openHiHatHit");
        }

        // 調整 targetTransformDataList 的 timestamp 根據 reviewBPM 和它們各自的 bpm
        if (targetTransformDataList != null)
        {
            AdjustTimestamps(targetTransformDataList, reviewBPM);

            // 如果目標變換數據不為空，獲取所有鼓的打擊數據
            targetSnareDrumHits = GetDrumHits(targetTransformDataList, "snareDrumHit");
            targetBassDrumHits = GetDrumHits(targetTransformDataList, "bassDrumHit");
            targetClosedHiHatHits = GetDrumHits(targetTransformDataList, "closedHiHatHit");
            targetTom1Hits = GetDrumHits(targetTransformDataList, "tom1Hit");
            targetTom2Hits = GetDrumHits(targetTransformDataList, "tom2Hit");
            targetFloorTomHits = GetDrumHits(targetTransformDataList, "floorTomHit");
            targetCrashHits = GetDrumHits(targetTransformDataList, "crashHit");
            targetRideHits = GetDrumHits(targetTransformDataList, "rideHit");
            targetOpenHiHatHits = GetDrumHits(targetTransformDataList, "openHiHatHit");
        }
    }

    // 加載變換數據
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

    // 調整變換數據的 timestamp 根據新的 BPM
    private void AdjustTimestamps(TransformRecorder.TransformDataList transformDataList, float newBPM)
    {
        float ratio = transformDataList.bpm / newBPM;
        foreach (var data in transformDataList.dataList)
        {
            data.timestamp *= ratio;
        }
        Debug.Log($"Adjusted timestamps for data with new BPM: {newBPM}");
    }

    // 獲取指定鼓的打擊數據
    private List<TransformDataIndex> GetDrumHits(TransformRecorder.TransformDataList transformDataList, string drumType)
    {
        List<TransformDataIndex> drumHitsList = new List<TransformDataIndex>();

        // 遍歷變換數據列表，根據鼓的類型和 hit value 大於 0 來篩選數據
        for (int i = 0; i < transformDataList.dataList.Count; i++)
        {
            var data = transformDataList.dataList[i];
            float hitValue = 0;

            // 根據鼓的類型設置 hit value
            switch (drumType)
            {
                case "snareDrumHit":
                    hitValue = data.snareDrumHit.value;
                    break;
                case "bassDrumHit":
                    hitValue = data.bassDrumHit.value;
                    break;
                case "closedHiHatHit":
                    hitValue = data.closedHiHatHit.value;
                    break;
                case "tom1Hit":
                    hitValue = data.tom1Hit.value;
                    break;
                case "tom2Hit":
                    hitValue = data.tom2Hit.value;
                    break;
                case "floorTomHit":
                    hitValue = data.floorTomHit.value;
                    break;
                case "crashHit":
                    hitValue = data.crashHit.value;
                    break;
                case "rideHit":
                    hitValue = data.rideHit.value;
                    break;
                case "openHiHatHit":
                    hitValue = data.openHiHatHit.value;
                    break;
            }

            // 如果 hit value 大於 0，將該數據和索引添加到列表中
            if (hitValue > 0)
            {
                drumHitsList.Add(new TransformDataIndex(data, i));
            }
        }

        return drumHitsList;
    }
}
