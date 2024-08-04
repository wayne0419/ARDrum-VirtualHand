using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase
{
    Normal, 
    Record, 
    PlayBack,
    PracticeRecord
}

public class PhaseManager : MonoBehaviour
{   
    [Header("Reference")]
    public RecordPhaseRunner recordPhaseRunner;
    public PlayBackPhaseRunner playBackPhaseRunner;
    public PracticeRecordPhaseRunner practiceRecordPhaseRunner;
    [Header("Debug (Dont modify from inspector)")]
    public Phase currentPhase;
    public PhaseRunner currentPhaseRunner;
    // Start is called before the first frame update
    void Start()
    {
        currentPhase = Phase.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            // Stop current phase
            if (currentPhaseRunner != null && currentPhaseRunner.isRunning) {
                currentPhaseRunner.StopPhaseRunner();
            }

            // Start a record phase
            currentPhase = Phase.Record;
            currentPhaseRunner = recordPhaseRunner;
            currentPhaseRunner.StartPhaseRunner();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1)) {
            // Stop current phase
            if (currentPhaseRunner != null && currentPhaseRunner.isRunning) {
                currentPhaseRunner.StopPhaseRunner();
            }

            // Start a record phase
            currentPhase = Phase.PlayBack;
            currentPhaseRunner = playBackPhaseRunner;
            currentPhaseRunner.StartPhaseRunner();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            // Stop current phase
            if (currentPhaseRunner != null && currentPhaseRunner.isRunning) {
                currentPhaseRunner.StopPhaseRunner();
            }

            // Start a record phase
            currentPhase = Phase.PracticeRecord;
            currentPhaseRunner = practiceRecordPhaseRunner;
            currentPhaseRunner.StartPhaseRunner();
        }
    }
}
