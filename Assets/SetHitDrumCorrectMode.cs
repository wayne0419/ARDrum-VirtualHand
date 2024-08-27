using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHitDrumCorrectMode : MonoBehaviour
{
    public RealTimeInputTracker inputTracker;
    public RealTimeInputTracker.CorrectMode correctMode = RealTimeInputTracker.CorrectMode.CorrectRhythmMode;

    void OnMouseDown() {
        inputTracker.currentMode = correctMode;
    }
}
