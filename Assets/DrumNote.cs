using UnityEngine;

public class DrumNote : MonoBehaviour
{
    public DrumType drumType; // 鼓的类型
    public Color skippedColor; // 跳过时的颜色
    public Color defaultColor; // 默认颜色
    public Color highlightColor; // 高亮时的颜色
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

    public void SetHighlightColor()
    {
        if (noteRenderer != null)
        {
            noteRenderer.material.color = highlightColor;
        }
    }
}
