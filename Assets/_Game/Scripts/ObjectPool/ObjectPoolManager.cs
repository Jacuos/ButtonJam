using System.Collections.Generic;
using UnityEngine;

namespace _Game
{
    public class ObjectPoolManager : Manager<ObjectPoolManager>
    {
        public ObjectPool poolPrefab;
        private Dictionary<GameObject, ObjectPool> _prefabToPool;
        private Dictionary<GameObject, ObjectPool> _instanceToPool;

        protected override void Awake()
        {
            base.Awake();
            _prefabToPool = new Dictionary<GameObject, ObjectPool>();
            _instanceToPool = new Dictionary<GameObject, ObjectPool>();
        }

        public GameObject Spawn( GameObject toSpawn, 
            Vector3 position = default, Quaternion rotation = default, Transform parent = null )
        {
            var pool = GetOrCreatePool( toSpawn );
            if ( parent == null )
                parent = pool.transform;
            var instance = pool.Spawn(position,rotation,parent);
            _instanceToPool.TryAdd( instance, pool );
            return instance;
        }

        ObjectPool GetOrCreatePool(GameObject toPool)
        {
            if ( !_prefabToPool.ContainsKey( toPool) )
                GenerateNewPool( toPool );
            return _prefabToPool[toPool];
        }

        void GenerateNewPool(GameObject toPool)
        {
            var newPool = Instantiate( poolPrefab,transform );
            newPool.gameObject.name += "_"+toPool.name;
            newPool.Initizalize( toPool,50 );
            _prefabToPool.Add(toPool, newPool );
        }

        public void Despawn(GameObject toDestroy)
        {
            if ( !_instanceToPool.ContainsKey( toDestroy ) ) {
                Destroy( toDestroy );
                return;
            }
            _instanceToPool[toDestroy].Recycle( toDestroy );
        }

        public void Clean()
        {
            foreach ( ObjectPool pool in _prefabToPool.Values ) {
                pool.Clean();
            }
        }
    }
}