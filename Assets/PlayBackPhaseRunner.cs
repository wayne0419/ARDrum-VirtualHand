using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackPhaseRunner : PhaseRunner
{
    [Header("Reference")]
    public GameObject transformPlayBacker;
    public override void StartPhaseRunner() {
        isRunning = true;
        transformPlayBacker.SetActive(true);
    }

    public override void StopPhaseRunner() {
        isRunning = false;
        transformPlayBacker.SetActive(false);
    }
}
