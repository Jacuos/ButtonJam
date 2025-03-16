using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Game
{
    public class SlotManager : Manager<SlotManager>
    {
        public Slot[] slots;

        public bool IsAnySlotEmpty()
        {
            foreach ( var slot in slots ) {
                if ( slot.content == null )
                    return true;
            }

            return false;
        }

        public void PutButtonOnSlot(ShirtButton button)
        {
            foreach ( var slot in slots ) {
                if ( slot.content == null ) {
                    button.GetComponent<Rigidbody>().isKinematic = true;
                    button.GetComponentInChildren<Collider>().enabled = false;
                    button.transform.DORotate( Quaternion.identity.eulerAngles,0.5f );
                    button.transform.DOMove( slot.transform.position,0.5f ).OnComplete( () =>
                    {
                        button.GetComponentInChildren<Collider>().enabled = true;
                    } );
                    slot.content = button;
                    button.OccupiedSlot = slot;
                    return;
                }
            }
        }

        public void RemoveButtonFromSlot( ShirtButton button )
        {
            var slot = button.OccupiedSlot;
            if(slot == null)
                return;
            slot.content = null;
            button.OccupiedSlot = null;
        }

        public List<ShirtButton> GetButtonsFromSlots( ColorConfig color )
        {
            List<ShirtButton> buttons = new List<ShirtButton>();
            foreach ( var slot in slots ) {
                if(slot.content == null)
                    continue;
                if ( slot.content.colorConfig != color )
                    continue;
                buttons.Add( slot.content );
            }
            return buttons;
        }
    }
}