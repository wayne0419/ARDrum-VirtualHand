using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelName; 
        public LevelController levelController;
    }

    [System.Serializable]
    public class Stage
    {
        public string stageName;
        public List<Level> levels;
    }

    public List<Stage> stages;

    private void Start()
    {
        // 确保 stages 列表不为空
        if (stages == null || stages.Count == 0)
        {
            Debug.LogError("LevelManager: Stages list is empty or null.");
            return;
        }

        // 遍历所有 stages
        for (int i = 0; i < stages.Count; i++)
        {
            Stage stage = stages[i];

            // 如果是第一个 stage，设置所有 level 为 focused
            if (i == 0)
            {
                foreach (var level in stage.levels)
                {
                    if (level.levelController != null)
                    {
                        level.levelController.SetFocused();
                    }
                }
            }
            else // 其他 stage 的 level 设置为 locked
            {
                foreach (var level in stage.levels)
                {
                    if (level.levelController != null)
                    {
                        level.levelController.SetLocked();
                    }
                }
            }
        }
    }
}
