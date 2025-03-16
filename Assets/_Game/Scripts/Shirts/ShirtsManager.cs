using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game
{
    public class ShirtsManager : Manager<ShirtsManager>
    {
        public static Action<int> ShirtFinished;
        
        public Shirt shirtPrefab;
        public Vector3 queueOffset;
        public Transform exitPoint;

        private List<Shirt> _shirts = new List<Shirt>();
        

        public void SpawnShirt(ColorConfig color, int slotCount)
        {
            Vector3 spawnPosition = transform.position + ( queueOffset * _shirts.Count );
            var newShirt = Instantiate( shirtPrefab,spawnPosition, transform.rotation, transform );
            newShirt.Initalize( color, slotCount );
            _shirts.Add( newShirt );
        }

        public Shirt GetCurrentShirt()
        {
            return _shirts[0];
        }

        public void MoveToNextShirt()
        {
            _shirts[0].transform.DOMove( exitPoint.position, 0.5f );
            for ( int i = 1; i < _shirts.Count; i++ ) {
                Vector3 spawnPosition = transform.position + ( queueOffset * (i-1) );
                _shirts[i].transform.DOMove( spawnPosition, 0.5f );
            }
            _shirts.RemoveAt( 0 );
            ShirtFinished?.Invoke( _shirts.Count );
        }
        
        public void DestroyAllShirts()
        {
            for(int i=_shirts.Count-1;i>=0;i--)
                Destroy( _shirts[i] );
            _shirts.Clear();
        }
    }
}