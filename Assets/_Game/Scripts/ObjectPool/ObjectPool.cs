using System.Collections.Generic;
using UnityEngine;

namespace _Game
{
    public class ObjectPool : MonoBehaviour
    {
        private GameObject _prefab;
        private Queue<GameObject> _objectsQueue;

        public void Initizalize(GameObject prefab, int startSize)
        {
            _prefab = prefab;
            _objectsQueue = new Queue<GameObject>(startSize);
        }

        public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent)
        {
            if ( _objectsQueue.Count > 0 ) {
                var existingObject = _objectsQueue.Dequeue();
                if ( existingObject != null ) {
                    existingObject.transform.position = position;
                    existingObject.transform.rotation = rotation;
                    existingObject.transform.localScale = _prefab.transform.localScale;
                    existingObject.transform.SetParent( parent, true );
                    existingObject.SetActive( true );
                    return existingObject;
                }
            }
            var newObject = Object.Instantiate( _prefab, position, rotation, parent );
            return newObject;
        }

        public void Recycle(GameObject instance)
        {
            instance.SetActive( false );
            instance.transform.SetParent( transform );
            _objectsQueue.Enqueue( instance );
        }
        
        public void Clean()
        {
            foreach (Transform child in transform) {
                if(child.gameObject.activeSelf)
                    Recycle(child.gameObject);
            }
        }
    }
}