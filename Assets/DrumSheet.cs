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
}
