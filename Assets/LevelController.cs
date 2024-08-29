using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Color lockedColor = Color.gray;      // 锁定时的颜色
    public Color focusedColor = Color.yellow;   // 聚焦时的颜色
    public Color unfocusedColor = Color.white;  // 非聚焦时的颜色
    public Color passedColor = Color.green;     // 通过时的颜色
    public bool focused = false;                // 表示关卡是否被聚焦
    public bool locked = false;                 // 表示关卡是否被锁定
    public bool passed = false;                 // 表示关卡是否通过
    private Renderer levelRenderer;             // 对应关卡对象的渲染器组件

    // 新增的枚举类型，用于指定追踪的正确率类型
    public enum TrackCorrectRate
    {
        RightHand4Beat,
        RightHand8Beat,
        RightHand16Beat,
        BothHand4Beat,
        BothHand8Beat,
        BothHand16Beat,
        RightHandRightFeet4Beat,
        RightHandRightFeet8Beat,
        RightHandRightFeet16Beat,
        RightHandLeftHandRightFeet4Beat,
        RightHandLeftHandRightFeet8Beat,
        RightHandLeftHandRightFeet16Beat
    }

    public TrackCorrectRate trackCorrectRate;   // 用于指定该关卡追踪的正确率类型

    private void Awake()
    {
        levelRenderer = GetComponent<Renderer>();
        if (levelRenderer == null)
        {
            Debug.LogError("LevelController 需要一个 Renderer 组件。");
        }
    }

    // 将关卡设置为锁定状态
    public void SetLocked()
    {
        locked = true;
        if (levelRenderer != null)
        {
            levelRenderer.material.color = lockedColor;
        }
    }

    // 将关卡设置为未锁定状态
    public void SetUnLocked()
    {
        locked = false;
    }

    // 将关卡设置为聚焦状态
    public void SetFocused()
    {
        focused = true;
        if (levelRenderer != null)
        {
            levelRenderer.material.color = focusedColor;
        }
    }

    // 将关卡设置为非聚焦状态
    public void SetUnFocused()
    {
        focused = false;
        if (levelRenderer != null)
        {
            levelRenderer.material.color = unfocusedColor;
        }
    }

    // 将关卡设置为通过状态
    public void SetPassed()
    {
        passed = true;
        if (levelRenderer != null)
        {
            levelRenderer.material.color = passedColor; // 设置为通过时的颜色
        }
    }
    // 将关卡设置为通过状态
    public void SetUnPassed()
    {
        passed = false;
    }

    // 根据给定的值设置关卡的外观
    public void SetAppearanceByValue(int value)
    {
        // 根据值来设置外观逻辑
    }
}
