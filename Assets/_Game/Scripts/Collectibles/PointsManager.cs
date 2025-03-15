using System;
using UnityEngine.Serialization;

namespace _Game
{
    public class PointsManager : Manager<PointsManager>
    {
        public static Action<int> PointsChanged;
        
        [FormerlySerializedAs( "levelsConfig" )]
        public AllLevelsConfig allLevelsConfig;

        public int Points
        {
            get;
            private set;
        }

        private void OnEnable()
        {
            GameStateManager.GameStateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            GameStateManager.GameStateChanged -= OnGameStateChanged;
        }
        
        private void OnGameStateChanged( GameState current )
        {
            if ( current != GameState.GamePlay )
                return;
            ResetPoints();
        }

        public void ResetPoints()
        {
            Points = 0;
            PointsChanged?.Invoke(0);
        }

        public void AddPoints(int count)
        {
            Points += count;
            PointsChanged?.Invoke( Points );
            //if(Points >= allLevelsConfig.minPointsToWin)
            //    GameStateManager.Instance.SetGameState( GameState.GameOver );
        }
    }
}