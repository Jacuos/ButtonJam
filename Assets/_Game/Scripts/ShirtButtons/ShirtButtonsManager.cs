using System.Collections.Generic;
using UnityEngine;

namespace _Game
{
    public class ShirtButtonsManager : Manager<ShirtButtonsManager>
    {
        public ShirtButton shirtButtonPrefab;
        
        public float maxDistance;

        private List<ShirtButton> _buttons = new List<ShirtButton>();

        public void DestroyAllButtons()
        {
            for(int i=_buttons.Count-1;i>=0;i--)
                Destroy( _buttons[i] );
            _buttons.Clear();
        }
        
        public void SpawnButtons(ColorConfig color, int buttonCount)
        {
            for ( int i = 0; i < buttonCount; i++ ) {
                Vector3 spawnPosition = transform.position + (Vector3) ( Random.insideUnitCircle * maxDistance );
                var newButton = Instantiate( shirtButtonPrefab, spawnPosition, transform.rotation, transform );
                _buttons.Add( newButton );
            }
        }
    }
}