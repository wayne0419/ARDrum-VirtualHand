using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Beat4

    }
    public Selector selector;
    public DrumSheet drumSheet;
    

    
    void OnMouseDown() {
        switch(selector) {
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
            
        }
    }

    void ToggleSkipForLimbs(string limb) {
        foreach (DrumNote note in drumSheet.drumNotes)
        {
            if (note.associatedSegment != null && note.associatedSegment.limbUsed == limb)
            {
                if (note.associatedSegment.skip)
                    drumSheet.SetDrumNoteSkipStateForLimb(limb, false);
                else
                    drumSheet.SetDrumNoteSkipStateForLimb(limb, true);
                break;
            }
        }
    }
    void ToggleSkipForBeats(float beat) {
        foreach (DrumNote note in drumSheet.drumNotes)
        {
            if (note.associatedSegment != null && note.beatPosition >= beat && note.beatPosition < beat + 1f)
            {
                if (note.associatedSegment.skip)
                    drumSheet.SetDrumNoteSkipStateForBeatRange(beat, beat + 1f, false);
                else
                    drumSheet.SetDrumNoteSkipStateForBeatRange(beat, beat + 1f, true);
                break;
            }
        }
    }
    void ToggleSkipForDrumType(DrumType drumType) {
        foreach (DrumNote note in drumSheet.drumNotes)
        {
            if (note.associatedSegment != null && note.drumType == drumType)
            {
                if (note.associatedSegment.skip)
                    drumSheet.SetDrumNoteSkipStateForDrumType(drumType, false);
                else
                    drumSheet.SetDrumNoteSkipStateForDrumType(drumType, true);
                break;
            }
        }
    }
}
