using UnityEngine;

namespace _Game
{
    [CreateAssetMenu(menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public ColorConfig[] colors;
        public float failureTime;
        public int shirtsToSpawn;
        public Vector2Int minMaxButtonSlots;
    }
}