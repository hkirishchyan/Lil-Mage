using System;
using ObjectPool;
using UnityEngine;

namespace Enemy
{
    public abstract class AEnemy : MonoBehaviour, IPoolable<AEnemy>
    {
        public abstract void Initialize(AHealth attackTarget);
        public abstract void Attack();
        public abstract void Die();
        public abstract void InitializePool(Action<AEnemy> returnAction);
        public abstract void ReturnToPool();
    }
}