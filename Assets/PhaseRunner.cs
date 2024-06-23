using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhaseRunner : MonoBehaviour
{
    public bool isRunning;
    public abstract void StartPhaseRunner();  // The function used to start this phase runner, with the parameters already been set
    public abstract void StopPhaseRunner();  // The function used to stop this phase runner, do cleanup
}
