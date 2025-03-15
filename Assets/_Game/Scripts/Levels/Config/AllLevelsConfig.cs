using UnityEngine;

namespace _Game
{
    [CreateAssetMenu(menuName = "Configs/AllLevelsConfig")]
    public class AllLevelsConfig : ScriptableObject
    {
        public LevelConfig[] levels;
    }
}