using System.Collections.Generic;
using UnityEngine;

public class DrumSheet : MonoBehaviour
{
    public List<DrumNote> drumNotes; // 存储所有的 DrumNote

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
