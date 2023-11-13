using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Object = UnityEngine.Object;

namespace ObjectPool
{
   public class ObjectPool<T> : IPool<T> where T : MonoBehaviour, IPoolable<T>
    {
        private readonly IList<T> _prefabs;
        private readonly Action<T> _pullObject;
        private readonly Action<T> _pushObject;
        private readonly Stack<T> _pooledObjects = new();
        protected readonly bool _lockExpansion;
        public int PooledCount => _pooledObjects.Count;
        
        
        public ObjectPool(IList<T> pooledObject, int numToSpawn = 0, bool lockExpansion = false)
        {
            _prefabs = pooledObject;
            _lockExpansion = lockExpansion;
            Spawn(numToSpawn);
        }

        public ObjectPool(IList<T> pooledObject, Action<T> pullObject, Action<T> pushObject, int numToSpawn = 0, bool lockExpansion = false)
        {
            _prefabs = pooledObject;
            _lockExpansion = lockExpansion;
            _pullObject = pullObject;
            _pushObject = pushObject;
            Spawn(numToSpawn);
        }

        private void Spawn(int number)
        {
            for (int i = 0; i < number; i++)
            {
                T spawnObject = Object.Instantiate(_prefabs.GetRandomElement());
                _pooledObjects.Push(spawnObject);
                spawnObject.gameObject.SetActive(false);
            }
        }
        public T Pull()
        {
            T pullObject;
            
            if (PooledCount > 0) pullObject = _pooledObjects.Pop();
            else if (!_lockExpansion) pullObject = Object.Instantiate(_prefabs.GetRandomElement());
            else return null;
            
            pullObject.gameObject.SetActive(true);
            pullObject.Initialize(Push);
            _pullObject?.Invoke(pullObject);
            return pullObject;
        }
        
        public T Pull(Vector3 position)
        {
            T pullObject = Pull();
            if (pullObject == null) return null;
            pullObject.transform.position = position;
            return pullObject;
        }

        public T Pull(Vector3 position, Quaternion rotation)
        {
            T pullObject = Pull();
            if (pullObject == null) return null;
            pullObject.transform.position = position;
            pullObject.transform.rotation = rotation;
            return pullObject;
        }
        
        public void Push(T pushObject)
        {
            _pooledObjects.Push(pushObject);
            _pushObject?.Invoke(pushObject);

            pushObject.gameObject.SetActive(false);
        }
    }
}
