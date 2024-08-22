using UnityEngine;

public class MetronomeNote : MonoBehaviour
{
    public Color onColor = Color.white; // 当音符被激活时的颜色
    public Color highlightColor = Color.yellow; // 当音符被高亮时的颜色

    private Renderer noteRenderer;

    private void Awake()
    {
        // 获取对象的渲染器组件
        noteRenderer = GetComponent<Renderer>();

        // 初始化时，将音符颜色设置为默认颜色
        if (noteRenderer != null)
        {
            noteRenderer.material.color = onColor;
        }
    }

    // 将音符设置为 onColor
    public void SetOn()
    {
        if (noteRenderer != null)
        {
            noteRenderer.material.color = onColor;
        }
    }

    // 将音符设置为 highlightColor
    public void SetHighlight()
    {
        if (noteRenderer != null)
        {
            noteRenderer.material.color = highlightColor;
        }
    }
}
