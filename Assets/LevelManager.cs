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

    
}
