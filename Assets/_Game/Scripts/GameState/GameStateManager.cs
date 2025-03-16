using System;

namespace _Game
{
    public class GameStateManager : Manager<GameStateManager>
    {
        public static Action<GameState> GameStateChanged;
        public static Action<GameResult> GameResultChanged;
        public GameState CurrentGameState
        {
            get;
            private set;
        }
        public GameState PreviousGameState
        {
            get;
            private set;
        }
                
        public GameResult CurrentGameResult
        {
            get;
            private set;
        }
        protected override void Awake()
        {
            base.Awake();
            CurrentGameState = GameState.None;
            PreviousGameState = GameState.None;
            SetGameState( GameState.MainMenu );
        }

        public void SetGameState(GameState newState)
        {
            PreviousGameState = CurrentGameState;
            CurrentGameState = newState;
            GameStateChanged?.Invoke( CurrentGameState );
        }

        public void SetGameResult( GameResult result )
        {
            CurrentGameResult = result;
            GameResultChanged?.Invoke( result );
        }
    }

    public enum GameState
    {
        None, MainMenu, GamePlay,GameOver
    }

    public enum GameResult
    {
        None,Success,Failure
    }
}