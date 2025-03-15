using UnityEngine;

namespace _Game
{
    public class DeactivateButtonsCollider : MonoBehaviour
    {
        
        void OnMouseDown()
        {
            ShirtButtonsManager.Instance.DeactivateAllButtons();
        }
    }
}