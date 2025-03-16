using System;
using TMPro;
using UnityEngine;

namespace _Game
{
    public class LevelTimeCounter : MonoBehaviour
    {
        public TextMeshProUGUI label;

        void Update()
        {
            if(GameStateManager.Instance == null || GameStateManager.Instance.CurrentGameState != GameState.GamePlay)
                return;
            float currentTime = Time.time - LevelManager.Instance.LevelStartTimestamp;
            float loseTime = LevelManager.Instance.CurrentLevelConfig.failureTime;
            label.text = "" + String.Format("{0:0.0}", Mathf.Max(0,loseTime - currentTime));
        }
    }
}