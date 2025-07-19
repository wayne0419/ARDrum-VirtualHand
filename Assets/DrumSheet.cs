using System.Collections.Generic;
using UnityEngine;

public class DrumSheet : MonoBehaviour
{
    public List<DrumNote> drumNotes; // A list containing all DrumNote objects in the drum sheet.

    public Transform drumSheetCursorStart; // The starting position for the drum sheet cursor.
    public Transform drumSheetCursorEnd;   // The ending position for the drum sheet cursor.
    public Transform drumSheetCrashRowAnchor;  // Anchor point for the Crash cymbal row on the drum sheet.
    public Transform drumSheetOpenHiHatRowAnchor;  // Anchor point for the Open Hi-Hat row.
    public Transform drumSheetClosedHiHatRowAnchor;  // Anchor point for the Closed Hi-Hat row.
    public Transform drumSheetRideRowAnchor;  // Anchor point for the Ride cymbal row.
    public Transform drumSheetTom1RowAnchor;  // Anchor point for the Tom 1 row.
    public Transform drumSheetTom2RowAnchor;  // Anchor point for the Tom 2 row.
    public Transform drumSheetSnareRowAnchor;  // Anchor point for the Snare drum row.
    public Transform drumSheetFloorTomRowAnchor;  // Anchor point for the Floor Tom row.
    public Transform drumSheetBassRowAnchor;  // Anchor point for the Bass drum row.
    public Transform drumSheetFloorHiHatRowAnchor;  // Anchor point for the Foot Hi-Hat row.

    /// <summary>
    /// Retrieves a DrumNote of a specific type by its index within that type.
    /// </summary>
    /// <param name="drumType">The type of drum to search for.</param>
    /// <param name="index">The zero-based index of the note within the specified drum type.</param>
    /// <returns>The DrumNote at the given index for the specified drum type, or null if not found.</returns>
    public DrumNote GetDrumNoteByIndex(DrumType drumType, int index)
    {
        int count = 0;
        foreach (DrumNote note in drumNotes)
        {
            if (note.drumType == drumType)
            {
                if (count == index)
                {
                    return note;
                }
                count++;
            }
        }
        return null; // Return null if no matching DrumNote is found.
    }

    /// <summary>
    /// Sets the skip state (skipped or unskipped) for all DrumNotes associated with a specific limb.
    /// </summary>
    /// <param name="limb">The limb identifier (e.g., "lefthand", "righthand") to filter notes by.</param>
    /// <param name="skipState">True to mark notes as skipped, false to unmark them.</param>
    public void SetDrumNoteSkipStateForLimb(string limb, bool skipState)
    {
        foreach (DrumNote note in drumNotes)
        {
            if (note.associatedSegment != null && note.associatedSegment.limbUsed == limb)
            {
                if (skipState)
                {
                    note.SetSkip(); // Mark the note as skipped.
                }
                else
                {
                    note.SetUnSkip(); // Unmark the note as skipped.
                }
            }
        }
    }

    /// <summary>
    /// Sets the skip state for all DrumNotes whose beat position falls within a specified range.
    /// The range is inclusive for the start beat and exclusive for the end beat.
    /// </summary>
    /// <param name="beatStart">The starting beat position of the range (inclusive).</param>
    /// <param name="beatEnd">The ending beat position of the range (exclusive).</param>
    /// <param name="skipState">True to mark notes as skipped, false to unmark them.</param>
    public void SetDrumNoteSkipStateForBeatRange(float beatStart, float beatEnd, bool skipState)
    {
        foreach (DrumNote note in drumNotes)
        {
            if (note.beatPosition >= beatStart && note.beatPosition < beatEnd)
            {
                if (skipState)
                {
                    note.SetSkip(); // Mark the note as skipped.
                }
                else
                {
                    note.SetUnSkip(); // Unmark the note as skipped.
                }
            }
        }
    }

    /// <summary>
    /// Sets the skip state for all DrumNotes of a specific drum type.
    /// </summary>
    /// <param name="drumType">The type of drum (e.g., DrumType.Snare) to filter notes by.</param>
    /// <param name="skipState">True to mark notes as skipped, false to unmark them.</param>
    public void SetDrumNoteSkipStateForDrumType(DrumType drumType, bool skipState)
    {
        foreach (DrumNote note in drumNotes)
        {
            if (note.drumType == drumType)
            {
                if (skipState)
                {
                    note.SetSkip(); // Mark the note as skipped.
                }
                else
                {
                    note.SetUnSkip(); // Unmark the note as skipped.
                }
            }
        }
    }

    /// <summary>
    /// Sets the skip state for DrumNotes that match both a specific beat position and a specific limb.
    /// </summary>
    /// <param name="beats">An array of beat positions to match.</param>
    /// <param name="limbs">An array of limb identifiers to match.</param>
    /// <param name="skipState">True to mark notes as skipped, false to unmark them.</param>
    public void SetDrumNoteSkipStateForBeatsAndLimbs(float[] beats, string[] limbs, bool skipState)
    {
        foreach (DrumNote note in drumNotes)
        {
            // Check if the DrumNote's associated limb is present in the provided limbs array.
            if (note.associatedSegment != null && System.Array.Exists(limbs, limb => limb == note.associatedSegment.limbUsed))
            {
                // Check if the DrumNote's beat position is approximately equal to any beat in the provided beats array.
                if (System.Array.Exists(beats, beat => Mathf.Approximately(beat, note.beatPosition)))
                {
                    if (skipState)
                    {
                        note.SetSkip(); // Mark the note as skipped.
                    }
                    else
                    {
                        note.SetUnSkip(); // Unmark the note as skipped.
                    }
                }
            }
        }
    }
}