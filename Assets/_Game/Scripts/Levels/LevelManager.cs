using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace _Game
{
    public class LevelManager : Manager<LevelManager>
    {
        public static Action NewLevelLoaded;
        public static Action MainMenuLoaded;
        
        [Scene]
        public int gameplayScene;
        [Scene]
        public int mainMenuScene;
        [FormerlySerializedAs( "levelsConfig" )]
        public AllLevelsConfig allLevelsConfig;
        
        public LevelConfig CurrentLevelConfig
        {get;private set;}

        public float LevelStartTimestamp
        {get;private set;}

        private int _levelNumber;
        
        void OnEnable()
        {
            GameStateManager.GameStateChanged += OnGameStateChanged;   
        }

        void OnDisable()
        {
            GameStateManager.GameStateChanged -= OnGameStateChanged;    
        }
        
        private void OnGameStateChanged( GameState current )
        {
           if(current == GameState.GamePlay)
               LoadLevel();
           else if ( current == GameState.MainMenu )
               LoadMenu();
        }
        async Awaitable LoadLevel()
        {
            SceneManager.UnloadSceneAsync( SceneManager.GetActiveScene());
            if ( GameStateManager.Instance.CurrentGameResult == GameResult.Success )
                _levelNumber++;
            CurrentLevelConfig = allLevelsConfig.levels[_levelNumber % allLevelsConfig.levels.Length];
            await SceneManager.LoadSceneAsync( gameplayScene , LoadSceneMode.Additive);
            SceneManager.SetActiveScene( SceneManager.GetSceneByBuildIndex( gameplayScene ) );
            LevelStartTimestamp = Time.time;
            NewLevelLoaded?.Invoke();
        }
        async Awaitable LoadMenu()
        {
            SceneManager.UnloadSceneAsync( SceneManager.GetActiveScene());
            await SceneManager.LoadSceneAsync( mainMenuScene , LoadSceneMode.Additive);
            SceneManager.SetActiveScene( SceneManager.GetSceneByBuildIndex( mainMenuScene ) );
            MainMenuLoaded?.Invoke();
        }
    }
}