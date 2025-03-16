using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game
{
    public class ShirtButtonHintController : MonoBehaviour
    {
        public float hintWaitTime = 10;

        private float _lastActionTimestamp;
        private List<ShirtButton> _shirtButtons;
        private List<ShirtButtonHintView> _hintedButtons = new List<ShirtButtonHintView>();
        void Start()
        {
            _lastActionTimestamp = Time.time;
        }

        private void Update()
        {
            if ( GameStateManager.Instance.CurrentGameState != GameState.GamePlay ) {
                _lastActionTimestamp = Time.time;
                return;
            }

            if ( Input.GetMouseButtonDown( 0 ) ) {
                _lastActionTimestamp = Time.time;
                HideHints();
            }
            float timeSinceLastAction = Time.time - _lastActionTimestamp;
            if(timeSinceLastAction > hintWaitTime && _hintedButtons.Count ==0)
                ShowHint();
        }

        void HideHints()
        {
            foreach ( var hintedButton in _hintedButtons ) {
                hintedButton.ShowHideHint( false );
            }
            _hintedButtons.Clear();
        }

        void ShowHint()
        {
            _shirtButtons = ShirtButtonsManager.Instance.GetAllButtons();
            var currentShirt = ShirtsManager.Instance.GetCurrentShirt();
            var currentColor = currentShirt.color;
            var requiredButtons = currentShirt.slotCount;
            foreach ( var button in _shirtButtons ) {
                if(button.IsFinished)
                    continue;
                if(button.colorConfig != currentColor)
                    continue;
                var hintedButton = button.GetComponent<ShirtButtonHintView>();
                hintedButton.ShowHideHint( true );
                _hintedButtons.Add( hintedButton );
                if(_hintedButtons.Count >= requiredButtons)
                    return;
            }
        }
    }
}