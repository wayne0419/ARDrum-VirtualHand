using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReviewManager : MonoBehaviour
{
    // Path to the user's recorded transform data file.
    public string userRecordFilePath;
    // Path to the target (reference) recorded transform data file.
    public string targetRecordFilePath;

    // The loaded transform data list for the user's performance.
    public TransformRecorder.TransformDataList userTransformDataList;
    // The loaded transform data list for the target performance.
    public TransformRecorder.TransformDataList targetTransformDataList;

    // The BPM at which the review playback should occur. Timestamps will be adjusted to this BPM.
    public float reviewBPM;

    // Lists to store specific drum hit data (TransformData and its original index) for the user.
    public List<TransformDataIndex> userSnareDrumHits;
    public List<TransformDataIndex> userBassDrumHits;
    public List<TransformDataIndex> userClosedHiHatHits;
    public List<TransformDataIndex> userTom1Hits;
    public List<TransformDataIndex> userTom2Hits;
    public List<TransformDataIndex> userFloorTomHits;
    public List<TransformDataIndex> userCrashHits;
    public List<TransformDataIndex> userRideHits;
    public List<TransformDataIndex> userOpenHiHatHits;

    // Lists to store specific drum hit data (TransformData and its original index) for the target.
    public List<TransformDataIndex> targetSnareDrumHits;
    public List<TransformDataIndex> targetBassDrumHits;
    public List<TransformDataIndex> targetClosedHiHatHits;
    public List<TransformDataIndex> targetTom1Hits;
    public List<TransformDataIndex> targetTom2Hits;
    public List<TransformDataIndex> targetFloorTomHits;
    public List<TransformDataIndex> targetCrashHits;
    public List<TransformDataIndex> targetRideHits;
    public List<TransformDataIndex> targetOpenHiHatHits;

    /// <summary>
    /// A serializable class to pair a TransformData object with its original index in the data list.
    /// This is useful for retaining context when filtering data.
    /// </summary>
    [System.Serializable]
    public class TransformDataIndex
    {
        public TransformRecorder.TransformData data; // The actual transform data.
        public int index; // The original index of this data point in its list.

        public TransformDataIndex(TransformRecorder.TransformData data, int index)
        {
            this.data = data;
            this.index = index;
        }
    }

    private void OnEnable()
    {
        // Load transform data for both user and target performances from their respective file paths.
        LoadTransformData(userRecordFilePath, out userTransformDataList);
        LoadTransformData(targetRecordFilePath, out targetTransformDataList);

        // Process user's transform data if successfully loaded.
        if (userTransformDataList != null)
        {
            // Adjust timestamps of user data to align with the `reviewBPM`.
            AdjustTimestamps(userTransformDataList, reviewBPM);

            // Extract and categorize all drum hit events from the user's data.
            userSnareDrumHits = GetDrumHits(userTransformDataList, "snareDrumHit");
            userBassDrumHits = GetDrumHits(userTransformDataList, "bassDrumHit");
            userClosedHiHatHits = GetDrumHits(userTransformDataList, "closedHiHatHit");
            userTom1Hits = GetDrumHits(userTransformDataList, "tom1Hit");
            userTom2Hits = GetDrumHits(userTransformDataList, "tom2Hit"); // Corrected method name
            userFloorTomHits = GetDrumHits(userTransformDataList, "floorTomHit");
            userCrashHits = GetDrumHits(userTransformDataList, "crashHit");
            userRideHits = GetDrumHits(userTransformDataList, "rideHit");
            userOpenHiHatHits = GetDrumHits(userTransformDataList, "openHiHatHit");
        }

        // Process target's transform data if successfully loaded.
        if (targetTransformDataList != null)
        {
            // Adjust timestamps of target data to align with the `reviewBPM`.
            AdjustTimestamps(targetTransformDataList, reviewBPM);

            // Extract and categorize all drum hit events from the target's data.
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

    /// <summary>
    /// Loads transform data from a specified JSON file path into a TransformDataList object.
    /// </summary>
    /// <param name="filePath">The full path to the JSON file.</param>
    /// <param name="transformDataList">Output parameter to store the loaded data.</param>
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

    /// <summary>
    /// Adjusts the timestamps within a TransformDataList to match a new BPM.
    /// This is crucial for synchronizing playback of recordings made at different tempos.
    /// </summary>
    /// <param name="transformDataList">The TransformDataList whose timestamps need adjustment.</param>
    /// <param name="newBPM">The target BPM to which timestamps should be adjusted.</param>
    private void AdjustTimestamps(TransformRecorder.TransformDataList transformDataList, float newBPM)
    {
        // Calculate the ratio between the original BPM and the new BPM.
        // If original BPM is 120 and new BPM is 60, ratio is 2. Timestamps will be doubled.
        // If original BPM is 60 and new BPM is 120, ratio is 0.5. Timestamps will be halved.
        float ratio = transformDataList.bpm / newBPM;
        foreach (var data in transformDataList.dataList)
        {
            data.timestamp *= ratio; // Apply the ratio to each timestamp.
        }
        Debug.Log($"Adjusted timestamps for data with new BPM: {newBPM}");
    }

    /// <summary>
    /// Extracts a list of specific drum hit events (where hit value > 0) from a TransformDataList.
    /// </summary>
    /// <param name="transformDataList">The TransformDataList to extract hits from.</param>
    /// <param name="drumType">A string representing the drum hit property name (e.g., "snareDrumHit").</param>
    /// <returns>A list of TransformDataIndex objects for the specified drum type.</returns>
    private List<TransformDataIndex> GetDrumHits(TransformRecorder.TransformDataList transformDataList, string drumType)
    {
        List<TransformDataIndex> drumHitsList = new List<TransformDataIndex>();

        // Iterate through the transform data list to find drum hits.
        for (int i = 0; i < transformDataList.dataList.Count; i++)
        {
            var data = transformDataList.dataList[i];
            float hitValue = 0;

            // Use a switch statement to get the correct hit value based on the drumType string.
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
                default:
                    Debug.LogWarning($"Unknown drum type: {drumType}");
                    break;
            }

            // If a hit value is greater than 0, it indicates a drum hit. Add it to the list.
            if (hitValue > 0)
            {
                drumHitsList.Add(new TransformDataIndex(data, i));
            }
        }

        return drumHitsList;
    }
}