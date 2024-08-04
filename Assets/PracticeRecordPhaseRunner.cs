using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeRecordPhaseRunner : PhaseRunner
{
    [Header("Reference")]
    public GameObject PracticeRecorder;
    public override void StartPhaseRunner() {
        isRunning = true;
        PracticeRecorder.SetActive(true);
    }

    public override void StopPhaseRunner() {
        isRunning = false;
        PracticeRecorder.SetActive(false);
    }
}
