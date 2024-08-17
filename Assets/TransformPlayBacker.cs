using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TransformPlayBacker : MonoBehaviour
{
    [Serializable]
    public class DrumHit
    {
        public float value; // 鼓击打的力度值
        public string limb; // 使用的肢体
    }

    [Serializable]
    public class TransformData
    {
        public Vector3 position1; // 第一个 Transform 的位置
        public Quaternion rotation1; // 第一个 Transform 的旋转
        public Vector3 position2; // 第二个 Transform 的位置
        public Quaternion rotation2; // 第二个 Transform 的旋转
        public Vector3 position3; // 第三个 Transform 的位置
        public Quaternion rotation3; // 第三个 Transform 的旋转
        public float timestamp; // 时间戳
        public DrumHit bassDrumHit; // 大鼓的击打
        public DrumHit snareDrumHit; // 小军鼓的击打
        public DrumHit closedHiHatHit; // 闭合的踩镲的击打
        public DrumHit tom1Hit; // Tom1 鼓的击打
        public DrumHit tom2Hit; // Tom2 鼓的击打
        public DrumHit floorTomHit; // 地板鼓的击打
        public DrumHit crashHit; // 碰镲的击打
        public DrumHit rideHit; // Ride镲的击打
        public DrumHit openHiHatHit; // 打开的踩镲的击打
    }

    [Serializable]
    public class TransformPlaybackData
    {
        public float bpm; // 记录的节拍速度
        public List<TransformData> dataList; // 记录的 Transform 数据列表
    }

    public class HitSegment
    {
        public string limbUsed; // 这个击打使用的肢体
        public string drumHit; // 被击打的鼓
        public int startIdx; // 该段的开始索引
        public int endIdx; // 该段的结束索引
        public bool skip; // 是否跳过这个区段
    }

    public enum PlayMode { A, B } // 播放模式枚举
    public PlayMode playMode = PlayMode.A; // 默认播放模式为 A

    public string jsonFilePath; // JSON 文件的路径
    public Transform targetTransform1; // 第一个 Transform 对象
    public Transform targetTransform2; // 第二个 Transform 对象
    public Transform targetTransform3; // 第三个 Transform 对象
    public float playBackBPM; // 用于调整播放速度
    public float startOffsetBeat; // 开始播放前的偏移节拍数
    public Metronome metronome; // Metronome 组件引用

    public AudioSource bassDrumAudioSource; // 大鼓音效
    public AudioSource snareDrumAudioSource; // 小军鼓音效
    public AudioSource closedHiHatAudioSource; // 闭合踩镲音效
    public AudioSource tom1AudioSource; // Tom1 鼓音效
    public AudioSource tom2AudioSource; // Tom2 鼓音效
    public AudioSource floorTomAudioSource; // 地板鼓音效
    public AudioSource crashAudioSource; // 碰镲音效
    public AudioSource rideAudioSource; // Ride镲音效
    public AudioSource openHiHatAudioSource; // 打开踩镲音效

    public DrumHitIndicator drumHitIndicator; // DrumHitIndicator 组件引用
    public DrumSheetPlayer drumSheetPlayer; // DrumSheetPlayer 组件引用

    public TransformPlaybackData playbackData; // 播放数据
    public List<HitSegment> hitSegments; // 保存击打区段信息
    public int currentIndex; // 当前播放的索引
    private float playbackSpeedMultiplier; // 播放速度倍率
    private float offsetDuration; // 播放前的偏移时长
    public bool isPlaying = false; // 播放状态标志
    public bool allowInputControl = true; // 是否允许通过按空格键来控制
    private Coroutine playbackCoroutine; // 播放协程引用
    public float playbackStartTime; // 播放开始时间

    void OnEnable()
    {
        // 每次启用时读取 JSON 文件
        LoadJsonFile(jsonFilePath);

        // 初始化 drumHitIndicator
        if (drumHitIndicator != null)
        {
            drumHitIndicator.Initialize();
        }

        // 生成区段信息
        GenerateHitSegments();
    }

    void Update()
    {
        // 如果允许输入控制且按下空格键，启动或停止播放
        if (allowInputControl && Input.GetKeyDown(KeyCode.Space))
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
    }

    public void StartPlayBack()
    {
        // 根据播放数据初始化播放速度倍率和偏移时长
        if (playbackData != null)
        {
            playbackSpeedMultiplier = playBackBPM / playbackData.bpm;
            offsetDuration = 60f / playBackBPM * startOffsetBeat;
        }
        else
        {
            playbackSpeedMultiplier = 1f;
            offsetDuration = 0f;
        }

        isPlaying = true;

        // 根据播放模式启动不同的 coroutine
        if (playMode == PlayMode.A)
        {
            playbackCoroutine = StartCoroutine(PlayBackCoroutineA());
        }
        else if (playMode == PlayMode.B)
        {
            playbackCoroutine = StartCoroutine(PlayBackCoroutineB());
        }
    }

    public void StopPlayBack()
    {
        // 停止播放协程
        if (playbackCoroutine != null)
        {
            StopCoroutine(playbackCoroutine);
        }

        isPlaying = false;

        // 停止播放 Metronome 和 Drum Sheet
        if (metronome != null)
        {
            metronome.StopMetronome();
        }

        if (drumSheetPlayer != null)
        {
            drumSheetPlayer.Pause();
        }
    }

    private IEnumerator PlayBackCoroutineA()
    {
        while (isPlaying)
        {
            // 开始播放 Metronome
            if (metronome != null)
            {
                metronome.bpm = playBackBPM; // 确保设置 bpm
                metronome.StopMetronome(); // 确保 metronome 停止
                metronome.StartMetronome(); // 重新启动 metronome
            }

            // 播放空白记录
            yield return new WaitForSeconds(offsetDuration);

            playbackStartTime = Time.time;
            currentIndex = 0;

            // 开始播放 Drum Sheet 影片
            if (drumSheetPlayer != null)
            {
                drumSheetPlayer.Play(0f, playBackBPM); // 从头开始播放，并调整速度
            }

            // 使用 PlayTransformData 简化代码
            yield return PlayTransformData();
        }
    }

    private IEnumerator PlayBackCoroutineB()
    {
        // 开始播放 Metronome
        if (metronome != null)
        {
            metronome.bpm = playBackBPM; // 确保设置 bpm
            metronome.StopMetronome(); // 确保 metronome 停止
            metronome.StartMetronome(); // 重新启动 metronome
        }

        // 播放空白记录
        yield return new WaitForSeconds(offsetDuration);

        playbackStartTime = Time.time;
        currentIndex = 0;

        // 开始播放 Drum Sheet 影片
        if (drumSheetPlayer != null)
        {
            drumSheetPlayer.Play(0f, playBackBPM); // 从头开始播放，并调整速度
        }

        // 播放整个 TransformPlayBackData 一次
        yield return PlayTransformData();

        // 循环播放 TransformPlayBackData 跳过开头一个 beat 的时间
        while (isPlaying)
        {
            playbackStartTime = Time.time;

            float skipBeat = 1f;

            // 重置 Metronome
            if (metronome != null)
            {
                metronome.bpm = playBackBPM; // 确保设置 bpm
                metronome.StopMetronome(); // 确保 metronome 停止
                metronome.StartMetronome(); // 重新启动 metronome
            }

            // 开始播放 Drum Sheet 影片并跳过开头一个 beat 的时间
            if (drumSheetPlayer != null)
            {
                drumSheetPlayer.Play(skipBeat, playBackBPM);
            }

            // 调用 PlayTransformData 时传递 skipBeat 以跳过开头一个 beat 的时间
            yield return PlayTransformData(skipBeat * (60f / playbackData.bpm));
        }
    }

    private IEnumerator PlayTransformData(float startTimeOffset = 0f)
    {
        if (startTimeOffset > 0)
        {
            // 计算跳过时间后应该从哪个索引开始播放
            currentIndex = GetStartIndexAfterSkipTime(startTimeOffset);
        }

        while (currentIndex < playbackData.dataList.Count - 1)
        {
            float elapsedTime = (Time.time - playbackStartTime) * playbackSpeedMultiplier + startTimeOffset;

            // 检查是否跳过了多个元素
            while (currentIndex < playbackData.dataList.Count - 1 && elapsedTime >= playbackData.dataList[currentIndex + 1].timestamp)
            {
                if (!IsCurrentSegmentSkipped(currentIndex))
                {
                    // 如果被跳过的元素中有击打事件，播放相应音效
                    CheckAndPlayDrumHits(playbackData.dataList[currentIndex]);
                }
                currentIndex++;
            }

            if (currentIndex < playbackData.dataList.Count - 1)
            {
                if (!IsCurrentSegmentSkipped(currentIndex))
                {
                    float targetTime = playbackData.dataList[currentIndex].timestamp;
                    float nextTime = playbackData.dataList[currentIndex + 1].timestamp;

                    // 计算插值因子
                    float t = Mathf.InverseLerp(targetTime, nextTime, elapsedTime);

                    // 使用线性插值更新 Transform
                    UpdateTransforms(currentIndex, currentIndex + 1, t);
                }
            }

            yield return null;
        }

        // 播放到最后一个 Transform
        if (!IsCurrentSegmentSkipped(currentIndex))
        {
            UpdateTransforms(currentIndex, currentIndex, 1.0f);
        }
    }

    private int GetStartIndexAfterSkipTime(float skipTime)
    {
        // 找到第一个 timestamp 大于 skipTime 的索引
        for (int i = 0; i < playbackData.dataList.Count; i++)
        {
            if (playbackData.dataList[i].timestamp >= skipTime)
            {
                return i;
            }
        }
        return 0; // 如果所有 timestamp 都小于 skipTime，则从头开始
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
        targetTransform3.position = Vector3.Lerp(dataA.position3, dataB.position3, t); // 更新第三个 Transform 的位置
        targetTransform3.rotation = Quaternion.Lerp(dataA.rotation3, dataB.rotation3, t); // 更新第三个 Transform 的旋转
    }

    void CheckAndPlayDrumHits(TransformData data)
    {
        if (data.bassDrumHit.value > 0f)
        {
            PlayDrumHit(bassDrumAudioSource, data.bassDrumHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerBassDrum(data.bassDrumHit.limb);
            }
        }
        if (data.snareDrumHit.value > 0f)
        {
            PlayDrumHit(snareDrumAudioSource, data.snareDrumHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerSnareDrum(data.snareDrumHit.limb);
            }
        }
        if (data.closedHiHatHit.value > 0f)
        {
            PlayDrumHit(closedHiHatAudioSource, data.closedHiHatHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerClosedHiHat(data.closedHiHatHit.limb);
            }
        }
        if (data.tom1Hit.value > 0f)
        {
            PlayDrumHit(tom1AudioSource, data.tom1Hit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerTom1(data.tom1Hit.limb);
            }
        }
        if (data.tom2Hit.value > 0f)
        {
            PlayDrumHit(tom2AudioSource, data.tom2Hit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerTom2(data.tom2Hit.limb);
            }
        }
        if (data.floorTomHit.value > 0f)
        {
            PlayDrumHit(floorTomAudioSource, data.floorTomHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerFloorTom(data.floorTomHit.limb);
            }
        }
        if (data.crashHit.value > 0f)
        {
            PlayDrumHit(crashAudioSource, data.crashHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerCrash(data.crashHit.limb);
            }
        }
        if (data.rideHit.value > 0f)
        {
            PlayDrumHit(rideAudioSource, data.rideHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerRide(data.rideHit.limb);
            }
        }
        if (data.openHiHatHit.value > 0f)
        {
            PlayDrumHit(openHiHatAudioSource, data.openHiHatHit.value); // 处理 openHiHatHit
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerOpenHiHat(data.openHiHatHit.limb);
            }
        }
    }

    void PlayDrumHit(AudioSource audioSource, float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    void GenerateHitSegments()
    {
        hitSegments = new List<HitSegment>();
        Dictionary<string, int> lastHitIndex = new Dictionary<string, int>();

        for (int i = 0; i < playbackData.dataList.Count; i++)
        {
            var data = playbackData.dataList[i];
            ProcessHit(data.bassDrumHit, "Bass Drum", i, lastHitIndex);
            ProcessHit(data.snareDrumHit, "Snare Drum", i, lastHitIndex);
            ProcessHit(data.closedHiHatHit, "Closed Hi-Hat", i, lastHitIndex);
            ProcessHit(data.tom1Hit, "Tom1", i, lastHitIndex);
            ProcessHit(data.tom2Hit, "Tom2", i, lastHitIndex);
            ProcessHit(data.floorTomHit, "Floor Tom", i, lastHitIndex);
            ProcessHit(data.crashHit, "Crash", i, lastHitIndex);
            ProcessHit(data.rideHit, "Ride", i, lastHitIndex);
            ProcessHit(data.openHiHatHit, "Open Hi-Hat", i, lastHitIndex);
        }
    }

    void ProcessHit(DrumHit drumHit, string drumName, int currentIndex, Dictionary<string, int> lastHitIndex)
    {
        if (drumHit.value > 0)
        {
            int startIdx = lastHitIndex.ContainsKey(drumHit.limb) ? lastHitIndex[drumHit.limb] + 1 : 0;
            int endIdx = currentIndex;

            hitSegments.Add(new HitSegment
            {
                limbUsed = drumHit.limb,
                drumHit = drumName,
                startIdx = startIdx,
                endIdx = endIdx,
                skip = false // 默认不跳过这个区段
            });

            lastHitIndex[drumHit.limb] = currentIndex;
        }
    }

    bool IsCurrentSegmentSkipped(int currentIndex)
    {
        foreach (var segment in hitSegments)
        {
            if (currentIndex >= segment.startIdx && currentIndex <= segment.endIdx && segment.skip)
            {
                return true;
            }
        }
        return false;
    }
}
