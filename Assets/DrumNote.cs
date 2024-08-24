using UnityEngine;

public class DrumNote : MonoBehaviour
{
    public DrumType drumType; // 鼓的类型
    public Color skippedColor; // 跳过时的颜色
    public Color defaultColor; // 默认颜色
    public TransformPlayBacker.HitSegment associatedSegment; // 关联的 HitSegment

    private Renderer noteRenderer;

    void Awake()
    {
        noteRenderer = GetComponent<Renderer>();
        if (noteRenderer != null)
        {
            noteRenderer.material.color = defaultColor; // 设置为默认颜色
        }
    }

    public void SetSkippedColor()
    {
        if (noteRenderer != null)
        {
            noteRenderer.material.color = skippedColor;
        }
    }

    public void SetDefaultColor()
    {
        if (noteRenderer != null)
        {
            noteRenderer.material.color = defaultColor;
        }
    }

    // 新增的功能：切换 associatedSegment 的 skip 状态并调整 DrumNote 的颜色
    public void ToggleSkip()
    {
        if (associatedSegment != null)
        {
            // 切换 skip 状态
            associatedSegment.skip = !associatedSegment.skip;

            // 根据 skip 状态调整 DrumNote 的颜色
            if (associatedSegment.skip)
            {
                SetSkippedColor(); // 如果跳过，设置为 skippedColor
            }
            else
            {
                SetDefaultColor(); // 否则，设置为 defaultColor
            }
        }
    }

    // 当鼠标点击此 DrumNote 时，执行 ToggleSkip
    void OnMouseDown()
    {
        ToggleSkip();
    }
}
