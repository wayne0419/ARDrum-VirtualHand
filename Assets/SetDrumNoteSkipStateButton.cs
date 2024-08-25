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
}
