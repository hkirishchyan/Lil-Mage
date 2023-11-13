using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ObjectPool;
using Player;
using Spawning;
using UnityEngine;
using Utilities;
using Zenject;

namespace Enemy
{
    public class EnemySpawner : ObjectSpawner<AEnemy>
    {
        [SerializeField] private List<SpawnArea> _spawnAreas;
        
        private PlayerComponentManager _player;
        
        [Inject]
        public void Construct(PlayerComponentManager player)
        {
            _player = player;
        }

        private void Awake()
        {
            Pool = new ObjectPool<AEnemy>(ObjectsToSpawn,
                (enemy)=>enemy.Initialize(_player),
                (enemy)=>enemy.OnDie(), 10, true);
        }

        private void Start()
        {
            Spawn().Forget();
        }

        public override async UniTask Spawn()
        {
            while (SpawnActive)
            {
                Pool.Pull(_spawnAreas.GetRandomElement().GetRandomPointInRectangle());
                await UniTask.WaitForSeconds(SpawnDelay, cancellationToken: this.GetCancellationTokenOnDestroy());
            }
        }
    }
}