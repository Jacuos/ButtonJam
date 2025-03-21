using System;
using DG.Tweening;
using UnityEngine;

namespace _Game
{
    public class ShirtButtonHintView : MonoBehaviour
    {
        public GameObject view;

        private void OnEnable()
        {
            view.SetActive( false );
            view.transform.DOKill();
            view.transform.localScale = Vector3.one;
        }

        public void ShowHideHint( bool show )
        {
            view.SetActive( show );
            view.transform.DOKill();
            view.transform.localScale = Vector3.one;
            if ( show )
                view.transform.DOScale( 0, 0.5f ).SetEase( Ease.InOutCubic ).SetLoops( -1, LoopType.Yoyo );
        }
    }
}