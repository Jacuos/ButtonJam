using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Game
{
    [RequireComponent(typeof(Button))]
    public class AutoPlaceADButton : MonoBehaviour
    {
        private Button _button;

        void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener( OnButtonClicked );
        }

        void OnButtonClicked()
        {
            var shirtButton = FindShirtButton();
            if(shirtButton != null)
                AutoPlaceButton(shirtButton);
        }

        ShirtButton FindShirtButton()
        {
            var shirtButtons = ShirtButtonsManager.Instance.GetAllButtons();
            var currentShirt = ShirtsManager.Instance.GetCurrentShirt();
            var currentColor = currentShirt.color;
            foreach ( var button in shirtButtons ) {
                if(button.IsFinished)
                    continue;
                if(button.colorConfig != currentColor)
                    continue;
                return button;
            }
            return null;
        }

        void AutoPlaceButton(ShirtButton currentButton)
        {
            var currentShirt = ShirtsManager.Instance.GetCurrentShirt();
            if(currentShirt.slotCount<=0)
                return;
            currentShirt.slotCount--;
            var targetSlot = currentShirt.buttonsSlots[currentShirt.slotCount];
            SlotManager.Instance.RemoveButtonFromSlot( currentButton );
            currentButton.IsFinished = true;
            currentButton.GetComponent<Rigidbody>().isKinematic = true;
            currentButton.GetComponentInChildren<Collider>().enabled = false;
            currentButton.transform.SetParent( targetSlot.transform, true );
            currentButton.transform.DORotate( Quaternion.identity.eulerAngles,0.5f );
            currentButton.transform.DOScale( 0.25f, 0.5f );
            currentButton.transform.DOLocalMove(  Vector3.back * 0.1f, 0.5f );
            ShirtButtonsManager.Instance.TryFinishShirt();
        }
        
    }
}