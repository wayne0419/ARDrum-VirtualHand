using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransformRecorder : MonoBehaviour
{
    // 记录 position、rotation 和打击音效值的类
    [System.Serializable]
    public class DrumHit
    {
        public float value;
        public string limb;

        public DrumHit(float val, string limbType)
        {
            value = val;
            limb = limbType;
        }
    }

    [System.Serializable]
    public class TransformData
    {
        public Vector3 position1;
        public Quaternion rotation1;
        public Vector3 position2;
        public Quaternion rotation2;
        public Vector3 position3;
        public Quaternion rotation3;
        public DrumHit bassDrumHit;
        public DrumHit snareDrumHit;
        public DrumHit closedHiHatHit;
        public DrumHit tom1Hit;
        public DrumHit tom2Hit;
        public DrumHit floorTomHit;
        public DrumHit crashHit;
        public DrumHit rideHit;
        public DrumHit openHiHatHit;
        public float timestamp;

        public TransformData(Vector3 pos1, Quaternion rot1, Vector3 pos2, Quaternion rot2, Vector3 pos3, Quaternion rot3, DrumHit bassHit, DrumHit snareHit, DrumHit hiHatHit, DrumHit t1Hit, DrumHit t2Hit, DrumHit floorHit, DrumHit crashH, DrumHit rideH, DrumHit openHiHat, float time)
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
            openHiHatHit = openHiHat;
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
    public bool allowInputControl = true; // 是否允许通过空白键操控
    public TransformPlayBacker transformPlayBacker; // TransformPlayBacker 组件

    // Input Actions
    public InputAction bassDrumHit;
    public InputAction snareDrumHit;
    public InputAction closedHiHatHit;
    public InputAction tom1Hit;
    public InputAction tom2Hit;
    public InputAction floorTomHit;
    public InputAction crashHit;
    public InputAction rideHit;
    public InputAction openHiHatHit; // 新增的 openHiHatHit

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
        openHiHatHit.Enable(); // 启用 openHiHatHit
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
        openHiHatHit.Disable(); // 禁用 openHiHatHit
    }

    void Update()
    {
        if (allowInputControl && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isRecordingInProgress)
            {
                // 如果没有进行中的记录延迟或记录过程，开始新的记录
                if (metronome != null)
                {
                    metronome.bpm = bpm;
                    metronome.StartMetronome();
                }
                StartRecord(recordDelayBeats, recordDurationBeats);
            }
        }

        if (isRecording)
        {
            RecordTransform();
        }
    }

    public Coroutine StartRecord(float delayBeats, float recordBeats)
    {
        return StartCoroutine(StartRecordingAfterBeats(delayBeats, recordBeats));
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

    void RecordTransform()
    {
        if (targetTransform1 != null && targetTransform2 != null)
        {
            // 检查是否有打击音效的触发
            DrumHit bassDrumHitValue = bassDrumHit.triggered ? new DrumHit(bassDrumHit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit snareDrumHitValue = snareDrumHit.triggered ? new DrumHit(snareDrumHit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit closedHiHatHitValue = closedHiHatHit.triggered ? new DrumHit(closedHiHatHit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit tom1HitValue = tom1Hit.triggered ? new DrumHit(tom1Hit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit tom2HitValue = tom2Hit.triggered ? new DrumHit(tom2Hit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit floorTomHitValue = floorTomHit.triggered ? new DrumHit(floorTomHit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit crashHitValue = crashHit.triggered ? new DrumHit(crashHit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit rideHitValue = rideHit.triggered ? new DrumHit(rideHit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit openHiHatHitValue = openHiHatHit.triggered ? new DrumHit(openHiHatHit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");

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
                openHiHatHitValue,
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
                float animationTime = timestamp - (endTime - clipLength); // 将 animationClip 的末尾对齐到 endTime
                animationClip.SampleAnimation(targetTransform3.gameObject, animationTime);
                transformDataList[i].position3 = targetTransform3.position;
                transformDataList[i].rotation3 = targetTransform3.rotation;
            }
        }
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

        // 修改 transformPlayBacker 的 jsonFilePath
        if (transformPlayBacker != null)
        {
            transformPlayBacker.jsonFilePath = filePath;
        }
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
