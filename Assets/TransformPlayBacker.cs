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

    [Serializable]
    public class HitSegment
    {
        public string limbUsed; // 这个击打使用的肢体
        public DrumType drumHit; // 被击打的鼓类型
        public int startIdx; // 该段的开始索引
        public int endIdx; // 该段的结束索引
        public bool skip; // 是否跳过这个区段
        public DrumNote associatedNote; // 关联的 DrumNote
    }

    public enum PlayMode { A, B } // 播放模式枚举
    public PlayMode playMode = PlayMode.A; // 默认播放模式为 A

    public string jsonFilePath; // JSON 文件的路径
    public Transform targetTransform1; // 第一个 Transform 对象 (左手)
    public Transform targetTransform2; // 第二个 Transform 对象 (右手)
    public Transform targetTransform3; // 第三个 Transform 对象 (右脚)
    public float playBackBPM; // 用于调整播放速度
    public float startOffsetBeat; // 开始播放前的偏移节拍数
    public Metronome metronome; // Metronome 组件引用
    public DrumSheet drumSheet; // 引用 DrumSheet 组件

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

    // 事件：当 PlayTransformData 开始时触发
    public event Action OnPlayTransformDataStart;

    // 事件：当 PlayTransformData 结束时触发
    public event Action OnPlayTransformDataEnd;

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
        // 触发开始事件
        OnPlayTransformDataStart?.Invoke();

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
                // 如果被跳过的元素中有击打事件，播放相应音效
                CheckAndPlayDrumHits(playbackData.dataList[currentIndex]);
                currentIndex++;
            }

            if (currentIndex < playbackData.dataList.Count - 1)
            {
                float targetTime = playbackData.dataList[currentIndex].timestamp;
                float nextTime = playbackData.dataList[currentIndex + 1].timestamp;

                // 计算插值因子
                float t = Mathf.InverseLerp(targetTime, nextTime, elapsedTime);

                // 使用线性插值更新 Transform
                UpdateTransforms(currentIndex, currentIndex + 1, t);
            }

            yield return null;
        }

        // 播放到最后一个 Transform
        UpdateTransforms(currentIndex, currentIndex, 1.0f);

        // 触发结束事件
        OnPlayTransformDataEnd?.Invoke();
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

        // 更新左手 (对应 position1 和 rotation1)
        if (!IsCurrentSegmentSkipped(indexA, "lefthand"))
        {
            targetTransform1.position = Vector3.Lerp(dataA.position1, dataB.position1, t);
            targetTransform1.rotation = Quaternion.Lerp(dataA.rotation1, dataB.rotation1, t);
        }

        // 更新右手 (对应 position2 和 rotation2)
        if (!IsCurrentSegmentSkipped(indexA, "righthand"))
        {
            targetTransform2.position = Vector3.Lerp(dataA.position2, dataB.position2, t);
            targetTransform2.rotation = Quaternion.Lerp(dataA.rotation2, dataB.rotation2, t);
        }

        // 更新右脚 (对应 position3 和 rotation3)
        if (!IsCurrentSegmentSkipped(indexA, "rightfeet"))
        {
            targetTransform3.position = Vector3.Lerp(dataA.position3, dataB.position3, t);
            targetTransform3.rotation = Quaternion.Lerp(dataA.rotation3, dataB.rotation3, t);
        }
    }

    void CheckAndPlayDrumHits(TransformData data)
    {
        // 播放大鼓击打音效
        if (data.bassDrumHit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.bassDrumHit.limb))
        {
            PlayDrumHit(bassDrumAudioSource, data.bassDrumHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerBassDrum(data.bassDrumHit.limb);
            }
        }

        // 播放小军鼓击打音效
        if (data.snareDrumHit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.snareDrumHit.limb))
        {
            PlayDrumHit(snareDrumAudioSource, data.snareDrumHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerSnareDrum(data.snareDrumHit.limb);
            }
        }

        // 播放闭合踩镲击打音效
        if (data.closedHiHatHit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.closedHiHatHit.limb))
        {
            PlayDrumHit(closedHiHatAudioSource, data.closedHiHatHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerClosedHiHat(data.closedHiHatHit.limb);
            }
        }

        // 播放 Tom1 鼓击打音效
        if (data.tom1Hit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.tom1Hit.limb))
        {
            PlayDrumHit(tom1AudioSource, data.tom1Hit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerTom1(data.tom1Hit.limb);
            }
        }

        // 播放 Tom2 鼓击打音效
        if (data.tom2Hit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.tom2Hit.limb))
        {
            PlayDrumHit(tom2AudioSource, data.tom2Hit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerTom2(data.tom2Hit.limb);
            }
        }

        // 播放地板鼓击打音效
        if (data.floorTomHit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.floorTomHit.limb))
        {
            PlayDrumHit(floorTomAudioSource, data.floorTomHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerFloorTom(data.floorTomHit.limb);
            }
        }

        // 播放碰镲击打音效
        if (data.crashHit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.crashHit.limb))
        {
            PlayDrumHit(crashAudioSource, data.crashHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerCrash(data.crashHit.limb);
            }
        }

        // 播放 Ride 镲击打音效
        if (data.rideHit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.rideHit.limb))
        {
            PlayDrumHit(rideAudioSource, data.rideHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerRide(data.rideHit.limb);
            }
        }

        // 播放打开踩镲击打音效
        if (data.openHiHatHit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.openHiHatHit.limb))
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
        Dictionary<DrumType, int> drumHitCounter = new Dictionary<DrumType, int>(); // 用于计数每种类型的 DrumNote

        for (int i = 0; i < playbackData.dataList.Count; i++)
        {
            var data = playbackData.dataList[i];
            ProcessHit(data.bassDrumHit, DrumType.BassDrum, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.snareDrumHit, DrumType.SnareDrum, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.closedHiHatHit, DrumType.ClosedHiHat, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.tom1Hit, DrumType.Tom1, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.tom2Hit, DrumType.Tom2, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.floorTomHit, DrumType.FloorTom, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.crashHit, DrumType.Crash, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.rideHit, DrumType.Ride, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.openHiHatHit, DrumType.OpenHiHat, i, lastHitIndex, drumHitCounter);
        }
    }

    void ProcessHit(DrumHit drumHit, DrumType drumType, int currentIndex, Dictionary<string, int> lastHitIndex, Dictionary<DrumType, int> drumHitCounter)
    {
        if (drumHit.value > 0)
        {
            // 计算击打区段的开始和结束索引
            int startIdx = lastHitIndex.ContainsKey(drumHit.limb) ? lastHitIndex[drumHit.limb] + 1 : 0;
            int endIdx = currentIndex;

            // 计算这是该类型的第几个 hit
            if (!drumHitCounter.ContainsKey(drumType))
            {
                drumHitCounter[drumType] = 0;
            }
            int hitIndex = drumHitCounter[drumType];
            drumHitCounter[drumType]++;

            // 连接 hitSegment 和 DrumNote 的代码
            DrumNote associatedNote = drumSheet.GetDrumNoteByIndex(drumType, hitIndex);

            // 创建新的 HitSegment 并添加到列表
            HitSegment segment = new HitSegment
            {
                limbUsed = drumHit.limb,
                drumHit = drumType,
                startIdx = startIdx,
                endIdx = endIdx,
                skip = false, // 默认不跳过这个区段
                associatedNote = associatedNote // 关联的 DrumNote
            };

            hitSegments.Add(segment);

            // 双向关联
            if (associatedNote != null)
            {
                associatedNote.associatedSegment = segment; // 在 DrumNote 中设置关联的 HitSegment
            }

            // 更新 lastHitIndex
            lastHitIndex[drumHit.limb] = currentIndex;
        }
    }

    bool IsCurrentSegmentSkipped(int currentIndex, string limb)
    {
        foreach (var segment in hitSegments)
        {
            if (currentIndex >= segment.startIdx && currentIndex <= segment.endIdx && segment.skip && segment.limbUsed == limb)
            {
                return true;
            }
        }
        return false;
    }
}
