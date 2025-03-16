using System;
using UnityEngine;

namespace _Game
{
    public class ShirtButton : MonoBehaviour
    {
        public static Action<ShirtButton, bool> ButtonActivated;
        public static Action<ShirtButton, Slot> ButtonPlacedInSlot;
        
        public ColorConfig colorConfig;
        public MeshRenderer mesh;
        public GameObject outline;
        
        private bool _isActive;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
                ButtonActivated?.Invoke( this, _isActive );
                outline.SetActive( _isActive );
            }
        }

        private Slot _slot;
        public Slot OccupiedSlot
        {
            get
            {
                return _slot;
            }
            set
            {
                _slot = value;
                ButtonPlacedInSlot?.Invoke( this, _slot );
            }
        }

        public bool IsFinished;

        public void Initalize(ColorConfig newColor)
        {
            colorConfig = newColor;
            mesh.material.color = newColor.color;
        }

        private void OnMouseDown()
        {
            if(IsFinished)
                return;
            if ( _slot != null ) {
                ShirtButtonsManager.Instance.TryFinishShirt( true );
                return;
            }
            var activeColor = ShirtButtonsManager.Instance.GetActiveColor();
            if(activeColor == null || activeColor == colorConfig)
                IsActive = !IsActive;
        }
    }
}