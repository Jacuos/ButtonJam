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
        private Rigidbody _rigidbody;
        private Collider _collider;
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

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponentInChildren<Collider>();
        }

        public void Initalize(ColorConfig newColor)
        {
            colorConfig = newColor;
            mesh.material.color = newColor.color;
            IsFinished = false;
            _rigidbody.isKinematic = false;
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _collider.enabled = true;
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