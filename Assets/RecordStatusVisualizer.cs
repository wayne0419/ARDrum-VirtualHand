using UnityEngine;

public class RecordStatusVisualizer : MonoBehaviour
{
    public TransformRecorder transformRecorder; // 參考 TransformRecorder 組件
    public Renderer sphereRenderer; // 球體的渲染器

    void Start()
    {
        if (sphereRenderer == null)
        {
            sphereRenderer = GetComponent<Renderer>();
        }
        UpdateColor();
    }

    void Update()
    {
        UpdateColor();
    }

    void UpdateColor()
    {
        if (transformRecorder == null || sphereRenderer == null)
        {
            return;
        }

        if (transformRecorder.isRecordingInProgress)
        {
            if (transformRecorder.isRecording)
            {
                // 記錄中
                sphereRenderer.material.color = Color.red;
            }
            else
            {
                // 記錄延遲中
                sphereRenderer.material.color = Color.yellow;
            }
        }
        else
        {
            // 待機中
            sphereRenderer.material.color = Color.green;
        }
    }
}
