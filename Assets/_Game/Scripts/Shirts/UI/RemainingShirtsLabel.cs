using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Game
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class RemainingShirtsLabel : MonoBehaviour
    {
        private TextMeshProUGUI _label;

        private void OnEnable()
        {
            _label = GetComponent<TextMeshProUGUI>();
            ShirtsManager.ShirtCountChanged += OnShirtCount;
        }

        private void OnShirtCount( int count )
        {
            _label.text = "" + count;
            _label.transform.DOKill(true);
            _label.transform.DOPunchScale( Vector3.one* 1.2f, 0.25f );
        }

        private void OnDisable()
        {
            ShirtsManager.ShirtCountChanged -= OnShirtCount;
        }
    }
}