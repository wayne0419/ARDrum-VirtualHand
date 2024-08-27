using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SetHitDrumCorrectMode : MonoBehaviour
{
    public RealTimeInputTracker inputTracker;
    public RealTimeInputTracker.CorrectMode correctMode = RealTimeInputTracker.CorrectMode.CorrectRhythmMode;
    public InputAction inputAction;

    void OnEnable() {
        inputAction.Enable();
    }
    void OnDisable() {
        inputAction.Disable();
    }
    void Update() {
        if (inputAction.triggered) {
            OnMouseDown();
        }
    }
    void OnMouseDown() {
        inputTracker.currentMode = correctMode;
    }
}
