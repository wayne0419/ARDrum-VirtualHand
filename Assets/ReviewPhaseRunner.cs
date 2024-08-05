using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReviewPhaseRunner : PhaseRunner
{
    [Header("Reference")]
    public GameObject reviewManager;
    public override void StartPhaseRunner() {
        isRunning = true;
        reviewManager.SetActive(true);
    }

    public override void StopPhaseRunner() {
        isRunning = false;
        reviewManager.SetActive(false);
    }

}
