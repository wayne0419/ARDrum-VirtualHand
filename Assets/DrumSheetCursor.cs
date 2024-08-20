using UnityEngine;

public class DrumSheetCursor : MonoBehaviour
{
    public DrumSheet drumSheet; // DrumSheet 的引用
    public TransformPlayBacker transformPlayBacker; // TransformPlayBacker 的引用

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float journeyLength;
    private bool isMoving = false;
    private float startTime;

    private void OnEnable()
    {
        // 订阅 TransformPlayBacker 的事件
        if (transformPlayBacker != null)
        {
            transformPlayBacker.OnPlayTransformDataStart += StartMoving;
            transformPlayBacker.OnPlayTransformDataEnd += StopMoving;
        }
    }

    private void OnDisable()
    {
        // 取消订阅 TransformPlayBacker 的事件
        if (transformPlayBacker != null)
        {
            transformPlayBacker.OnPlayTransformDataStart -= StartMoving;
            transformPlayBacker.OnPlayTransformDataEnd -= StopMoving;
        }
    }

    private void StartMoving()
    {
        if (drumSheet != null && drumSheet.drumSheetCursorStart != null && drumSheet.drumSheetCursorEnd != null)
        {
            // 获取起始和结束位置
            startPosition = drumSheet.drumSheetCursorStart.position;
            endPosition = drumSheet.drumSheetCursorEnd.position;

            // 计算整个旅程的长度
            journeyLength = Vector3.Distance(startPosition, endPosition);

            // 将光标移动到起始位置
            transform.position = startPosition;

            // 记录开始时间
            startTime = Time.time;

            // 开始移动
            isMoving = true;
        }
    }

    private void StopMoving()
    {
        isMoving = false;
    }

    private void Update()
    {
        if (isMoving && journeyLength > 0)
        {
            // 计算已经过的时间
            float elapsedTime = Time.time - startTime;

            // 计算完成比例
            float fracJourney = elapsedTime / transformPlayBacker.playbackData.dataList[transformPlayBacker.playbackData.dataList.Count - 1].timestamp;

            // 根据完成比例插值位置
            transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);

            // 如果光标到达终点，停止移动
            if (fracJourney >= 1f)
            {
                StopMoving();
            }
        }
    }
}
