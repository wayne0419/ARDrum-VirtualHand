using System.Collections;
using UnityEngine;

public class PracticeRecorder : MonoBehaviour
{
    public TransformRecorder transformRecorder;
    public TransformPlayBacker transformPlayBacker;
    // public Metronome metronome;
    public float recordDelayBeats = 4f;
    public float recordDurationBeats = 4f;

    private Coroutine practiceCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (practiceCoroutine == null)
            {
                practiceCoroutine = StartCoroutine(StartPractice());
            }
            else
            {
                StopCoroutine(practiceCoroutine);
                practiceCoroutine = null;
                transformPlayBacker.StopPlayBack(); // 停止播放
            }
        }
    }

    private IEnumerator StartPractice()
    {
        // if (metronome != null)
        // {
        //     metronome.bpm = transformRecorder.bpm;
        //     metronome.StartMetronome();
        // }

        // 開始播放
        transformPlayBacker.playMode = TransformPlayBacker.PlayMode.A;
        transformPlayBacker.StartPlayBack();

        while (true)
        {
            // 等待記錄延遲時間後開始記錄
            yield return StartCoroutine(transformRecorder.StartRecordingAfterBeats(recordDelayBeats, recordDurationBeats));
        }
    }
}
