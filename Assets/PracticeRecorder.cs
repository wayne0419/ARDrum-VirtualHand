using System.Collections;
using UnityEngine;

public class PracticeRecorder : MonoBehaviour
{
    public TransformRecorder transformRecorder;
    public TransformPlayBacker transformPlayBacker;
    public float recordDelayBeats = 4f;
    public float recordDurationBeats = 4f;
    public bool isPracticeRecording = false; // 加入此變量

    private Coroutine practiceCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isPracticeRecording)
            {
                StartPracticeRecord();
            }
            else
            {
                StopPracticeRecord();
            }
        }
    }

    public void StartPracticeRecord()
    {
        isPracticeRecording = true;
        practiceCoroutine = StartCoroutine(StartPractice());
    }

    public void StopPracticeRecord()
    {
        if (practiceCoroutine != null)
        {
            StopCoroutine(practiceCoroutine);
            practiceCoroutine = null;
            transformPlayBacker.StopPlayBack(); // 停止播放
            isPracticeRecording = false;
        }
    }

    private IEnumerator StartPractice()
    {
        // 開始播放
        transformPlayBacker.playMode = TransformPlayBacker.PlayMode.A;
        transformPlayBacker.StartPlayBack();

        while (true)
        {
            // 等待記錄延遲時間後開始記錄
            yield return transformRecorder.StartRecord(recordDelayBeats, recordDurationBeats);
        }
    }
}
