using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RecordPhaseRunner : PhaseRunner
{
    [Header("Reference")]
    public GameObject transformRecorder;
    public override void StartPhaseRunner() {
        isRunning = true;
        transformRecorder.SetActive(true);
    }

    public override void StopPhaseRunner() {
        isRunning = false;
        transformRecorder.SetActive(false);
    }

}
