using UnityEngine;

namespace _Game
{
    [RequireComponent(typeof(CanvasGroup))]
    public class GameResultWindow : MonoBehaviour
    {
        public GameResult myState;
        private CanvasGroup _canvasGroup;
        
        void OnEnable()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            GameStateManager.GameResultChanged += OnGameResultChanged;
        }

        void OnDisable()
        {
            GameStateManager.GameResultChanged -= OnGameResultChanged;
        }

        public void OnGameResultChanged(GameResult current)
        {
            _canvasGroup.SetActive(current == myState);
        }
    }
}