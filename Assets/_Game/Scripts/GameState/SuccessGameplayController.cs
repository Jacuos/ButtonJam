using System;
using UnityEngine;

namespace _Game
{
    public class SuccessGameplayController : MonoBehaviour
    {
        private void OnEnable()
        {
            ShirtsManager.ShirtFinished += OnShirtFinished;
        }

        private void OnShirtFinished( int remainingShirtsCount )
        {
            if(GameStateManager.Instance.CurrentGameState != GameState.GamePlay)
                return;
            if(remainingShirtsCount !=0)
                return;
            GameStateManager.Instance.SetGameState( GameState.GameOver );
            GameStateManager.Instance.SetGameResult( GameResult.Success );
        }

        private void OnDisable()
        {
            ShirtsManager.ShirtFinished -= OnShirtFinished;
        }
    }
}