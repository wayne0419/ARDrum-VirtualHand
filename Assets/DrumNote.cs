using UnityEngine;

public class DrumNote : MonoBehaviour
{
    public DrumType drumType; // 鼓的类型
    public Color skippedColor; // 跳过时的颜色
    public Color defaultColor; // 默认颜色
    public TransformPlayBacker.HitSegment associatedSegment; // 关联的 HitSegment
    public float beatPosition; // 代表这个 drumNote 所对应的 hitSegment 的 beatPosition

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

    // 新增的功能：设置 associatedSegment 的 skip 状态为 true 并调整 DrumNote 的颜色
    public void SetSkip()
    {
        if (associatedSegment != null)
        {
            associatedSegment.skip = true;
            SetSkippedColor(); // 设置为 skippedColor
        }
    }

    // 新增的功能：设置 associatedSegment 的 skip 状态为 false 并调整 DrumNote 的颜色
    public void SetUnSkip()
    {
        if (associatedSegment != null)
        {
            associatedSegment.skip = false;
            SetDefaultColor(); // 设置为 defaultColor
        }
    }

    // 当鼠标点击此 DrumNote 时，执行 ToggleSkip
    void OnMouseDown()
    {
        if (associatedSegment != null)
        {
            if (associatedSegment.skip)
            {
                SetUnSkip();
            }
            else
            {
                SetSkip();
            }
        }
    }
}
