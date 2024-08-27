using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetDrumNoteSkipStateButton : MonoBehaviour
{
    public enum Selector {
        righthand,
        lefthand,
        rightfeet,
        leftfeet,
        snare,
        bass,
        closedHiHat,
        openHiHat,
        crash,
        ride,
        tom1,
        tom2,
        floorTom,
        Beat1,
        Beat2,
        Beat3,
        Beat4,
        RightHand4Beat,
        RightHand8Beat,
        RightHand16Beat,
        BothHand4Beat,
        BothHand8Beat,
        BothHand16Beat,
        RightHandRightFeet4Beat,
        RightHandRightFeet8Beat,
        RightHandRightFeet16Beat,
        RightHandLeftHandRightFeet4Beat,
        RightHandLeftHandRightFeet8Beat,
        RightHandLeftHandRightFeet16Beat,
        All

    }
    public Selector selector;
    public TransformPlayBacker transformPlayBacker;
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
        switch(selector) {
            case Selector.All:
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(1f, 5f, false);
                break;
            case Selector.righthand:
                ToggleSkipForLimbs("righthand");
                break;
            case Selector.lefthand:
                ToggleSkipForLimbs("lefthand");
                break;
            case Selector.rightfeet:
                ToggleSkipForLimbs("rightfeet");
                break;
            case Selector.leftfeet:
                ToggleSkipForLimbs("leftfeet");
                break;
            case Selector.snare:
                ToggleSkipForDrumType(DrumType.SnareDrum);
                break;
            case Selector.bass:
                ToggleSkipForDrumType(DrumType.BassDrum);
                break;
            case Selector.closedHiHat:
                ToggleSkipForDrumType(DrumType.ClosedHiHat);
                break;
            case Selector.openHiHat:
                ToggleSkipForDrumType(DrumType.OpenHiHat);
                break;
            case Selector.crash:
                ToggleSkipForDrumType(DrumType.Crash);
                break;
            case Selector.ride:
                ToggleSkipForDrumType(DrumType.Ride);
                break;
            case Selector.tom1:
                ToggleSkipForDrumType(DrumType.Tom1);
                break;
            case Selector.tom2:
                ToggleSkipForDrumType(DrumType.Tom2);
                break;
            case Selector.floorTom:
                ToggleSkipForDrumType(DrumType.FloorTom);
                break;
            case Selector.Beat1:
                ToggleSkipForBeats(1f);
                break;
            case Selector.Beat2:
                ToggleSkipForBeats(2f);
                break;
            case Selector.Beat3:
                ToggleSkipForBeats(3f);
                break;
            case Selector.Beat4:
                ToggleSkipForBeats(4f);
                break;
            case Selector.RightHand4Beat:
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(1f, 5f, true);
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatsAndLimbs(new float[] {1f, 2f, 3f, 4f}, new string[]{"righthand"}, false);
                break;
            case Selector.RightHand8Beat:
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(1f, 5f, true);
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatsAndLimbs(new float[] {1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f, 4.5f}, new string[]{"righthand"}, false);
                break;
            case Selector.RightHand16Beat:
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(1f, 5f, true);
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatsAndLimbs(new float[] {1f, 1.25f, 1.5f, 1.75f, 2f, 2.25f, 2.5f, 2.75f, 3f, 3.25f, 3.5f, 3.75f, 4f, 4.25f, 4.5f, 4.75f}, new string[]{"righthand"}, false);
                break;
            case Selector.BothHand4Beat:
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(1f, 5f, true);
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatsAndLimbs(new float[] {1f, 2f, 3f, 4f}, new string[]{"righthand", "lefthand"}, false);
                break;
            case Selector.BothHand8Beat:
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(1f, 5f, true);
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatsAndLimbs(new float[] {1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f, 4.5f}, new string[]{"righthand", "lefthand"}, false);
                break;
            case Selector.BothHand16Beat:
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(1f, 5f, true);
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatsAndLimbs(new float[] {1f, 1.25f, 1.5f, 1.75f, 2f, 2.25f, 2.5f, 2.75f, 3f, 3.25f, 3.5f, 3.75f, 4f, 4.25f, 4.5f, 4.75f}, new string[]{"righthand", "lefthand"}, false);
                break;
            case Selector.RightHandRightFeet4Beat:
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(1f, 5f, true);
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatsAndLimbs(new float[] {1f, 2f, 3f, 4f}, new string[]{"righthand", "rightfeet"}, false);
                break;
            case Selector.RightHandRightFeet8Beat:
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(1f, 5f, true);
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatsAndLimbs(new float[] {1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f, 4.5f}, new string[]{"righthand", "rightfeet"}, false);
                break;
            case Selector.RightHandRightFeet16Beat:
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(1f, 5f, true);
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatsAndLimbs(new float[] {1f, 1.25f, 1.5f, 1.75f, 2f, 2.25f, 2.5f, 2.75f, 3f, 3.25f, 3.5f, 3.75f, 4f, 4.25f, 4.5f, 4.75f}, new string[]{"righthand", "rightfeet"}, false);
                break;
            case Selector.RightHandLeftHandRightFeet4Beat:
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(1f, 5f, true);
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatsAndLimbs(new float[] {1f, 2f, 3f, 4f}, new string[]{"righthand", "lefthand", "rightfeet"}, false);
                break;
            case Selector.RightHandLeftHandRightFeet8Beat:
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(1f, 5f, true);
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatsAndLimbs(new float[] {1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f, 4.5f}, new string[]{"righthand", "lefthand", "rightfeet"}, false);
                break;
            case Selector.RightHandLeftHandRightFeet16Beat:
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(1f, 5f, true);
                transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatsAndLimbs(new float[] {1f, 1.25f, 1.5f, 1.75f, 2f, 2.25f, 2.5f, 2.75f, 3f, 3.25f, 3.5f, 3.75f, 4f, 4.25f, 4.5f, 4.75f}, new string[]{"righthand", "lefthand", "rightfeet"}, false);
                break;
        }
    }

    void ToggleSkipForLimbs(string limb) {
        foreach (DrumNote note in transformPlayBacker.drumSheet.drumNotes)
        {
            if (note.associatedSegment != null && note.associatedSegment.limbUsed == limb)
            {
                if (note.associatedSegment.skip)
                    transformPlayBacker.drumSheet.SetDrumNoteSkipStateForLimb(limb, false);
                else
                    transformPlayBacker.drumSheet.SetDrumNoteSkipStateForLimb(limb, true);
                break;
            }
        }
    }
    void ToggleSkipForBeats(float beat) {
        foreach (DrumNote note in transformPlayBacker.drumSheet.drumNotes)
        {
            if (note.associatedSegment != null && note.beatPosition >= beat && note.beatPosition < beat + 1f)
            {
                if (note.associatedSegment.skip)
                    transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(beat, beat + 1f, false);
                else
                    transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(beat, beat + 1f, true);
                break;
            }
        }
    }
    void ToggleSkipForDrumType(DrumType drumType) {
        foreach (DrumNote note in transformPlayBacker.drumSheet.drumNotes)
        {
            if (note.associatedSegment != null && note.drumType == drumType)
            {
                if (note.associatedSegment.skip)
                    transformPlayBacker.drumSheet.SetDrumNoteSkipStateForDrumType(drumType, false);
                else
                    transformPlayBacker.drumSheet.SetDrumNoteSkipStateForDrumType(drumType, true);
                break;
            }
        }
    }
}
