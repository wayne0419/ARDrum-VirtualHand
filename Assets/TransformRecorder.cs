using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransformRecorder : MonoBehaviour
{
    // 记录 position、rotation 和打击音效值的结构体
    [System.Serializable]
    public class TransformData
    {
        public Vector3 position1;
        public Quaternion rotation1;
        public Vector3 position2;
        public Quaternion rotation2;
        public Vector3 position3;
        public Quaternion rotation3;
        public float bassDrumHit;
        public float snareDrumHit;
        public float closedHiHatHit;
        public float tom1Hit;
        public float tom2Hit;
        public float floorTomHit;
        public float crashHit;
        public float rideHit;
        public float timestamp;

        public TransformData(Vector3 pos1, Quaternion rot1, Vector3 pos2, Quaternion rot2, Vector3 pos3, Quaternion rot3, float bassHit, float snareHit, float hiHatHit, float t1Hit, float t2Hit, float floorHit, float crashH, float rideH, float time)
        {
            position1 = pos1;
            rotation1 = rot1;
            position2 = pos2;
            rotation2 = rot2;
            position3 = pos3;
            rotation3 = rot3;
            bassDrumHit = bassHit;
            snareDrumHit = snareHit;
            closedHiHatHit = hiHatHit;
            tom1Hit = t1Hit;
            tom2Hit = t2Hit;
            floorTomHit = floorHit;
            crashHit = crashH;
            rideHit = rideH;
            timestamp = time;
        }
    }

    public Transform targetTransform1; // 第一组要记录的 Transform
    public Transform targetTransform2; // 第二组要记录的 Transform
    public Transform targetTransform3; // 第三组要记录的 Transform
    public string folderPath = "Assets/RecordedTransforms"; // 指定文件夹路径
    public float bpm = 120f; // 每分钟的节拍数
    public float recordDelayBeats = 4f; // 延迟的节拍数量
    public float recordDurationBeats = 4f; // 记录持续的节拍数量
    public Metronome metronome; // 参考 Metronome 组件
    public AnimationClip animationClip; // 用于第三组 Transform 的 AnimationClip

    // Input Actions
    public InputAction bassDrumHit;
    public InputAction snareDrumHit;
    public InputAction closedHiHatHit;
    public InputAction tom1Hit;
    public InputAction tom2Hit;
    public InputAction floorTomHit;
    public InputAction crashHit;
    public InputAction rideHit;

    private List<TransformData> transformDataList = new List<TransformData>();
    public bool isRecording = false; // 记录状态
    public bool isRecordingInProgress = false; // 记录延迟或记录过程的状态
    private float recordingStartTime = 0f;

    void OnEnable()
    {
        bassDrumHit.Enable();
        snareDrumHit.Enable();
        closedHiHatHit.Enable();
        tom1Hit.Enable();
        tom2Hit.Enable();
        floorTomHit.Enable();
        crashHit.Enable();
        rideHit.Enable();
    }

    void OnDisable()
    {
        bassDrumHit.Disable();
        snareDrumHit.Disable();
        closedHiHatHit.Disable();
        tom1Hit.Disable();
        tom2Hit.Disable();
        floorTomHit.Disable();
        crashHit.Disable();
        rideHit.Disable();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isRecordingInProgress)
            {
                // 如果没有进行中的记录延迟或记录过程，开始新的记录
                if (metronome != null)
                {
                    metronome.bpm = bpm;
                    metronome.StartMetronome();
                }
                StartCoroutine(StartRecordingAfterBeats(recordDelayBeats, recordDurationBeats));
            }
        }

        if (isRecording)
        {
            RecordTransform();
        }
    }

    void RecordTransform()
    {
        if (targetTransform1 != null && targetTransform2 != null)
        {
            // 检查是否有打击音效的触发
            float bassDrumHitValue = bassDrumHit.triggered ? bassDrumHit.ReadValue<float>() : 0f;
            float snareDrumHitValue = snareDrumHit.triggered ? snareDrumHit.ReadValue<float>() : 0f;
            float closedHiHatHitValue = closedHiHatHit.triggered ? closedHiHatHit.ReadValue<float>() : 0f;
            float tom1HitValue = tom1Hit.triggered ? tom1Hit.ReadValue<float>() : 0f;
            float tom2HitValue = tom2Hit.triggered ? tom2Hit.ReadValue<float>() : 0f;
            float floorTomHitValue = floorTomHit.triggered ? floorTomHit.ReadValue<float>() : 0f;
            float crashHitValue = crashHit.triggered ? crashHit.ReadValue<float>() : 0f;
            float rideHitValue = rideHit.triggered ? rideHit.ReadValue<float>() : 0f;

            float timestamp = Time.time - recordingStartTime;

            if (bassDrumHit.triggered)
            {
                float bassDrumHitTime = timestamp;
                OverwriteTransformDataWithAnimationClip(bassDrumHitTime);
            }

            Vector3 position3 = targetTransform3.position;
            Quaternion rotation3 = targetTransform3.rotation;

            TransformData data = new TransformData(
                targetTransform1.position, targetTransform1.rotation,
                targetTransform2.position, targetTransform2.rotation,
                position3, rotation3,
                bassDrumHitValue,
                snareDrumHitValue,
                closedHiHatHitValue,
                tom1HitValue,
                tom2HitValue,
                floorTomHitValue,
                crashHitValue,
                rideHitValue,
                timestamp
            );
            transformDataList.Add(data);
        }
    }

    void OverwriteTransformDataWithAnimationClip(float endTime)
    {
        float clipLength = animationClip.length;
        for (int i = 0; i < transformDataList.Count; i++)
        {
            float timestamp = transformDataList[i].timestamp;
            if (timestamp <= endTime && timestamp >= endTime - clipLength)
            {
                float animationTime = timestamp - (endTime - clipLength);
                animationClip.SampleAnimation(targetTransform3.gameObject, animationTime);
                transformDataList[i].position3 = targetTransform3.position;
                transformDataList[i].rotation3 = targetTransform3.rotation;
            }
        }
    }

    System.Collections.IEnumerator StartRecordingAfterBeats(float delayBeats, float recordBeats)
    {
        isRecordingInProgress = true;
        float beatDuration = 60f / bpm;
        yield return new WaitForSeconds(delayBeats * beatDuration);
        isRecording = true;
        recordingStartTime = Time.time;
        transformDataList.Clear(); // 清除之前的记录
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

        // 只计算 .json 文件
        int fileCount = Directory.GetFiles(folderPath, "*.json").Length;
        string filePath = Path.Combine(folderPath, fileCount + ".json");

        // 将 transformDataList 转换为 JSON 格式并写入文件
        TransformDataList dataList = new TransformDataList(bpm, transformDataList);
        string json = JsonUtility.ToJson(dataList, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Transform data saved to " + filePath);
    }

    [System.Serializable]
    public class TransformDataList
    {
        public float bpm;
        public List<TransformData> dataList;

        public TransformDataList(float bpmValue, List<TransformData> list)
        {
            bpm = bpmValue;
            dataList = list;
        }
    }
}
