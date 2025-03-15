using System.Collections.Generic;
using UnityEngine;

namespace _Game
{
    public class ShirtsManager : Manager<ShirtsManager>
    {
        public Shirt shirtPrefab;
        public Vector3 queueOffset;

        private List<Shirt> _shirts = new List<Shirt>();
        public void DestroyAllShirts()
        {
            for(int i=_shirts.Count-1;i>=0;i--)
                Destroy( _shirts[i] );
            _shirts.Clear();
        }
        
        public void SpawnShirt(ColorConfig color, int slotCount)
        {
            Vector3 spawnPosition = transform.position + ( queueOffset * _shirts.Count );
            var newShirt = Instantiate( shirtPrefab,spawnPosition, transform.rotation, transform );
            newShirt.Initalize( color, slotCount );
            _shirts.Add( newShirt );
        }
    }
}