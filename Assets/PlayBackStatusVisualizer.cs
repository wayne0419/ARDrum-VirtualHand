using UnityEngine;

public class PlayBackStatusVisualizer : MonoBehaviour
{
    public TransformPlayBacker transformPlayBacker; // 引用 TransformPlayBacker 組件
    public Renderer sphereRenderer; // 球體的 Renderer

    private Color standbyColor = Color.green;
    private Color playBackColor = Color.red;

    void Update()
    {
        if (transformPlayBacker != null && sphereRenderer != null)
        {
            if (transformPlayBacker.isPlaying)
            {
                sphereRenderer.material.color = playBackColor;
            }
            else
            {
                sphereRenderer.material.color = standbyColor;
            }
        }
    }
}