using System;
using UnityEngine;

namespace _Game
{
    public class FailureGameplayController : MonoBehaviour
    {
        private float _levelStartTimestamp;
        private void OnEnable()
        {
            LevelManager.NewLevelLoaded += OnLevelLoaded;
        }

        void OnLevelLoaded()
        {
            _levelStartTimestamp = LevelManager.Instance.LevelStartTimestamp;
        }

        private void Update()
        {
            if ( GameStateManager.Instance.CurrentGameState != GameState.GamePlay ) {
                _levelStartTimestamp = -1;
                return;
            }
            if(_levelStartTimestamp <0)
                return;
            float timeSinceStart = Time.time - _levelStartTimestamp;
            if ( timeSinceStart > LevelManager.Instance.CurrentLevelConfig.failureTime )
                FailLevel();
        }

        void FailLevel()
        {
            GameStateManager.Instance.SetGameState( GameState.GameOver );
            GameStateManager.Instance.SetGameResult( GameResult.Failure );
            _levelStartTimestamp = -1;
        }

        private void OnDisable()
        {
            LevelManager.NewLevelLoaded -= OnLevelLoaded;
        }
    }
}