using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ObjectPool;
using UnityEngine;


namespace Spawning
{
    public abstract class ObjectSpawner<T>: MonoBehaviour where T : MonoBehaviour, IPoolable<T>
    {
        [SerializeField] private float _spawnDelay;
        [SerializeField] private List<T> _objectsToSpawn;
        
        public bool SpawnActive { get; set; } = true;
        public float SpawnDelay { get => _spawnDelay; set => _spawnDelay = value; }
        
        protected ObjectPool<T> Pool { get; set; }
        protected List<T> ObjectsToSpawn => _objectsToSpawn;


        public abstract UniTask Spawn();
    }
}