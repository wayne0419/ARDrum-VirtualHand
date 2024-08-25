using System.Collections.Generic;
using UnityEngine;

public class DrumSheet : MonoBehaviour
{
    public List<DrumNote> drumNotes; // 存储所有的 DrumNote

    public Transform drumSheetCursorStart; // 鼓谱光标的起始位置
    public Transform drumSheetCursorEnd;   // 鼓谱光标的结束位置

    // 新增方法：找出某个 drumType 的第 i 个 DrumNote
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
        return null; // 如果找不到对应的 DrumNote，则返回 null
    }

    // 更新后的方法：设置所有使用指定 limb 的 DrumNote 为跳过或不跳过状态
    public void SetDrumNoteSkipStateForLimb(string limb, bool skipState)
    {
        foreach (DrumNote note in drumNotes)
        {
            if (note.associatedSegment != null && note.associatedSegment.limbUsed == limb)
            {
                if (skipState)
                {
                    note.SetSkip(); // 如果 skipState 为 true，则设置为跳过状态
                }
                else
                {
                    note.SetUnSkip(); // 如果 skipState 为 false，则设置为不跳过状态
                }
            }
        }
    }

    // 更新后的方法：设置所有 beatPosition 在 [beatStart, beatEnd) 范围内的 DrumNote 为跳过或不跳过状态
    public void SetDrumNoteSkipStateForBeatRange(float beatStart, float beatEnd, bool skipState)
    {
        foreach (DrumNote note in drumNotes)
        {
            if (note.beatPosition >= beatStart && note.beatPosition < beatEnd)
            {
                if (skipState)
                {
                    note.SetSkip(); // 如果 skipState 为 true，则设置为跳过状态
                }
                else
                {
                    note.SetUnSkip(); // 如果 skipState 为 false，则设置为不跳过状态
                }
            }
        }
    }

    // 更新后的方法：设置所有 drumType 符合的 DrumNote 为跳过或不跳过状态
    public void SetDrumNoteSkipStateForDrumType(DrumType drumType, bool skipState)
    {
        foreach (DrumNote note in drumNotes)
        {
            if (note.drumType == drumType)
            {
                if (skipState)
                {
                    note.SetSkip(); // 如果 skipState 为 true，则设置为跳过状态
                }
                else
                {
                    note.SetUnSkip(); // 如果 skipState 为 false，则设置为不跳过状态
                }
            }
        }
    }

    // 新增方法：根据提供的 beats 和 limbs 设置 DrumNote 的跳过状态
    public void SetDrumNoteSkipStateForBeatsAndLimbs(float[] beats, string[] limbs, bool skipState)
    {
        foreach (DrumNote note in drumNotes)
        {
            // 检查 DrumNote 的 limbUsed 是否在提供的 limbs 数组中
            if (note.associatedSegment != null && System.Array.Exists(limbs, limb => limb == note.associatedSegment.limbUsed))
            {
                // 检查 DrumNote 的 beatPosition 是否在提供的 beats 数组中
                if (System.Array.Exists(beats, beat => Mathf.Approximately(beat, note.beatPosition)))
                {
                    if (skipState)
                    {
                        note.SetSkip(); // 如果 skipState 为 true，则设置为跳过状态
                    }
                    else
                    {
                        note.SetUnSkip(); // 如果 skipState 为 false，则设置为不跳过状态
                    }
                }
            }
        }
    }
}
