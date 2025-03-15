using UnityEngine;

namespace _Game
{
    public class RandomLevelGenerator : MonoBehaviour
    {
        private LevelConfig _currentLevelConfig;
        private void Start()
        {
            _currentLevelConfig = LevelManager.Instance.CurrentLevelConfig;
            GenerateLevel();
        }

        void GenerateLevel()
        {
            ShirtsManager.Instance.DestroyAllShirts();
            ShirtButtonsManager.Instance.DestroyAllButtons();
            for ( int i = 0; i < _currentLevelConfig.shirtsToSpawn; i++ ) {
                GenerateShirtAndButtons();   
            }
        }

        void GenerateShirtAndButtons()
        {
            ColorConfig randomColor = _currentLevelConfig.colors[Random.Range( 0, _currentLevelConfig.colors.Length )];
            int randomButtonCount = Random.Range( _currentLevelConfig.minMaxButtonSlots.x,
                _currentLevelConfig.minMaxButtonSlots.y + 1 );
            ShirtsManager.Instance.SpawnShirt(randomColor,randomButtonCount);
            ShirtButtonsManager.Instance.SpawnButtons( randomColor,randomButtonCount);
        }
    }
}