using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Game
{
    public class ShirtButtonsManager : Manager<ShirtButtonsManager>
    {
        public ShirtButton shirtButtonPrefab;
        
        public float maxDistance;

        private List<ShirtButton> _buttons = new List<ShirtButton>();
        private List<ShirtButton> _activeButtons = new List<ShirtButton>();

        private ColorConfig _activeColor;

        private void OnEnable()
        {
            ShirtButton.ButtonActivated += OnButtonActivated;
        }

        private void OnButtonActivated( ShirtButton button, bool activated)
        {
            if ( activated ) {
                _activeColor = button.colorConfig;
                _activeButtons.Add( button );
                TryFinishShirt();
            }
            else {
                _activeButtons.Remove( button );
                if ( _activeButtons.Count <= 0 )
                    _activeColor = null;
            }
        }

        void TryFinishShirt()
        {
            var currentShirt = ShirtsManager.Instance.GetCurrentShirt();
            if(currentShirt.color != _activeColor)
                return;
            if(_activeButtons.Count < currentShirt.slotCount)
                return;
            for ( int i = 0; i < currentShirt.slotCount; i++ ) {
                var activeButton = _activeButtons[i];
                activeButton.GetComponent<Rigidbody>().isKinematic = true;
                activeButton.transform.DOScale( 0.25f, 0.5f );
                var i1 = i;
                activeButton.transform.DOMove( currentShirt.buttonsSlots[i].transform.position+Vector3.up*0.1f,0.5f )
                    .OnComplete( ()=>activeButton.transform.SetParent(  currentShirt.buttonsSlots[i1].transform,true) );
            }
            DeactivateAllButtons();
            DOVirtual.DelayedCall( 0.6f, () =>
            {
                ShirtsManager.Instance.MoveToNextShirt();
            } );
        }

        public void DestroyAllButtons()
        {
            for(int i=_buttons.Count-1;i>=0;i--)
                Destroy( _buttons[i] );
            _buttons.Clear();
        }
        
        public void SpawnButtons(ColorConfig color, int buttonCount)
        {
            for ( int i = 0; i < buttonCount; i++ ) {
                Vector3 spawnPosition = transform.position + (Quaternion.Euler(90, 0, 0)*( Random.insideUnitCircle * maxDistance ));
                var newButton = Instantiate( shirtButtonPrefab, spawnPosition, transform.rotation, transform );
                newButton.Initalize( color );
                _buttons.Add( newButton );
            }
        }
        public ColorConfig GetActiveColor()
        {
            return _activeColor;
        }

        public void DeactivateAllButtons()
        {
            for(int i = _activeButtons.Count-1; i>=0;i--)
                _activeButtons[i].IsActive = false;
        }

        private void OnDisable()
        {
            ShirtButton.ButtonActivated -= OnButtonActivated;
        }
    }
}