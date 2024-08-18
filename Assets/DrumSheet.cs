using System.Collections.Generic;
using UnityEngine;

public class DrumSheet : MonoBehaviour
{
    public List<DrumNote> drumNotes; // 存储所有的 DrumNote

    // 方法用于高亮所有音符
    public void HighlightAllNotes()
    {
        foreach (DrumNote note in drumNotes)
        {
            note.SetHighlightColor();
        }
    }

    // 方法用于重置所有音符的颜色为默认颜色
    public void ResetAllNotes()
    {
        foreach (DrumNote note in drumNotes)
        {
            note.SetDefaultColor();
        }
    }

    // 方法用于根据鼓的类型跳过特定音符
    public void SkipNoteByType(DrumType drumType)
    {
        foreach (DrumNote note in drumNotes)
        {
            if (note.drumType == drumType)
            {
                note.SetSkippedColor();
            }
        }
    }

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
}
